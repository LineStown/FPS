using Assets.SCSIA.Scripts.Core;
using Assets.SCSIA.Scripts.DataModel;
using Assets.SCSIA.Scripts.Enums;
using UnityEngine;


namespace Assets.SCSIA.Scripts.Weapon
{
    public class WeaponController : PickupableItem
    {
        //############################################################################################
        // FIELDS
        //############################################################################################
        [Header("Weapon Data")]
        [SerializeField] private WeaponDataModel _weaponData;

        [Header("Ammo")]
        [SerializeField] private Rigidbody _ammoRigidbody;
        [SerializeField] private Transform _ammoSpawnPoint;

        [Header("Audio")]
        [SerializeField] private AudioSource _audioSource;

        private int _currentFireMode;
        private float _fireDelay;

        private int _burstShotsCount;
        private int _burstShotsCountDone;
        private float _currentSpread;

        private int _magAmmo;
        private int _totalAmmo;

        private float _nextFireReadyTime;
        private bool _firing;
        private float _nextReloadReadyTime;
        private bool _reloading;

        //############################################################################################
        // PROPERTIES
        //############################################################################################
        public WeaponId Id => _weaponData.Id;
        public WeaponSlot Slot => _weaponData.Slot;

        //############################################################################################
        // PUBLIC METHODS
        //############################################################################################
        public void Init()
        {
            _currentFireMode = 0;
            _fireDelay = 60f / _weaponData.FireRate;
            _magAmmo = _weaponData.MagCapacity;
            _totalAmmo = _weaponData.TotalCapacity;
            _firing = false;
            _reloading = false;
        }

        public void StartFire()
        {
            if (_reloading == true)
                return;
            if (_magAmmo > 0)
            {
                _burstShotsCount = _weaponData.FireModeConfig[_currentFireMode].BurstShotsCount;
                _burstShotsCountDone = 0;
                _firing = true;
            }
            else
            {
                _audioSource.PlayOneShot(_weaponData.EmptySound);
                Reload();
            }
        }

        public void StopFire()
        {
            _firing = false;
        }

        public void SwitchFireMode()
        {
            // don't switch if only 1 mode
            if (_weaponData.FireModeConfig.Count == 1)
                return;
            // switch
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
            if (_magAmmo == _weaponData.MagCapacity)
                return;

            _nextReloadReadyTime = Time.time + _weaponData.ReloadTime;
            _reloading = true;
            _audioSource.PlayOneShot(_weaponData.ReloadSound);
        }

        public void BuyAmmo()
        {
            _totalAmmo = _weaponData.TotalCapacity;
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
                    _audioSource.PlayOneShot(_weaponData.EmptySound);
                    Reload();
                    return;
                }
                if (_burstShotsCount == 0)
                {
                    StopFire();
                    return;
                }
                // spread by shot
                _currentSpread = _weaponData.DeltaSpread + _weaponData.DeltaSpread * _burstShotsCountDone;
                if (_currentSpread > _weaponData.MaxSpread)
                    _currentSpread = _weaponData.MaxSpread;
                SingleShot();
                _magAmmo--;
                _burstShotsCount--;
                _burstShotsCountDone++;
                _nextFireReadyTime = time + _fireDelay;
                Debug.Log($"_magAmmo: {_magAmmo}  _burstCount: {_burstShotsCount}  _totalAmmo: {_totalAmmo}");
            }

            if (_reloading && time >= _nextReloadReadyTime)
            {
                _totalAmmo += _magAmmo;
                _magAmmo = _weaponData.MagCapacity;
                _totalAmmo -= _magAmmo;
                if (_totalAmmo < 0)
                {
                    _magAmmo += _totalAmmo;
                    _totalAmmo = 0;
                }
                _reloading = false;
            }
        }

        private void SingleShot()
        {
            for (int i = 0; i < _weaponData.FireModeConfig[_currentFireMode].CartridgeShotsCount; i++)
            {
                Ray ray = new Ray(transform.parent.transform.position, transform.parent.transform.forward);
                // calculate final point with spread
                Vector3 ammoFinalPoint = ray.direction * _weaponData.MaxDistance;
                ammoFinalPoint.x += Random.Range(-_currentSpread, _currentSpread);
                ammoFinalPoint.y += Random.Range(-_currentSpread, _currentSpread);
                ammoFinalPoint.z += Random.Range(-_currentSpread, _currentSpread);
                // draw ray
                Debug.DrawRay(ray.origin, ray.direction * _weaponData.MaxDistance);
                // prepare ammo
                Vector3 ammoDirection = (ray.origin + ammoFinalPoint - _ammoSpawnPoint.position).normalized;
                Quaternion ammoRotation = Quaternion.LookRotation(ammoDirection);
                Rigidbody ammoInstance = Instantiate(_ammoRigidbody, _ammoSpawnPoint.position, ammoRotation);
                ammoInstance.AddForce(ammoDirection * _weaponData.AmmoSpeed, ForceMode.Impulse);
            }
            _audioSource.PlayOneShot(_weaponData.FireSound);
        }
    }
}
