using System.Collections.Generic;
using UnityEngine;
using System;


#if UNITY_EDITOR
using UnityEditor;
#endif

namespace FarmJam2026
{
    [CreateAssetMenu(fileName = "SFXDico", menuName = "Data/Sound SFX Dico")]
    public class SFXDictionary : ScriptableObject
    {
        [SerializeField, EnumDictionary(typeof(ESoundSFX))]
        public List<AudioClip> Audios = new List<AudioClip>();

        public void Validate()
        {
            var vals = Enum.GetValues(typeof(ESoundSFX));
            if (Audios.Count != vals.Length)
            {
#if UNITY_EDITOR
                Undo.RecordObject(this, $"Validate sfx dico");
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
