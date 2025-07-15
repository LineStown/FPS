using System.Collections.Generic;
using Assets.SCSIA.Scripts.Enums;
using Assets.SCSIA.Scripts.Weapon;
using UnityEngine;

namespace Assets.SCSIA.Scripts.Data
{
    [CreateAssetMenu(fileName = "WeaponData", menuName = "Scriptable Objects/WeaponDataModel")]
    public class WeaponDataModel : ScriptableObject
    {
        //############################################################################################
        // FIELDS
        //############################################################################################
        [Header("Weapon Identificator")]
        [SerializeField] private WeaponId _id;
        [SerializeField] private WeaponSlot _slot;

        [Header("Weapon Fire Mode Config")]
        [SerializeField] private List<WeaponFireModeConfig> _fireModeConfig;

        [Header("Weapon Ammo Config")]
        [SerializeField] private WeaponAmmoConfig _ammoConfig;
       
        [Header("View")]
        [SerializeField] private WeaponView _view;

        [Header("Audio")]
        [SerializeField] private AudioClip _fireSound;
        [SerializeField] private AudioClip _reloadSound;
        [SerializeField] private AudioClip _switchFireModeSound;

        //############################################################################################
        // PROPERTIES
        //############################################################################################
        public WeaponId Id => _id;
        public WeaponSlot Slot => _slot;

        public IReadOnlyList<WeaponFireModeConfig> FireModeConfig => _fireModeConfig;

        public WeaponAmmoConfig AmmoConfig => _ammoConfig;

        public WeaponView View => _view;

        public AudioClip FireSound => _fireSound;
        public AudioClip ReloadSound => _reloadSound;
        public AudioClip SwitchFireModeSound => _switchFireModeSound;
    }
}
