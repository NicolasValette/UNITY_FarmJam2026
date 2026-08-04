using System;
using UnityEngine;

namespace FarmJam2026
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    public class EnumDictionaryAttribute : PropertyAttribute
    {
        public Type EnumType { get; private set; }

        public EnumDictionaryAttribute(Type enumType)
        {
            if (enumType == null || !enumType.IsEnum)
            {
                Debug.LogError("EnumDictionary attribute expects ENUM type.");
            }

            EnumType = enumType;
        }
    }
}