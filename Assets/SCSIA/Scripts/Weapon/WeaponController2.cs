using Assets.SCSIA.Scripts.Data;
using Assets.SCSIA.Scripts.Enums;
using Assets.SCSIA.Scripts.Weapons;
using UnityEngine;

namespace Assets.SCSIA.Scripts.Weapon
{
    public class WeaponController2 : MonoBehaviour
    {
        //############################################################################################
        // FIELDS
        //############################################################################################
        [Header("Weapon Data")]
        [SerializeField] private WeaponDataModel _weaponData;

        [Header("Weapon Controller Data")]
        [SerializeField] private Transform _ammoSpawnPoint;
        [SerializeField] private AudioSource _audioSource;

        private Camera _camera;
        private Rigidbody _ammoRigidbody;

        private int _magAmmo;
        private int _totalAmmo;

        private int _currentFireMode;
        private int _maxShotCount;
        private float _fireDelay;
        
        private float _nextFireReadyTime;
        private bool _firing;
        private float _nextReloadReadyTime;
        private bool _reloading;

        //############################################################################################
        // PUBLIC METHODS
        //############################################################################################
        public void Initialize(Camera camera, Rigidbody ammoRigidbody)
        {
            _camera = camera;
            _ammoRigidbody = ammoRigidbody;
            _currentFireMode = 0;
            //_magAmmo = _weaponData.MagCapacity;
            //_totalAmmo = _weaponData.TotalCapacity;
            _firing = false;
            _reloading = false;
        }

        public void StartFire()
        {
            if (_magAmmo > 0)
            {
                //_weaponData.FireModeConfig[0].fireRate = 5f;
                //_fireDelay = 60f / _weaponData.SupportedFireModes[_currentFireMode].fireRate;
                //_maxShotCount = _weaponData.SupportedFireModes[_currentFireMode].maxShotCount;
                _firing = true;
            }
            else
                Reload();
        }

        public void StopFire()
        {
            _firing = false;
        }

        public void SwitchFireMode()
        {
            //if (_currentFireMode == _weaponData.SupportedFireModes.Count - 1)
            //    _currentFireMode = 0;
            //else
            //    _currentFireMode++;
        }

        public void Reload()
        {
            //StopFire();
            //if (_totalAmmo > 0)
            //{
            //    _nextReloadReadyTime = Time.time + _weaponData.ReloadTime;
            //    _reloading = true;
            //}
        }

        //############################################################################################
        // PRIVATE UNITY METHODS
        //############################################################################################
        private void Update()
        {
            CalculateFire();
        }

        //############################################################################################
        // PRIVATE METHODS
        //############################################################################################
        private void CalculateFire()
        {
            float time = Time.time;
            if (_firing && time > _nextFireReadyTime)
            {
                if (_magAmmo == 0)
                {
                    Reload();
                    return;
                }
                if (_maxShotCount == 0)
                {
                    StopFire();
                    return;
                }
                _magAmmo--;
                _maxShotCount--;
                _nextFireReadyTime = time + _fireDelay;
                SingleFire();
            }

            if (_reloading && time >= _nextReloadReadyTime)
            {
                _reloading = false;
                //_magAmmo = _weaponData.MagCapacity;
                //_totalAmmo -= _weaponData.MagCapacity;
                if (_totalAmmo < 0)
                {
                    _magAmmo += _totalAmmo;
                    _totalAmmo = 0;
                }
            }
        }

        private void SingleFire()
        {
            //Ray ray = _camera.ScreenPointToRay(new Vector2(Screen.width * 0.5f, Screen.height * 0.5f));
            //Debug.DrawRay(ray.origin, ray.direction * _weaponData.AmmoMaxDistance);
            //Vector3 ammoDirection = (ray.origin + ray.direction * _weaponData.AmmoMaxDistance - _ammoSpawnPoint.position).normalized;
            //Quaternion ammoRotation = Quaternion.LookRotation(ammoDirection);
            //Rigidbody ammoInstance = Instantiate(_ammoRigidbody, _ammoSpawnPoint.position, ammoRotation);
            //ammoInstance.AddForce(ammoDirection * _weaponData.AmmoSpeed, ForceMode.Impulse);
            //_audioSource.PlayOneShot(_weaponData.FireSound);
        }
    }
}
