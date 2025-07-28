using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
namespace Assets.SCSIA.Scripts.Unit
{
    public class NMAgentController : MonoBehaviour
    {
        //############################################################################################
        // FIELDS
        //############################################################################################
        [SerializeField] private List<Transform> _points;
        [SerializeField] private float _threshold;
        [SerializeField] private NavMeshAgent _agent;
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private LayerMask _bulletLayer;

        private Transform _nextPoint;
        private bool _killed = false;

        //############################################################################################
        // PRIVATE UNITY METHODS
        //############################################################################################
        private void Start()
        {
            SelectRandonPointAndGo();
        }

        private void Update()
        {
            CheckAgent();
        }

        private void OnCollisionEnter(Collision collision)
        {
            if(!_killed && (_bulletLayer & (1 << collision.gameObject.layer)) != 0)
                GotBullet();
        }

        //############################################################################################
        // PRIVATE METHODS
        //############################################################################################
        private void CheckAgent()
        {
            if (_agent.remainingDistance < _threshold)
            {
                SelectRandonPointAndGo();
            }
        }

        private void SelectRandonPointAndGo()
        {
            _nextPoint = _points[Random.Range(0, _points.Count)];
            _agent.SetDestination(_nextPoint.position);
        }

        private void GotBullet()
        {
            _agent.enabled = false;
            _rigidbody.isKinematic = false;
            _rigidbody.constraints = RigidbodyConstraints.None;
            _killed = true;
        }
    }
}
