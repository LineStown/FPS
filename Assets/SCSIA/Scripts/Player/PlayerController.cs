using Assets.SCSIA.Scripts.Input;
using Assets.SCSIA.Scripts.Weapon;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets.SCSIA.Scripts.Player
{
    internal class PlayerController : MonoBehaviour
    {
        //############################################################################################
        // FIELDS
        //############################################################################################
        [Header("Look Control Settings")]
        [SerializeField] private Camera _lookCamera;
        [SerializeField] private Transform _lookSpot;
        [SerializeField] private float _lookSpotHeight = 1.75f;
        [SerializeField, Range(-80, -20)] private float _lookMinPitch = -80f;
        [SerializeField, Range(20, 80)] private float _lookMaxPitch = 80f;
        [SerializeField] private float _lookSensitivity = 0.08f;

        [Header("Move Control Settings")]
        [SerializeField] private Rigidbody _playerRigidbody;
        [SerializeField] private CapsuleCollider _playerCapsuleCollider;
        [SerializeField] private float _walkSpeed = 10f;
        [SerializeField] private float _sprintSpeed = 15f;
        [SerializeField] private float _accelerateMultiplier = 10f;
        [SerializeField] private float _airMultiplier = 0.2f;
        [SerializeField] private float _jumpPower = 7f;

        [Header("Weapon Control Settings")]
        [SerializeField] private WeaponController _weaponController;

        private GIS _gis;
        private Vector2 _lookDirection = Vector2.zero;
        private Vector2 _moveInput = Vector2.zero;
        private bool _sprintEnabled = false;
        private bool _jumpEnabled = false;
        private bool _jumpWithSprint = false;
        private bool _grounded = false;
        private float _groundedRayDistance;

        //############################################################################################
        // PUBLIC METHODS
        //############################################################################################
        public void Initialize()
        {
            // disable cursor
            Cursor.lockState = CursorLockMode.Locked;
            // setup GIS
            SetupGIS();
            // setup look spot
            SetupLookSpot();
            // setup look camera
            SetupLookCamera();
            // calculate grounded ray distance
            _groundedRayDistance = _playerCapsuleCollider.height * 0.5f + _playerCapsuleCollider.height * 0.1f;
        }

        //############################################################################################
        // PRIVATE UNITY METHODS
        //############################################################################################
        private void OnEnable()
        {
            SubscribeGIS();
        }

        private void OnDisable()
        {
            UnsubscribeGIS();
        }

        private void FixedUpdate()
        {
            UpdatePlayerForces();
        }

        //############################################################################################
        // PRIVATE METHODS
        //############################################################################################
        private void SetupGIS()
        {
            _gis = new GIS();
            _gis.Player.Look.performed += OnLook;
            _gis.Player.Move.performed += ctx => _moveInput = ctx.ReadValue<Vector2>();
            _gis.Player.Move.canceled += ctx => _moveInput = Vector2.zero;
            _gis.Player.Sprint.performed += ctx => _sprintEnabled = true;
            _gis.Player.Sprint.canceled += ctx => _sprintEnabled = false;
            _gis.Player.Jump.performed += ctx => _jumpEnabled = true;
            _gis.Player.Shot.performed += ctx => _weaponController.Fire = true;
            _gis.Player.Shot.canceled += ctx => _weaponController.Fire = false;
            _gis.Enable();
        }

        private void OnLook(InputAction.CallbackContext context)
        {
            Vector2 lookInput = context.ReadValue<Vector2>() * _lookSensitivity;
            // x
            _lookDirection.x += lookInput.x;
            _playerRigidbody.MoveRotation(Quaternion.Euler(0f, _lookDirection.x, 0f));
            //transform.Rotate(Vector3.up * lookInput.x);
            // y
            _lookDirection.y -= lookInput.y;
            _lookDirection.y = Mathf.Clamp(_lookDirection.y, _lookMinPitch, _lookMaxPitch);
            _lookSpot.transform.localRotation = Quaternion.Euler(_lookDirection.y, 0f, 0f);
        }

        private void SetupLookSpot()
        {
            _lookSpot.localPosition = new Vector3(0f, -_playerCapsuleCollider.height * 0.5f + _lookSpotHeight, 0f);
            _lookSpot.localRotation = Quaternion.identity;
        }

        private void SetupLookCamera()
        {
            _lookCamera.transform.SetParent(_lookSpot);
            _lookCamera.transform.localPosition = Vector3.zero;
            _lookCamera.transform.localRotation = Quaternion.identity;
        }

        private void SubscribeGIS()
        {
            _gis?.Enable();
        }

        private void UnsubscribeGIS()
        {
            _gis?.Disable();
        }

        private void UpdatePlayerForces()
        {
            // check ground
            _grounded = Physics.Raycast(transform.position, Vector3.down, _groundedRayDistance);

            if (_moveInput == Vector2.zero)
            {
                // stop on ground
                if (_grounded)
                    _playerRigidbody.linearVelocity = new Vector3(0f, _playerRigidbody.linearVelocity.y, 0f);
            }
            else
            {
                // move
                Vector3 targetDirection = (_lookSpot.transform.forward * _moveInput.y + _lookSpot.transform.right * _moveInput.x);
                targetDirection.y = 0f;
                targetDirection = targetDirection.normalized;
                float acceleration = (_grounded ? (_sprintEnabled ? _sprintSpeed : _walkSpeed) : _walkSpeed * _airMultiplier) * _accelerateMultiplier;
                _playerRigidbody.AddForce(targetDirection * acceleration, ForceMode.Force);
            }

            // jump
            if (_grounded && _jumpEnabled)
            {
                _playerRigidbody.AddForce(Vector3.up * _jumpPower, ForceMode.Impulse);
                _jumpWithSprint = _sprintEnabled;
            }
            _jumpEnabled = false;

            if (_playerRigidbody.linearVelocity.y < 0f)
            {

                _playerRigidbody.AddForce(Vector3.up * Physics.gravity.y * 1.5f, ForceMode.Acceleration);
            }

            // speed limit
            float speedLimit = _grounded ? (_sprintEnabled ? _sprintSpeed : _walkSpeed) : (_jumpWithSprint ? _sprintSpeed : _walkSpeed);
            Vector3 currentSpeed = new Vector3(_playerRigidbody.linearVelocity.x, 0f, _playerRigidbody.linearVelocity.z);
            if (currentSpeed.magnitude > speedLimit)
            {
                currentSpeed = currentSpeed.normalized * speedLimit;
                _playerRigidbody.linearVelocity = new Vector3(currentSpeed.x, _playerRigidbody.linearVelocity.y, currentSpeed.z);
            }
        }
    }
}