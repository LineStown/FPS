using UnityEngine;

namespace Assets.SCSIA.Scripts.Weapon
{
    public class WeaponView : MonoBehaviour
    {
        //############################################################################################
        // FIELDS
        //############################################################################################
        [SerializeField] private Transform _ammoSpawnPoint;

        //############################################################################################
        // PROPERTIES
        //############################################################################################
        public Transform AmmoSpawnPoint => _ammoSpawnPoint;
    }
}
