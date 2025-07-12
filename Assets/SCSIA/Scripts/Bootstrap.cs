using Assets.SCSIA.Scripts.Player;
using UnityEngine;

namespace Assets.SCSIA.Scripts
{
    internal class Bootstrap : MonoBehaviour
    {
        [SerializeField] PlayerController _playerController;

        private void Awake()
        {
            _playerController.Initialize();
        }
    }
}
