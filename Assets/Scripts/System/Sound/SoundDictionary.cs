using System;
using System.Collections.Generic;
using UnityEngine;

namespace FarmJam2026
{
    [CreateAssetMenu(fileName = "SFXDico", menuName = "Data/Sound SFX Dico")]
    public class SFXDictionary : SoundDictionary<ESoundSFX>
    {
        private void OnValidate()
        {
            Validate();
        }
    }

    [CreateAssetMenu(fileName = "MusicDico", menuName = "Data/Sound Music Dico")]
    public class MusicDictionary : SoundDictionary<ESoundMusic>
    {
        private void OnValidate()
        {
            Validate();
        }
    }

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