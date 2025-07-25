using Assets.SCSIA.Scripts.Level;
using Assets.SCSIA.Scripts.UnitControl;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.SCSIA.Scripts
{
    public class Bootstrap : MonoBehaviour
    {
        [SerializeField] private LevelController _levelController;
        [SerializeField] private PlayerControl _playerControl;
        [SerializeField] private List<Transform> _spawnPositions;

        private void Awake()
        {
            _levelController.Init();
            _levelController.SpawnWeaponOnPosision();
            _playerControl.Init();
        }
    }
}
