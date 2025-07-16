using Assets.SCSIA.Scripts.Enums;
using Assets.SCSIA.Scripts.Weapon;
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
        [Header("Available Weapon Slots")]
        [SerializeField] private List<WeaponSlot> _availableSlots;

        [Header("Available Weapons")]
        [SerializeField] private List<WeaponDataModel> _avalaibleWeapons;

        //############################################################################################
        // PROPERTIES
        //############################################################################################
        public IReadOnlyList<WeaponSlot> AvailableSlots => _availableSlots;
        public IReadOnlyList<WeaponDataModel> AvalaibleWeapons => _avalaibleWeapons;
    }
}
