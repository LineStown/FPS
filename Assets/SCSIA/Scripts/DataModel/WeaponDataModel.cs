using Assets.SCSIA.Scripts.Enums;
using Assets.SCSIA.Scripts.Weapon;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.SCSIA.Scripts.DataModel
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

        [Header("Weapon Parameters")]
        [SerializeField] private float _fireRate;
        [SerializeField] private float _maxDistance;
        [SerializeField] private float _deltaSpread;
        [SerializeField] private float _maxSpread;
        [SerializeField] private int _magCapacity;
        [SerializeField] private int _totalCapacity;
        [SerializeField] private float _reloadTime;
        [SerializeField] private float _ammoSpeed;

        [Header("Weapon Fire Mode Config")]
        [SerializeField] private List<WeaponFireModeConfig> _fireModeConfig;

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

        // Weapon Fire Mode Config
        public float FireRate => _fireRate;
        public float MaxDistance => _maxDistance;
        public float DeltaSpread => _deltaSpread;
        public float MaxSpread => _maxSpread;
        public int MagCapacity => _magCapacity;
        public int TotalCapacity => _totalCapacity;
        public float ReloadTime => _reloadTime;
        public float AmmoSpeed => _ammoSpeed;

        public IReadOnlyList<WeaponFireModeConfig> FireModeConfig => _fireModeConfig;

        // Audio
        public AudioClip FireSound => _fireSound;
        public AudioClip EmptySound => _emptySound;
        public AudioClip ReloadSound => _reloadSound;
        public AudioClip SwitchFireModeSound => _switchFireModeSound;
    }
}
