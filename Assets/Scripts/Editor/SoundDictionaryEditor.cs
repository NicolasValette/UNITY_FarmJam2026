using System;
using UnityEditor;
using UnityEngine;

namespace FarmJam2026
{
    [CustomEditor(typeof(SFXDictionary))]
    public class SFXDictionaryEditor : SoundDictionaryEditor<ESoundSFX> { }

    [CustomEditor(typeof(MusicDictionary))]
    public class MusicDictionaryEditor : SoundDictionaryEditor<ESoundMusic> { }

    public class SoundDictionaryEditor<T> : Editor
        where T : Enum
    {
        public override void OnInspectorGUI()
        {
            var dico = (SoundDictionary<T>)target;
            if (dico == null)
            {
                EditorGUILayout.LabelField("Dico is null..?");
                return;
            }

            dico.Validate();

            foreach (T val in Enum.GetValues(typeof(T)))
            {
                int valId = Convert.ToInt32(val);
                var item = EditorGUILayout.ObjectField(val.ToString(), dico.Audios[valId], typeof(AudioClip), false);
                if (item != dico.Audios[valId])
                {
                    Undo.RecordObject(dico, $"Edit clip for {val}");
                    dico.Audios[valId] = item as AudioClip;
                    EditorUtility.SetDirty(dico);
                }
            }
        }
    }
}
