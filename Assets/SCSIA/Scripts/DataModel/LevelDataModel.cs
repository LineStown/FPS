using Assets.SCSIA.Scripts.Level;
using Assets.SCSIA.Scripts.Weapon;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.SCSIA.Scripts.DataModel
{
    [CreateAssetMenu(fileName = "LevelData", menuName = "Scriptable Objects/LevelDataModel")]
    public class LevelDataModel : ScriptableObject
    {
        //############################################################################################
        // FIELDS
        //############################################################################################
        [SerializeField] private List<WeaponController> _weaponControllers;

        //############################################################################################
        // PROPERTIES
        //############################################################################################
        public IReadOnlyList<WeaponController> WeaponControllers => _weaponControllers;
    }
}
