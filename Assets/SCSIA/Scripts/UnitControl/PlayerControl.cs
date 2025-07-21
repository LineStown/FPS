using Assets.SCSIA.Scripts.Input;
using Assets.SCSIA.Scripts.Unit;
using Assets.SCSIA.Scripts.Weapons;
using UnityEngine;

namespace Assets.SCSIA.Scripts.UnitControl
{
    public class PlayerControl : MonoBehaviour
    {
        //############################################################################################
        // FIELDS
        //############################################################################################
        [SerializeField] private UnitController _unitController;
        [SerializeField] private WeaponSelector _weaponSelector;
        private GIS _gis;

        //############################################################################################
        // PROPERTIES
        //############################################################################################
        public WeaponSelector Selector => _weaponSelector;

        //############################################################################################
        // PUBLIC METHODS
        //############################################################################################
        public void Init()
        {
            _unitController.Init();
            _weaponSelector.Init();
            SetupGIS();
        }

        //############################################################################################
        // PRIVATE UNITY METHODS
        //############################################################################################
        private void OnEnable()
        {
            EnableGIS();
        }

        private void OnDisable()
        {
            DisabeGIS();
        }

        //############################################################################################
        // PRIVATE METHODS
        //############################################################################################
        private void SetupGIS()
        {
            _gis = new GIS();
            // unit controller
            _gis.Player.Look.performed += ctx => _unitController.OnLookInput(ctx.ReadValue<Vector2>());
            _gis.Player.Move.performed += ctx => _unitController.OnMoveInput(ctx.ReadValue<Vector2>());
            _gis.Player.Move.canceled += ctx => _unitController.OnMoveInput(Vector2.zero);
            _gis.Player.Sprint.performed += ctx => _unitController.OnSprint(true);
            _gis.Player.Sprint.canceled += ctx => _unitController.OnSprint(false);
            _gis.Player.Jump.performed += ctx => _unitController.OnJump();

            // weapon selector
            _gis.Player.Shot.performed += ctx => _weaponSelector.StartFire();
            _gis.Player.Shot.canceled += ctx => _weaponSelector.StopFire();
            _gis.Player.SwitchFireMode.performed += ctx => _weaponSelector.SwitchFireMode();
            _gis.Player.Reload.performed += ctx => _weaponSelector.Reload();
            _gis.Player.PrimarySlot.performed += ctx => _weaponSelector.SwitchWeaponSlot(Enums.WeaponSlot.Primary);
            _gis.Player.SecondarySlot.performed += ctx => _weaponSelector.SwitchWeaponSlot(Enums.WeaponSlot.Secondary);
            _gis.Player.PistolSlot.performed += ctx => _weaponSelector.SwitchWeaponSlot(Enums.WeaponSlot.Pistol);
            _gis.Player.BuyAmmo.performed += ctx => _weaponSelector.BuyAmmo();
            _gis.Player.PickupWeapon.performed += ctx => _weaponSelector.PickupWeapon();
            _gis.Player.DropWeapon.performed += ctx => _weaponSelector.DropWeapon();
        }

        private void EnableGIS()
        {
            _gis?.Enable();
        }

        private void DisabeGIS()
        {
            _gis?.Disable();
        }
    }
}
