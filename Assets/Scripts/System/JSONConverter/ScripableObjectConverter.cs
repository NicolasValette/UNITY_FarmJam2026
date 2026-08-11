using Newtonsoft.Json;
using System;
using UnityEngine;

namespace FarmJam2026
{
    public class ScripableObjectConverter : JsonConverter
    {
       
            public override bool CanConvert(Type  objectType)
            {
                // S'applique à n'importe quel type qui hérite de ScriptableObject
                return typeof(ScriptableObject).IsAssignableFrom(objectType);
            }

            public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
            {
                if (reader.TokenType == JsonToken.Null) return null;

                object target = existingValue ?? ScriptableObject.CreateInstance(objectType);
                serializer.Populate(reader, target);
                return target;
            }

            public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
            {
                serializer.Serialize(writer, value);
            }
        
    }
}
