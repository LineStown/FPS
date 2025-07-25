﻿using UnityEngine;

namespace Assets.SCSIA.Scripts.Actions
{
    internal class DestroyWithDelay : MonoBehaviour
    {
        //############################################################################################
        // FIELDS
        //############################################################################################
        [SerializeField] private float _delay;

        //############################################################################################
        // PRIVATE UNITY METHODS
        //############################################################################################
        private void Awake()
        {
            Destroy(gameObject, _delay);
        }
    }
}
