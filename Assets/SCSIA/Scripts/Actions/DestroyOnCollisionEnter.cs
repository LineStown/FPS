using UnityEngine;

namespace Assets.SCSIA.Scripts.Actions
{
    internal class DestroyOnCollisionEnter : MonoBehaviour
    {
        //############################################################################################
        // FIELDS
        //############################################################################################
        [SerializeField] private float _delay;

        //############################################################################################
        // PRIVATE UNITY METHODS
        //############################################################################################
        private void OnCollisionEnter(Collision collision)
        {
            Destroy(gameObject, _delay);
        }
    }
}
