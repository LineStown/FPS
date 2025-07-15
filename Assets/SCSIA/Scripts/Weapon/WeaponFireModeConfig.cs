using Assets.SCSIA.Scripts.Enums;
using System;
using UnityEditor;
using UnityEngine;

namespace Assets.SCSIA.Scripts.Weapon
{
    [Serializable]
    public class WeaponFireModeConfig
    {
        [SerializeField] private WeaponFireMode _fireMode;
        [SerializeField] private float _fireRate;
        [SerializeField] private int _burstCount;

        public WeaponFireMode FireMode => _fireMode;
        public float FireRate => _fireRate;
        public int BurstCount => _burstCount;
    }

    [CustomPropertyDrawer(typeof(WeaponFireModeConfig))]
    public class WeaponFireModeDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            float s = 5f;
            float f = (position.width - s * 2) / 3f;
            float h = EditorGUIUtility.singleLineHeight;

            EditorGUIUtility.labelWidth = 40f;
            EditorGUI.PropertyField(new Rect(position.x, position.y, f, h), property.FindPropertyRelative("_fireMode"), new GUIContent("Mode"));
            EditorGUI.PropertyField(new Rect(position.x + f + s, position.y, f, h), property.FindPropertyRelative("_fireRate"), new GUIContent("Rate"));
            EditorGUIUtility.labelWidth = 80f;
            EditorGUI.PropertyField(new Rect(position.x + (f + s) * 2, position.y, f, h), property.FindPropertyRelative("_burstCount"), new GUIContent("Burst Count"));
        }
    }
}