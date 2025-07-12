using UnityEngine;

namespace Assets.SCSIA.Scripts.Actions
{
    internal class SpawnOnCollisionEnter : MonoBehaviour
    {
        [SerializeField] private GameObject _gameObject;
        [SerializeField] private bool _invertNormal;

        private void OnCollisionEnter(Collision collision)
        {
            ContactPoint contactPoint = collision.contacts[0];
            Vector3 decalPosition = contactPoint.point + contactPoint.normal * 0.001f;
            Quaternion rotation = Quaternion.LookRotation(_invertNormal ? -contactPoint.normal : contactPoint.normal);
            Instantiate(_gameObject, decalPosition, rotation);
        }
    }
}
