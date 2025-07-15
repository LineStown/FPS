using Assets.SCSIA.Scripts.Data;
using Assets.SCSIA.Scripts.Enums;
using Unity.VisualScripting;
using UnityEngine;

namespace Assets.SCSIA.Scripts.Weapon
{
    public class WeaponController : MonoBehaviour
    {
        //############################################################################################
        // FIELDS
        //############################################################################################
        private Camera _camera;
        private WeaponDataModel _weaponData;
        private Rigidbody _ammoRigidbody;

        private WeaponView _weaponView;
        private AudioSource _audioSource;

        private bool _inited = false;

        private int _currentFireMode;
        private int _burstCount;
        private float _fireDelay;

        private int _magAmmo;
        private int _totalAmmo;

        private float _nextFireReadyTime;
        private bool _firing;
        private float _nextReloadReadyTime;
        private bool _reloading;

        //############################################################################################
        // PUBLIC METHODS
        //############################################################################################
        public void Initialize(Camera camera, Rigidbody ammoRigidbody, AudioSource audioSource)
        {
            if (!_inited)
            {
                _camera = camera;
                _ammoRigidbody = ammoRigidbody;
                _audioSource = audioSource;
                _inited = true;
            }
        }

        public void SpawnWeapon(WeaponDataModel weaponData)
        {
            if (_inited)
            {
                _weaponData = weaponData;
                if (_weaponView != null)
                    Destroy(_weaponView);
                _weaponView = Instantiate(_weaponData.View, this.transform);

                _currentFireMode = 0;
                _magAmmo = _weaponData.AmmoConfig.MagCapacity;
                _totalAmmo = _weaponData.AmmoConfig.TotalCapacity;
                _firing = false;
                _reloading = false;
            }
        }

        public void StartFire()
        {
            if (_reloading == true)
                return;
            if (_magAmmo > 0)
            {
                _fireDelay = 60f / _weaponData.FireModeConfig[_currentFireMode].FireRate;
                _burstCount = _weaponData.FireModeConfig[_currentFireMode].BurstCount;
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
            StopFire();
            if (_currentFireMode == _weaponData.FireModeConfig.Count - 1)
                _currentFireMode = 0;
            else
                _currentFireMode++;
            _audioSource.PlayOneShot(_weaponData.SwitchFireModeSound);
        }

        public void Reload()
        {
            StopFire();
            if (_reloading == true)
                return;
            if (_totalAmmo == 0)
                return;
            if (_magAmmo == _weaponData.AmmoConfig.MagCapacity)
                return;

            _nextReloadReadyTime = Time.time + _weaponData.AmmoConfig.ReloadTime;
            _reloading = true;
            _audioSource.PlayOneShot(_weaponData.ReloadSound);
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
                if (_burstCount == 0)
                {
                    StopFire();
                    return;
                }
                _magAmmo--;
                _burstCount--;
                _nextFireReadyTime = time + _fireDelay;
                Debug.Log($"_magAmmo: {_magAmmo}  _burstCount: {_burstCount}  _totalAmmo: {_totalAmmo}");
                SingleFire();
            }

            if (_reloading && time >= _nextReloadReadyTime)
            {
                _totalAmmo += _magAmmo;
                _magAmmo = _weaponData.AmmoConfig.MagCapacity;
                _totalAmmo -= _magAmmo;
                if (_totalAmmo < 0)
                {
                    _magAmmo += _totalAmmo;
                    _totalAmmo = 0;
                }
                _reloading = false;
            }
        }

        private void SingleFire()
        {
            Ray ray = _camera.ScreenPointToRay(new Vector2(Screen.width * 0.5f, Screen.height * 0.5f));
            Debug.DrawRay(ray.origin, ray.direction * _weaponData.AmmoConfig.MaxDistance);
            Vector3 ammoDirection = (ray.origin + ray.direction * _weaponData.AmmoConfig.MaxDistance - _weaponView.AmmoSpawnPoint.position).normalized;
            Quaternion ammoRotation = Quaternion.LookRotation(ammoDirection);
            Rigidbody ammoInstance = Instantiate(_ammoRigidbody, _weaponView.AmmoSpawnPoint.position, ammoRotation);
            ammoInstance.AddForce(ammoDirection * _weaponData.AmmoConfig.Speed, ForceMode.Impulse);
            _audioSource.PlayOneShot(_weaponData.FireSound);
        }
    }
}
