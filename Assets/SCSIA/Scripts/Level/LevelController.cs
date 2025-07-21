using Assets.SCSIA.Scripts.DataModel;
using Assets.SCSIA.Scripts.Enums;
using Assets.SCSIA.Scripts.Weapon;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.SCSIA.Scripts.Level
{
    public class LevelController : MonoBehaviour
    {
        //############################################################################################
        // FIELDS
        //############################################################################################
        [Header("Weapon Data")]
        [SerializeField] private LevelDataModel _levelControllerData;
        [SerializeField] private List<WeaponSpawnPosition> _weaponSpawnPositions;

        private Dictionary<WeaponId, WeaponController> _weaponData;

        //############################################################################################
        // PUBLIC METHODS
        //############################################################################################
        public void Init()
        {
            LoadWeaponData();
            Cursor.lockState = CursorLockMode.Locked;
        }

        public WeaponController SpawnWeapon(WeaponId id)
        {
            if (!_weaponData.ContainsKey(id))
            {
                Debug.Log($"Weapon [id] in not allowen on this map");
                return null;
            }
            WeaponController newWeapon = Instantiate(_weaponData[id]);
            newWeapon.Init();
            return newWeapon;
        }

        public void SpawnWeaponOnPosision()
        {
            foreach (WeaponSpawnPosition wsp in _weaponSpawnPositions)
            {
                var wc = SpawnWeapon(wsp.WeaponId);
                wc.transform.position = wsp.Position.position;
                wc.transform.rotation = wsp.Position.rotation;
            }
        }

        //############################################################################################
        // PRIVATE METHODS
        //############################################################################################
        private void LoadWeaponData()
        {
            _weaponData = new Dictionary<WeaponId, WeaponController>();
            foreach (var weaponData in _levelControllerData.WeaponControllers)
                _weaponData[weaponData.Id] = weaponData;
        }

    }
}
