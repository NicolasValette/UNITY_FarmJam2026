using System;
using UnityEditor;
using UnityEngine;

namespace FarmJam2026
{
    [CustomEditor(typeof(MushroomDefinitions))]
    public class MushroomDefinitionEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            var defs = (MushroomDefinitions)target;
            if (defs == null)
            {
                EditorGUILayout.LabelField("Mushroom Definitions file is null..?");
                return;
            }

            EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(defs.MushroomPrefab)));

            var expectedVariationCount = (int)EBodyType.ENUM_COUNT * (int)EBodyType.ENUM_COUNT;
            if (defs.MushroomVariations.Length != expectedVariationCount)
            {
                Undo.RecordObject(defs, $"Resize {nameof(defs.MushroomVariations)}");
                var memo = defs.MushroomVariations;
                defs.MushroomVariations = new MushroomVariantData[expectedVariationCount];
                for (int i = 0; i < Mathf.Min(expectedVariationCount, memo.Length); i++)
                {
                    defs.MushroomVariations[i] = memo[i];
                }
                EditorUtility.SetDirty(defs);
            }

            var enumValues = (EBodyType[])Enum.GetValues(typeof(EBodyType));
            for (var primId = 0; primId < (int)EBodyType.ENUM_COUNT; primId++)
            {
                EditorGUILayout.LabelField($"PRIMARY VARIANT {enumValues[primId]}");
                for (var secId = 0; secId < (int)EBodyType.ENUM_COUNT; secId++)
                {
                    var varId = primId * (int)EBodyType.ENUM_COUNT + secId;
                    var item = EditorGUILayout.ObjectField($"{(EBodyType)secId}", defs.MushroomVariations[varId], typeof(MushroomVariantData), false);
                    if (item != defs.MushroomVariations[varId])
                    {
                        Undo.RecordObject(defs, $"Edit {nameof(defs.MushroomVariations)}[{varId}]");
                        defs.MushroomVariations[varId] = item as MushroomVariantData;
                        EditorUtility.SetDirty(defs);
                    }
                }
            }
        }
    }
}