using Assets.SCSIA.Scripts.DataModel;
using UnityEngine;

namespace Assets.SCSIA.Scripts.Unit
{
    public class UnitController : MonoBehaviour
    {
        //############################################################################################
        // FIELDS
        //############################################################################################
        [Header("Character Data")]
        [SerializeField] private UnitDataModel _unitData;
        [Header("Character Settings")]
        [SerializeField] private Camera _lookCamera;
        [SerializeField] private Transform _lookSpot;
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private CapsuleCollider _capsuleCollider;

        private Vector2 _lookDirection;
        private Vector2 _moveInput;
        private bool _sprintEnabled;
        private bool _jumpEnabled;
        private bool _jumpWithSprint;
        private bool _grounded;
        private float _groundedRayDistance;

        //############################################################################################
        // PUBLIC METHODS
        //############################################################################################
        public void Init()
        {
            _lookDirection = Vector2.zero;
            _moveInput = Vector2.zero;
            _sprintEnabled = false;
            _jumpEnabled = false;
            _jumpWithSprint = false;
            _grounded = false;
            _groundedRayDistance = _capsuleCollider.height * 0.5f + _capsuleCollider.height * 0.1f;

            SetupLookSpot();
            SetupLookCamera();
        }

        public void OnLookInput(Vector2 lookInput)
        {
            lookInput *= _unitData.LookSensitivity;
            // x
            _lookDirection.x += lookInput.x;
            _rigidbody.MoveRotation(Quaternion.Euler(0f, _lookDirection.x, 0f));
            // y
            _lookDirection.y -= lookInput.y;
            _lookDirection.y = Mathf.Clamp(_lookDirection.y, _unitData.LookMinPitch, _unitData.LookMaxPitch);
            _lookSpot.transform.localRotation = Quaternion.Euler(_lookDirection.y, 0f, 0f);
        }

        public void OnMoveInput(Vector2 moveInput)
        {
            _moveInput = moveInput;
        }

        public void OnSprint(bool value)
        {
            _sprintEnabled = value;
        }

        public void OnJump()
        {
            _jumpEnabled = true;
        }

        //############################################################################################
        // PRIVATE UNITY METHODS
        //############################################################################################
        private void Update()
        {
            CheckUnitGround();
            MoveUnit();
            JumpUnit();
            ClampUnitVelocity();
        }

        //############################################################################################
        // PRIVATE METHODS
        //############################################################################################
        private void SetupLookSpot()
        {
            _lookSpot.localPosition = new Vector3(0, -_capsuleCollider.height * 0.5f + _unitData.LookHeight, 0);
            _lookSpot.localRotation = Quaternion.identity;
        }

        private void SetupLookCamera()
        {
            _lookCamera.transform.SetParent(_lookSpot);
            _lookCamera.transform.localPosition = Vector3.zero;
            _lookCamera.transform.localRotation = Quaternion.identity;
        }

        private void CheckUnitGround()
        {
            _grounded = Physics.Raycast(transform.position, Vector3.down, _groundedRayDistance);
        }

        private void MoveUnit()
        {
            
            if (_moveInput == Vector2.zero)
            {
                // stop on ground
                if (_grounded)
                    _rigidbody.linearVelocity = new Vector3(0f, _rigidbody.linearVelocity.y, 0f);
            }
            else
            {
                // move
                Vector3 targetDirection = (_lookSpot.transform.forward * _moveInput.y + _lookSpot.transform.right * _moveInput.x);
                targetDirection.y = 0f;
                targetDirection = targetDirection.normalized;
                float acceleration = (_grounded ? (_sprintEnabled ? _unitData.SprintSpeed : _unitData.WalkSpeed) : _unitData.WalkSpeed * _unitData.AirMultiplie) * _unitData.AccelerateMultiplier;
                _rigidbody.AddForce(targetDirection * acceleration, ForceMode.Force);
            }
        }

        private void JumpUnit()
        {
            if (_grounded && _jumpEnabled)
            {
                _rigidbody.AddForce(Vector3.up * _unitData.JumpPower, ForceMode.Impulse);
                _jumpWithSprint = _sprintEnabled;
            }
            _jumpEnabled = false;

            if (_rigidbody.linearVelocity.y < 0f)
            {

                _rigidbody.AddForce(Vector3.up * Physics.gravity.y * 1.5f, ForceMode.Acceleration);
            }
        }

        private void ClampUnitVelocity()
        {
            float speedLimit = _grounded ? (_sprintEnabled ? _unitData.SprintSpeed : _unitData.WalkSpeed) : (_jumpWithSprint ? _unitData.SprintSpeed : _unitData.WalkSpeed);
            Vector3 currentSpeed = new Vector3(_rigidbody.linearVelocity.x, 0f, _rigidbody.linearVelocity.z);
            if (currentSpeed.magnitude > speedLimit)
            {
                currentSpeed = currentSpeed.normalized * speedLimit;
                _rigidbody.linearVelocity = new Vector3(currentSpeed.x, _rigidbody.linearVelocity.y, currentSpeed.z);
            }
        }
    }
}
