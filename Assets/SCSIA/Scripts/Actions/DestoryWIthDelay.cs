using UnityEngine;

namespace Assets.SCSIA.Scripts.Actions
{
    internal class DestroyWithDelay : MonoBehaviour
    {
        [SerializeField] private float _delay;

        private void Awake()
        {
            Destroy(gameObject, _delay);
        }
    }
}
