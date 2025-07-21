using System.Collections.Generic;
using UnityEngine;

namespace Assets.SCSIA.Scripts.Core
{
    public class PickupableItem : MonoBehaviour
    {
        [SerializeField] private List<Collider> _itemColliders;
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private float _dropForce;

        public void PickupItem()
        {
            foreach(Collider collider in _itemColliders)
                collider.enabled = false;
            _rigidbody.isKinematic = true;
        }

        public void DropItem(Vector3 direction)
        {
            foreach (Collider collider in _itemColliders)
                collider.enabled = true;
            _rigidbody.isKinematic = false;
            _rigidbody.AddForce(direction * _dropForce, ForceMode.Impulse);
        }
    }
}
