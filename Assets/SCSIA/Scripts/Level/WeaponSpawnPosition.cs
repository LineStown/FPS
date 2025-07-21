using Assets.SCSIA.Scripts.Enums;
using System;
using UnityEngine;

namespace Assets.SCSIA.Scripts.Level
{
    [Serializable]
    public class WeaponSpawnPosition
    {
        //############################################################################################
        // FIELDS
        //############################################################################################
        [SerializeField] private Transform _position;
        [SerializeField] private WeaponId _weaponId;

        //############################################################################################
        // PROPERTIES
        //############################################################################################
        public Transform Position => _position;
        public WeaponId WeaponId => _weaponId;
    }
}
