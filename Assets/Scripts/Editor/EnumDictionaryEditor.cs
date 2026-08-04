using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace FarmJam2026
{
    [CustomPropertyDrawer(typeof(EnumDictionaryAttribute))]
    public class EnumDictionaryDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EnumDictionaryAttribute enumAttribute = (EnumDictionaryAttribute)attribute;
            if (enumAttribute.EnumType == null || !enumAttribute.EnumType.IsEnum)
            {
                EditorGUILayout.HelpBox("EnumDictionary expects enum type.", MessageType.Error);
                return;
            }
            if (!fieldInfo.FieldType.IsGenericType || fieldInfo.FieldType.GetGenericTypeDefinition() != typeof(List<>))
            {
                EditorGUILayout.HelpBox("EnumDictionary should target a List<>.", MessageType.Error);
                return;
            }

            string[] enumNames = Enum.GetNames(enumAttribute.EnumType);
            var elemIdx = int.Parse(property.displayName.Substring(8));
            var enumName = enumNames[elemIdx];

            EditorGUI.BeginProperty(position, label, property);

            Rect foldoutRect = new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight);
            property.isExpanded = EditorGUI.Foldout(foldoutRect, property.isExpanded, enumName, true);
            if (property.isExpanded)
            {
                EditorGUI.indentLevel++;
                EditorGUILayout.PropertyField(property, GUIContent.none);
                EditorGUI.indentLevel--;
            }
            EditorGUI.EndProperty();
        }
    }
}