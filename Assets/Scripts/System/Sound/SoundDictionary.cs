using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

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
                Undo.RecordObject(this, $"Validate sound dico");
                var mem = new List<AudioClip>(Audios);
                Audios = new List<AudioClip>(Audios);
                for (int i = 0; i < vals.Length; i++)
                {
                    Audios[i] = mem.Count > i ? mem[i] : null;
                }
                EditorUtility.SetDirty(this);
            }
        }
    }
}