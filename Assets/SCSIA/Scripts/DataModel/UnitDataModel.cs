using UnityEngine;

namespace Assets.SCSIA.Scripts.DataModel
{
    [CreateAssetMenu(fileName = "UnitData", menuName = "Scriptable Objects/UnitDataModel")]
    public class UnitDataModel : ScriptableObject
    {
        //############################################################################################
        // FIELDS
        //############################################################################################
        [Header("Look Settings")]
        [SerializeField] private float _lookHeight = 1.75f;
        [SerializeField, Range(-70, -20)] private float _lookMinPitch = -70f;
        [SerializeField, Range(20, 70)] private float _lookMaxPitch = 70f;
        [SerializeField] private float _lookSensitivity = 0.08f;

        [Header("Move Settings")]
        [SerializeField] private float _walkSpeed = 10f;
        [SerializeField] private float _sprintSpeed = 15f;
        [SerializeField] private float _moveMultiplier = 10f;

        [Header("Air Settings")]
        [SerializeField] private float _jumpPower = 7f;
        [SerializeField] private float _airMultiplier = 0.2f;
        [SerializeField] private float _gravityMultiplier = 1.5f;

        //############################################################################################
        // PROPERTIES
        //############################################################################################
        public float LookHeight => _lookHeight;
        public float LookMinPitch => _lookMinPitch;
        public float LookMaxPitch => _lookMaxPitch;
        public float LookSensitivity => _lookSensitivity;

        public float WalkSpeed => _walkSpeed;
        public float SprintSpeed => _sprintSpeed;
        public float MoveMultiplier => _moveMultiplier;

        public float JumpPower => _jumpPower;
        public float AirMultiplie => _airMultiplier;
        public float GravityMultiplier => _gravityMultiplier;
    }
}
