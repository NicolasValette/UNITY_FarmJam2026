using System.Collections.Generic;
using UnityEngine;
using System;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace FarmJam2026
{
    [CreateAssetMenu(fileName = "ColorDico", menuName = "Data/Color Dico")]
    public class ColorDictionary : ScriptableObject
    {
        [SerializeField, EnumDictionary(typeof(ColorName))]
        public List<Color> ColorForName = new List<Color>();

        public void Validate()
        {
            var vals = Enum.GetValues(typeof(ColorName));
            if (ColorForName.Count != vals.Length)
            {
#if UNITY_EDITOR
                Undo.RecordObject(this, $"Validate color dico");
#endif
                var mem = new List<Color>(ColorForName);
                ColorForName = new List<Color>();
                for (int i = 0; i < vals.Length; i++)
                {
                    if (ColorForName.Count > i)
                        ColorForName.Add(mem[i]);
                    else
                    {
                        ColorForName.Add(Color.black);
                        if (mem.Count >= i)
                            ColorForName[i] = mem[i];
                    }
                }
#if UNITY_EDITOR
                EditorUtility.SetDirty(this);
#endif
            }
        }
    }
}
