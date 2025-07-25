using Assets.SCSIA.Scripts.DataModel;
using UnityEngine;

namespace Assets.SCSIA.Scripts.Unit
{
    public class UnitController : MonoBehaviour
    {
        //############################################################################################
        // FIELDS
        //############################################################################################
        [Header("Unit Data")]
        [SerializeField] private UnitDataModel _unitData;
        [Header("Unit Settings")]
        [SerializeField] private Camera _lookCamera;
        [SerializeField] private Transform _lookSpot;
        //[SerializeField] private Transform _
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private CapsuleCollider _capsuleCollider;

        private Vector2 _lookInput;
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
            _lookInput = Vector2.zero;
            _moveInput = Vector2.zero;
            _sprintEnabled = false;
            _jumpEnabled = false;
            _jumpWithSprint = false;
            _grounded = false;
            _groundedRayDistance = _capsuleCollider.height * 0.5f + _capsuleCollider.height * 0.1f;

            _lookSpot.position = new Vector3(0, -_capsuleCollider.height * 0.5f + _unitData.LookHeight, 0);
            _lookSpot.rotation = Quaternion.identity;

            _lookCamera.transform.SetParent(_lookSpot);
            _lookCamera.transform.localPosition = Vector3.zero;
            _lookCamera.transform.localRotation = Quaternion.identity;
        }

        public void OnLookInput(Vector2 lookInput)
        {
            lookInput *= _unitData.LookSensitivity;
            _lookInput.x += lookInput.x;
            _lookInput.y -= lookInput.y;
            _lookInput.y = Mathf.Clamp(_lookInput.y, _unitData.LookMinPitch, _unitData.LookMaxPitch);
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
        private void FixedUpdate()
        {
            CheckGround();
            MoveAndRotateUnit();
            JumpUnit();
            ClampUnitVelocity();
        }

        private void LateUpdate()
        {
            UpdateLookSpot();
        }

        //############################################################################################
        // PRIVATE METHODS
        //############################################################################################
        private void CheckGround()
        {
            _grounded = Physics.Raycast(transform.position, Vector3.down, _groundedRayDistance);
        }

        private void MoveAndRotateUnit()
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
                _rigidbody.AddForce(targetDirection * acceleration, ForceMode.Acceleration);
            }
            // rotate
            _rigidbody.MoveRotation(Quaternion.Euler(0f, _lookInput.x, 0f));
        }

        private void JumpUnit()
        {
            if (_grounded && _jumpEnabled)
            {
                _rigidbody.AddForce(Vector3.up * _unitData.JumpPower, ForceMode.Impulse);
                _jumpWithSprint = _sprintEnabled;
            }
            _jumpEnabled = false;
        }

        private void ClampUnitVelocity()
        {
            float maxSpeed = _grounded ? (_sprintEnabled ? _unitData.SprintSpeed : _unitData.WalkSpeed) : (_jumpWithSprint ? _unitData.SprintSpeed : _unitData.WalkSpeed);
            Vector3 currentSpeed = new Vector3(_rigidbody.linearVelocity.x, 0f, _rigidbody.linearVelocity.z);
            if (currentSpeed.magnitude > maxSpeed)
            {
                currentSpeed = currentSpeed.normalized * maxSpeed;
                _rigidbody.linearVelocity = new Vector3(currentSpeed.x, _rigidbody.linearVelocity.y, currentSpeed.z);
            }
        }

        private void UpdateLookSpot()
        {
            _lookSpot.position = new Vector3(_rigidbody.transform.position.x, _rigidbody.transform.position.y + -_capsuleCollider.height * 0.5f + _unitData.LookHeight, _rigidbody.transform.position.z);
            _lookSpot.rotation = Quaternion.Euler(_lookInput.y, _lookInput.x, 0f);
        }
    }
}
