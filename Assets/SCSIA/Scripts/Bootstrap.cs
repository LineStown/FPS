using Assets.SCSIA.Scripts.Enums;
using Assets.SCSIA.Scripts.Player;
using Assets.SCSIA.Scripts.Weapons;
using UnityEngine;

namespace Assets.SCSIA.Scripts
{
    public class Bootstrap : MonoBehaviour
    {
        [SerializeField] PlayerController _playerController;
        [SerializeField] WeaponSelector _weaponSelector;

        private void Awake()
        {
            _playerController.Initialize();
            _weaponSelector.Initialize();
            _weaponSelector.SpawnWeapon(WeaponId.M4);
        }
    }
}
