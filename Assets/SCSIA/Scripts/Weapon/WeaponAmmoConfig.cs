using System;
using UnityEngine;

namespace Assets.SCSIA.Scripts.Weapon
{
    [Serializable]
    public class WeaponAmmoConfig
    {
        //############################################################################################
        // FIELDS
        //############################################################################################
        [SerializeField] private float _speed;
        [SerializeField] private float _maxDistance;
        [SerializeField] private int _magCapacity;
        [SerializeField] private int _totalCapacity;
        [SerializeField] private float _reloaadTime;

        //############################################################################################
        // PREPERTIES
        //############################################################################################
        public float Speed => _speed;
        public float MaxDistance => _maxDistance;
        public int MagCapacity => _magCapacity;
        public int TotalCapacity => _totalCapacity;
        public float ReloadTime => _reloaadTime;
    }
}
