using System;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace FarmJam2026
{
    [CustomEditor(typeof(Mushroom))]
    public class MushroomEditor : Editor
    {
        override public void OnInspectorGUI()
        {
            Mushroom mushroom = (Mushroom)target;
            if (mushroom == null)
            {
                EditorGUILayout.LabelField("Mushroom is null..?");
                return;
            }

            DrawDefaultInspector();

            EditorGUILayout.Space(10);

            var properties = mushroom.GetType()
                .GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

            bool headerDrawn = false;
            foreach (var prop in properties)
            {
                if (Attribute.IsDefined(prop, typeof(MushroomGeneExpressionAttribute)))
                {
                    if (!headerDrawn)
                    {
                        EditorGUILayout.Space(10);
                        EditorGUILayout.LabelField("Gene Expression", EditorStyles.boldLabel);
                        headerDrawn = true;
                    }

                    EditorGUI.BeginDisabledGroup(true);

                    object val = prop.GetValue(mushroom, null);
                    string label = ObjectNames.NicifyVariableName(prop.Name);

                    if (prop.PropertyType == typeof(float))
                        EditorGUILayout.FloatField(label, val != null ? (float)val : 0f);
                    else if (prop.PropertyType == typeof(int))
                        EditorGUILayout.IntField(label, val != null ? (int)val : 0);
                    else if (prop.PropertyType == typeof(Color))
                        EditorGUILayout.ColorField(label, val != null ? (Color)val : Color.white);
                    else
                        EditorGUILayout.TextField(label, val?.ToString() ?? "null");
                    
                    EditorGUI.EndDisabledGroup();
                }
            }
        }
    }
}
