using System;
using System.Collections.Generic;
using UnityEngine;

namespace FarmJam2026
{
    [Serializable]
    public class SoundDictionary<T> : ScriptableObject
        where T : Enum
    {
        [SerializeField]
        public List<AudioClip> Audios = new List<AudioClip>();

        protected void Validate()
        {
            var vals = Enum.GetValues(typeof(T));
            if (Audios.Count != vals.Length)
            {
                var mem = new List<AudioClip>(Audios);
                Audios.Clear();
                for (int i = 0; i < vals.Length; i++)
                {
                    if (mem.Count > i)
                        Audios.Add(mem[i]);
                    else Audios.Add(null);
                }
            }
        }
    }
}