using UnityEngine;

namespace Assets.SCSIA.Scripts.Actions
{
    internal class SpawnOnCollisionEnter : MonoBehaviour
    {
        //############################################################################################
        // FIELDS
        //############################################################################################
        [SerializeField] private GameObject _gameObject;
        [SerializeField] private bool _invertNormal;

        //############################################################################################
        // PRIVATE UNITY METHODS
        //############################################################################################
        private void OnCollisionEnter(Collision collision)
        {
            ContactPoint contactPoint = collision.contacts[0];
            Vector3 decalPosition = contactPoint.point;
            Quaternion rotation = Quaternion.LookRotation(_invertNormal ? -contactPoint.normal : contactPoint.normal);
            Instantiate(_gameObject, decalPosition, rotation, collision.transform);
        }
    }
}
