using UnityEngine;

namespace Assets.SCSIA.Scripts.Actions
{
    internal class DestroyOnCollisionEnter : MonoBehaviour
    {
        [SerializeField] private float _delay;

        private void OnCollisionEnter(Collision collision)
        {
            Destroy(gameObject, _delay);
        }
    }
}
