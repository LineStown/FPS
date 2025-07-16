using Assets.SCSIA.Scripts.Enums;
using System;
using UnityEditor;
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

        //############################################################################################
        // PREPERTIES
        //############################################################################################
        public WeaponFireMode FireMode => _fireMode;
        public int BurstShotsCount => _burstShotsCount;
    }

    [CustomPropertyDrawer(typeof(WeaponFireModeConfig))]
    public class WeaponFireModeDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            float s = 5f;
            float f = (position.width - s) / 2f;
            float h = EditorGUIUtility.singleLineHeight;

            EditorGUIUtility.labelWidth = 40f;
            EditorGUI.PropertyField(new Rect(position.x, position.y, f, h), property.FindPropertyRelative("_fireMode"), new GUIContent("Mode"));
            EditorGUIUtility.labelWidth = 120f;
            EditorGUI.PropertyField(new Rect(position.x + (f + s), position.y, f, h), property.FindPropertyRelative("_burstShotsCount"), new GUIContent("Burst Shots Count"));
        }
    }
}