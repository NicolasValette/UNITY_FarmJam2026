using UnityEditor;
using UnityEngine;

namespace FarmJam2026
{
    [CustomPropertyDrawer(typeof(ChampiNameAttribute))]
    public class ChampiNameDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (property.propertyType != SerializedPropertyType.String)
            {
                EditorGUI.HelpBox(position, $"[ChampiName] ne s'applique que sur un champ 'string' ('{label.text}' est invalide).", MessageType.Error);
                return;
            }

            EditorGUI.BeginProperty(position, label, property);
            EditorGUILayout.BeginHorizontal();
            {
                EditorGUILayout.PropertyField(property);
                if (GUILayout.Button("Mushify!", GUILayout.MaxWidth(110)))
                {
                    property.stringValue = NameGenerator.Instance?.GenerateRandomName() ?? "No Generator found";
                    property.serializedObject.ApplyModifiedProperties();
                }
            }
            EditorGUILayout.EndHorizontal();
            EditorGUI.EndProperty();
        }
    }
}
