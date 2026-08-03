using System;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace FarmJam2026
{
    [Serializable]
    public class SoundDictionary<T> : ScriptableObject
        where T : Enum
    {
        [SerializeField]
        public List<AudioClip> Audios = new List<AudioClip>();

        public void Validate()
        {
            var vals = Enum.GetValues(typeof(T));
            if (Audios.Count != vals.Length)
            {
#if UNITY_EDITOR
                Undo.RecordObject(this, $"Validate sound dico");
#endif
                var mem = new List<AudioClip>(Audios);
                Audios = new List<AudioClip>(Audios);
                for (int i = 0; i < vals.Length; i++)
                {
                    Audios[i] = mem.Count > i ? mem[i] : null;
                }
#if UNITY_EDITOR
                EditorUtility.SetDirty(this);
#endif
            }
        }
    }
}