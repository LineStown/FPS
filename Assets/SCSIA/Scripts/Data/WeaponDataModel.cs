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
        [Header("Weapon")]
        [SerializeField] private WeaponId _id;
        [SerializeField] private WeaponSlot _slot;
        [SerializeField] private WeaponController _controller;

        [Header("Weapon Parameters")]
        [SerializeField] private float _fireRate;
        [SerializeField] private float _maxDistance;
        [SerializeField] private float _deltaSpread;
        [SerializeField] private float _maxSpread;
        [SerializeField] private int _magCapacity;
        [SerializeField] private int _totalCapacity;
        [SerializeField] private float _reloadTime;

        [Header("Weapon Fire Mode Config")]
        [SerializeField] private List<WeaponFireModeConfig> _fireModeConfig;

        [Header("Weapon Ammo Config")]
        [SerializeField] private WeaponAmmoConfig _ammoConfig;

        [Header("Audio")]
        [SerializeField] private AudioClip _fireSound;
        [SerializeField] private AudioClip _emptySound;
        [SerializeField] private AudioClip _reloadSound;
        [SerializeField] private AudioClip _switchFireModeSound;

        //############################################################################################
        // PROPERTIES
        //############################################################################################
        // Weapon
        public WeaponId Id => _id;
        public WeaponSlot Slot => _slot;
        public WeaponController Controller => _controller;

        // Weapon Fire Mode Config
        public float FireRate => _fireRate;
        public float MaxDistance => _maxDistance;
        public float DeltaSpread => _deltaSpread;
        public float MaxSpread => _maxSpread;
        public int MagCapacity => _magCapacity;
        public int TotalCapacity => _totalCapacity;
        public float ReloadTime => _reloadTime;

        public IReadOnlyList<WeaponFireModeConfig> FireModeConfig => _fireModeConfig;

        // Weapon Ammo Config
        public WeaponAmmoConfig AmmoConfig => _ammoConfig;

        // Audio
        public AudioClip FireSound => _fireSound;
        public AudioClip EmptySound => _emptySound;
        public AudioClip ReloadSound => _reloadSound;
        public AudioClip SwitchFireModeSound => _switchFireModeSound;
    }
}
