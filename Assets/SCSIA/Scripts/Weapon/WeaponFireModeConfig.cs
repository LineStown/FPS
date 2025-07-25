﻿using Assets.SCSIA.Scripts.Enums;
using System;
using UnityEngine;

namespace Assets.SCSIA.Scripts.Weapon
{
    [Serializable]
    public class WeaponFireModeConfig
    {
        //############################################################################################
        // FIELDS
        //############################################################################################
        [SerializeField] private WeaponFireMode _fireMode;
        [SerializeField] private int _burstShotsCount;
        [SerializeField] private int _cartridgeShotsCount;

        //############################################################################################
        // PREPERTIES
        //############################################################################################
        public WeaponFireMode FireMode => _fireMode;
        public int BurstShotsCount => _burstShotsCount;
        public int CartridgeShotsCount => _cartridgeShotsCount;
    }
}