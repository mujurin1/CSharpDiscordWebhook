using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CSharpDiscordWebhook.NET.Discord.Json
{
    public class AllowedMentionTypeConverter : JsonConverter<AllowedMentionType>
    {
        public override AllowedMentionType Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            string str = reader.GetString();
            if (str == null) return null;

            return str.ToLower() switch
            {
                "users" => AllowedMentionType.User,
                "roles" => AllowedMentionType.Role,
                "everyone" => AllowedMentionType.Everyone,
                _ => throw new InvalidOperationException($"Can't read mention type {str}"),
            };
        }
        public override void Write(Utf8JsonWriter writer, AllowedMentionType value, JsonSerializerOptions options)
            => writer.WriteStringValue(value.ToString());
    }
}
