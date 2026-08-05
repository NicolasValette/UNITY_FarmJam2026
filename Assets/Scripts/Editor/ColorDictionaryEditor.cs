using System;
using UnityEditor;

namespace FarmJam2026
{
    [CustomEditor(typeof(ColorDictionary))]
    public class ColorDictionaryEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            var dico = (ColorDictionary)target;
            if (dico == null)
            {
                EditorGUILayout.LabelField("Dico is null..?");
                return;
            }

            dico.Validate();

            foreach (ColorName val in Enum.GetValues(typeof(ColorName)))
            {
                int valId = Convert.ToInt32(val);
                var color = EditorGUILayout.ColorField(val.ToString(), dico.ColorForName[valId]);
                if (color != dico.ColorForName[valId])
                {
                    Undo.RecordObject(dico, $"Edit color for {val}");
                    dico.ColorForName[valId] = color;
                    EditorUtility.SetDirty(dico);
                }
            }
        }
    }
}