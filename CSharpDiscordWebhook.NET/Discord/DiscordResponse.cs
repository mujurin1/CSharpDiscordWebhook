using System;
using System.Collections.Generic;

namespace CSharpDiscordWebhook.NET.Discord
{
    public class DiscordResponse
    {
        // id: "1137006305631019068";
        public string Id { get; set; } = null!;

        // type: 0;
        public int Type { get; set; }

        // content: "Message!";
        public string Content { get; set; } = null!;

        // channel_id: "1098698350682968216";
        public string ChannelId { get; set; } = null!;

        public DiscordWebhookResponseAuthor Author { get; set; } = null!;


        // attachments:[];
        public object[] Attachments { get; set; } = null!;

        // embeds:[];
        public DiscordEmbed[] Embeds { get; set; } = null!;

        // mentions:[];
        public object[] Mentions { get; set; } = null!;

        // mention_roles:[];
        public object[] MentionRoles { get; set; } = null!;

        // pinned: false;
        public bool Pinned { get; set; }

        // mention_everyone: false;
        public bool MentionEveryone { get; set; }

        // tts: false;
        public bool TTS { get; set; }

        // timestamp: "2023-08-04T1:5:07.818000+0:00";
        public string Timestamp { get; set; } = null!;

        // edited_timestamp: "2023-08-04T1:5:28.922030+0:00";
        public string EditedTimestamp { get; set; } = null!;

        // flags: 0;
        public DiscordMessageFlag Flags { get; set; }

        // components:[];
        public object[] Components { get; set; } = null!;

        // webhook_id: "1135233696740425808"
        public string WebhookId { get; set; } = null!;
    }

    public class DiscordWebhookResponseAuthor
    {
        // id: "1135233696740425808";
        public string Id { get; set; } = null!;

        // username: "Webhook Username";
        public string Username { get; set; } = null!;

        // avatar: null;
        public string? Avatar { get; set; }

        // discriminator: "0000";
        public string Discriminator { get; set; } = null!;

        // public_flags: 0;
        public int PublicFlags { get; set; }

        // flags: 0;
        public int Flags { get; set; }

        // bot: true
        public bool Bot { get; set; }
    }
}
