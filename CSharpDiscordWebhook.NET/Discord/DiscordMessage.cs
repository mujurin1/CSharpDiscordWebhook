using System;
using System.Collections.Generic;

namespace CSharpDiscordWebhook.NET.Discord
{
    public class DiscordMessage
    {
        /// <summary>
        /// The message contents (up to 2000 characters)
        /// </summary>
        public string Content { get; set; } = null!;

        /// <summary>
        /// Override the default username of the webhook
        /// </summary>
        public string? Username { get; set; }

        /// <summary>
        /// Override the default avatar of the webhook
        /// </summary>
        public Uri? AvatarUrl { get; set; }

        /// <summary>
        /// True if this is a TTS message
        /// </summary>
        public bool TTS { get; set; }

        /// <summary>
        /// Embedded rich content
        /// </summary>
        public List<DiscordEmbed>? Embeds { get; set; }

        /// <summary>
        /// Allowed mentions for the message
        /// </summary>
        public AllowedMention? AllowedMentions { get; set; }

        /// <summary>
        /// Name of thread to create (requires the webhook channel to be a forum channel)
        /// </summary>
        public string? ThreadName { get; set; }

        public static DiscordMessage FromResponse(DiscordResponse response)
        {
            return new DiscordMessage
            {
                Content = response.Content,
                // Username = response.Author.Username,
                // AvatarUrl = ,
                TTS = response.TTS,
                Embeds = response.Embeds is null ? null : new List<DiscordEmbed>(response.Embeds),
                // AllowedMentions = ,
            };
        }
    }
}
