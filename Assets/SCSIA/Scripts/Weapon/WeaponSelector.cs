using System;
using System.Collections.Generic;
using System.Linq;
using Assets.SCSIA.Scripts.Data;
using Assets.SCSIA.Scripts.Enums;
using Assets.SCSIA.Scripts.Weapon;
using UnityEngine;

namespace Assets.SCSIA.Scripts.Weapons
{
    public class WeaponSelector : MonoBehaviour
    {
        //############################################################################################
        // FIELDS
        //############################################################################################
        [Header("Selector Data")]
        [SerializeField] private Camera _camera;
        [SerializeField] private WeaponSelectorDataModel _weaponSelectorData;
        [SerializeField] private AudioSource _audioSource;

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
            if (_availableWeapons.ContainsKey(id))
            {
                _weaponSlots[_availableWeapons[id].Slot].SpawnWeapon(_availableWeapons[id]);
                SwitchWeaponSlot(_availableWeapons[id].Slot);
            }
            else
                Debug.LogError($"Failed to spawn Weapon {id}. Weapon is not available");
        }

        public void SwitchWeaponSlot(WeaponSlot slot)
        {
            if (_weaponSlots.ContainsKey(slot) && _currentWeaponSlot != slot)
            {
                foreach (var weaponRuntime in _weaponSlots.Values)
                    weaponRuntime.gameObject.SetActive(false);
                _currentWeaponSlot = slot;
                _weaponSlots[_currentWeaponSlot].gameObject.SetActive(true);
            }
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
            {
                _weaponSlots[slot] = new GameObject(slot.ToString()).AddComponent<WeaponController>();
                _weaponSlots[slot].transform.SetParent(this.transform, false);
                _weaponSlots[slot].Initialize(_camera, _weaponSelectorData.AmmoRigidbody, _audioSource);
            }
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
