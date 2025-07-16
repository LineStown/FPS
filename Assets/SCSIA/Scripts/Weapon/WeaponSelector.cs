using Assets.SCSIA.Scripts.Data;
using Assets.SCSIA.Scripts.Enums;
using Assets.SCSIA.Scripts.Weapon;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Assets.SCSIA.Scripts.Weapons
{
    public class WeaponSelector : MonoBehaviour
    {
        //############################################################################################
        // FIELDS
        //############################################################################################
        [Header("Selector Data")]
        [SerializeField] private WeaponSelectorDataModel _weaponSelectorData;
        [SerializeField] private TextMeshProUGUI _ammoText;

        private Dictionary<WeaponSlot, WeaponController> _weaponSlots;
        private Dictionary<WeaponId, WeaponDataModel> _availableWeapons;

        private WeaponSlot _currentWeaponSlot;

        //############################################################################################
        // PUBLIC METHODS
        //############################################################################################
        public void Initialize()
        {
            CreateWeaponSlots();
            CreateAvailableWeapons();
        }

        public void SpawnWeapon(WeaponId id)
        {
            if (_availableWeapons.ContainsKey(id) && _weaponSlots[_availableWeapons[id].Slot] == null)
            {
                _weaponSlots[_availableWeapons[id].Slot] = Instantiate(_availableWeapons[id].Controller, transform);
                _weaponSlots[_availableWeapons[id].Slot].Init(_availableWeapons[id], _ammoText);
                SwitchWeaponSlot(_availableWeapons[id].Slot);
            }
            else
                Debug.LogError($"Failed to spawn Weapon {id}. Weapon is not available");
        }

        public void SwitchWeaponSlot(WeaponSlot newWeaponSlot)
        {
            if (!_weaponSlots.ContainsKey(newWeaponSlot) || _currentWeaponSlot == newWeaponSlot || _weaponSlots[newWeaponSlot] == null)
                return;
            foreach (var weaponController in _weaponSlots.Values)
                if (weaponController != null)
                    weaponController.gameObject.SetActive(false);
            _weaponSlots[newWeaponSlot].gameObject.SetActive(true);
            _currentWeaponSlot = newWeaponSlot;
        }
        public void StartFire()
        {
            _weaponSlots[_currentWeaponSlot].StartFire();
        }

        public void StopFire()
        {
            _weaponSlots[_currentWeaponSlot].StopFire();
        }

        public void SwitchFireMode()
        {
            _weaponSlots[_currentWeaponSlot].SwitchFireMode();
        }

        public void Reload()
        {
            _weaponSlots[_currentWeaponSlot].Reload();
        }

        public void BuyAmmo()
        {
            _weaponSlots[_currentWeaponSlot].BuyAmmo();
        }

        //############################################################################################
        // PRIVATE METHODS
        //############################################################################################
        private void CreateWeaponSlots()
        {
            if(_weaponSelectorData.AvailableSlots.Count == 0)
            {
                Debug.LogError("Failed to create WeaponSlots. Configuration is empty");
                return;
            }
            _weaponSlots = new Dictionary<WeaponSlot, WeaponController>();
            foreach (WeaponSlot slot in _weaponSelectorData.AvailableSlots)
                _weaponSlots[slot] = null;
            SwitchWeaponSlot(_weaponSelectorData.AvailableSlots[0]);
        }

        private void CreateAvailableWeapons()
        {
            _availableWeapons = new Dictionary<WeaponId, WeaponDataModel>();
            foreach (var weapon in _weaponSelectorData.AvalaibleWeapons)
            {
                if (_weaponSlots.ContainsKey(weapon.Slot))
                    _availableWeapons[weapon.Id] = weapon;
                else
                    Debug.LogError($"Failed to add Weapon {weapon.Id}. Slot {weapon.Slot} is not available");
            }
        }
    }
}
