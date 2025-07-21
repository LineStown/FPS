using Assets.SCSIA.Scripts.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.SCSIA.Scripts.DataModel
{
    public class WeaponSpawnModel : ScriptableObject
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
