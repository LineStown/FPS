using UnityEngine;

namespace Assets.SCSIA.Scripts.Weapon
{
    internal class WeaponController : MonoBehaviour
    {
        [Header("Weapon Settings")]
        [SerializeField] private float _fireSpeed;
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private AudioClip _audioClip;

        [Header("Bullet Settings")]
        [SerializeField] private Camera _camera;
        [SerializeField] private Rigidbody _bulletRigidbody;
        [SerializeField] private Transform _bulletSpawnPoint;
        [SerializeField] private float _bulletSpeed;
        [SerializeField] private float _bulletMaxDistance;

        private float _fireDelay;
        private float _fireTime;

        public bool Fire { get; set; } = false;

        private void Awake()
        {
            _fireDelay = 60f / _fireSpeed;
            _fireTime = Time.time;
        }

        private void Update()
        {
            if(Fire && Time.time > _fireTime)
            {
                Shot();
                _fireTime = Time.time + _fireDelay;
            }
        }

        private void Shot()
        {
            Ray ray = _camera.ScreenPointToRay(new Vector2(Screen.width * 0.5f, Screen.height * 0.5f));
            Debug.DrawRay(ray.origin, ray.direction * _bulletMaxDistance);
            Vector3 bulletDirection = (ray.origin + ray.direction * _bulletMaxDistance - _bulletSpawnPoint.position).normalized;
            Quaternion bulletRotation = Quaternion.LookRotation(bulletDirection);
            Rigidbody bulletInstance = Instantiate(_bulletRigidbody, _bulletSpawnPoint.position, bulletRotation);
            bulletInstance.AddForce(bulletDirection * _bulletSpeed, ForceMode.Impulse);
            _audioSource.PlayOneShot(_audioClip);
        }
    }
}
