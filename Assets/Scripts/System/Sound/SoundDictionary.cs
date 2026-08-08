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
                Audios = new List<AudioClip>();
                for (int i = 0; i < vals.Length; i++)
                {
                    if (Audios.Count > i)
                        Audios.Add(mem[i]);
                    else
                    {
                        Audios.Add(null);
                        if (mem.Count >= i)
                            Audios[i] = mem[i];
                    }
                }
#if UNITY_EDITOR
                EditorUtility.SetDirty(this);
#endif
            }
        }
    }
}