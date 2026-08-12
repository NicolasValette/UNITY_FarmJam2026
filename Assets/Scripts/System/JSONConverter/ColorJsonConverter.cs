using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using UnityEngine;

namespace FarmJam2026
{
    public class ColorJsonConverter : JsonConverter<Color>
    {
        public override void WriteJson(JsonWriter writer, Color value, JsonSerializer serializer)
        {
            JObject obj = new JObject
            {
                ["r"] = value.r,
                ["g"] = value.g,
                ["b"] = value.b,
                ["a"] = value.a
            };
            obj.WriteTo(writer);
        }

        public override Color ReadJson(JsonReader reader, Type objectType, Color existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            JObject obj = JObject.Load(reader);
            return new Color(
                (float)(obj["r"] ?? 0),
                (float)(obj["g"] ?? 0),
                (float)(obj["b"] ?? 0),
                (float)(obj["a"] ?? 1)
            );
        }
    }
}
