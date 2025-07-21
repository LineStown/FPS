using Assets.SCSIA.Scripts.Enums;
using Assets.SCSIA.Scripts.Weapon;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.SCSIA.Scripts.Weapons
{
    public class WeaponSelector : MonoBehaviour
    {
        //############################################################################################
        // FIELDS
        //############################################################################################
        [Header("Selector Data")]
        [SerializeField] private List<WeaponSlot> _availableSlots;
        [SerializeField] private Transform _weaponSpot;
        [SerializeField] private float _pickupDistance;

        private Dictionary<WeaponSlot, WeaponController> _weaponSlots;
        private WeaponSlot _currentWeaponSlot;

        //############################################################################################
        // PUBLIC METHODS
        //############################################################################################
        public void Init()
        {
            CreateWeaponSlots();
        }

        public void AttachWeapon(WeaponController weapon)
        {
            if (_currentWeaponSlot == weapon.Slot)
                DropWeapon();
            weapon.gameObject.transform.SetParent(_weaponSpot, false);
            weapon.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
            _weaponSlots[weapon.Slot] = weapon;
            SwitchWeaponSlot(weapon.Slot);
        }

        public WeaponController DetachWeapon()
        {
            if (_weaponSlots[_currentWeaponSlot] == null)
                return null;
            var weapon = _weaponSlots[_currentWeaponSlot];
            weapon.transform.SetParent(null);
            _weaponSlots[_currentWeaponSlot] = null;
            return weapon;
        }

        public void PickupWeapon()
        {
            Ray ray = new Ray(_weaponSpot.position, _weaponSpot.forward);
            Debug.DrawRay(ray.origin, ray.direction * _pickupDistance, Color.green);
            if (Physics.Raycast(ray, out var hit, _pickupDistance))
                if (hit.transform.TryGetComponent(out WeaponController weaponController))
                {
                    weaponController.PickupItem();
                    AttachWeapon(weaponController);
                }
        }

        public void DropWeapon()
        {
            if (_weaponSlots[_currentWeaponSlot] == null)
                return;
            var weapon = (_weaponSlots[_currentWeaponSlot]);
            DetachWeapon();
            weapon.transform.Rotate(Random.Range(0,360), Random.Range(0, 360), Random.Range(0, 360));
            weapon.DropItem(_weaponSpot.forward);
        }

        public void SwitchWeaponSlot(WeaponSlot slot)
        {
            if (!_weaponSlots.ContainsKey(slot) || _currentWeaponSlot == slot || _weaponSlots[slot] == null)
                return;
            foreach (var weaponController in _weaponSlots.Values)
                if (weaponController != null)
                    weaponController.gameObject.SetActive(false);
            _weaponSlots[slot].gameObject.SetActive(true);
            _currentWeaponSlot = slot;
        }
        public void StartFire()
        {
            _weaponSlots[_currentWeaponSlot]?.StartFire();
        }

        public void StopFire()
        {
            _weaponSlots[_currentWeaponSlot]?.StopFire();
        }

        public void SwitchFireMode()
        {
            _weaponSlots[_currentWeaponSlot]?.SwitchFireMode();
        }

        public void Reload()
        {
            _weaponSlots[_currentWeaponSlot]?.Reload();
        }

        public void BuyAmmo()
        {
            _weaponSlots[_currentWeaponSlot]?.BuyAmmo();
        }

        //############################################################################################
        // PRIVATE METHODS
        //############################################################################################
        private void CreateWeaponSlots()
        {
            if (_availableSlots.Count == 0)
            {
                Debug.LogError("WeaponSelector. Failed to create WeaponSlots. Configuration is empty");
                return;
            }
            _weaponSlots = new Dictionary<WeaponSlot, WeaponController>();
            foreach (WeaponSlot slot in _availableSlots)
                _weaponSlots[slot] = null;
            SwitchWeaponSlot(_availableSlots[0]);
        }
    }
}
