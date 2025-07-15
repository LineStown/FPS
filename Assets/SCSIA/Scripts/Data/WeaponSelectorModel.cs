using Assets.SCSIA.Scripts.Enums;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.SCSIA.Scripts.Data
{
    [CreateAssetMenu(fileName = "WeaponSelectorData", menuName = "Scriptable Objects/WeaponSelectorDataModel")]
    public class WeaponSelectorDataModel : ScriptableObject
    {
        //############################################################################################
        // FIELDS
        //############################################################################################
        [Header("Ammo Rigidbody")]
        [SerializeField] private Rigidbody _ammoRigidbody;

        [Header("Available Weapon Slots")]
        [SerializeField] private List<WeaponSlot> _availableSlots;

        [Header("Available Weapons")]
        [SerializeField] private List<WeaponDataModel> _avalaibleWeapons;

        //############################################################################################
        // PROPERTIES
        //############################################################################################
        public Rigidbody AmmoRigidbody => _ammoRigidbody;

        public IReadOnlyList<WeaponSlot> AvailableSlots => _availableSlots;
        public IReadOnlyList<WeaponDataModel> AvalaibleWeapons => _avalaibleWeapons;
    }
}
