using CSharpDiscordWebhook.NET.Discord.Json;
using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;

namespace CSharpDiscordWebhook.NET.Discord;

public class DiscordWebhook
{
    internal static readonly HttpClient HttpClient = new();

    /// <summary>
    /// Webhook url
    /// </summary>
    public string WebhookUrl { get; set; }

    public DiscordWebhook(string webhookUrl = null!)
    {
        WebhookUrl = webhookUrl;
    }

    /// <summary>
    /// Send webhook message
    /// </summary>
    public async Task<DiscordResponse> SendAsync(DiscordMessage message, params FileInfo[] files)
    {
        return await PostOrPatch(message, files);
    }

    public async Task<DiscordResponse> EditAsync(DiscordResponse response, params FileInfo[] files)
    {
        return await PostOrPatch(DiscordMessage.FromResponse(response), files, response.Id);
    }

    public async Task DeleteAsync()
    {
        await HttpClient.DeleteAsync(WebhookUrl);
    }

    private async Task<DiscordResponse> PostOrPatch(DiscordMessage message, FileInfo[] files, string? messageId = null)
    {
        string bound = $"------------------------{DateTime.Now.Ticks:x}";

        var httpContent = new MultipartFormDataContent(bound);

        foreach (var file in files)
        {
            var fileContent = new ByteArrayContent(await File.ReadAllBytesAsync(file.FullName));
            fileContent.Headers.ContentType = MediaTypeHeaderValue.Parse("application/octet-stream");
            httpContent.Add(fileContent, file.Name, file.Name);
        }

        var jsonContent = new StringContent(JsonSerializer.Serialize(message, JSON_SETTINGS));
        jsonContent.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");
        httpContent.Add(jsonContent, "payload_json");

        var response = string.IsNullOrWhiteSpace(messageId)
            ? await HttpClient.PostAsync($"{WebhookUrl}?wait=true", httpContent)
            : await HttpClient.PatchAsync($"{WebhookUrl}/messages/{messageId}", httpContent);

        if (!response.IsSuccessStatusCode)
            throw new Exception(await response.Content.ReadAsStringAsync());

        var result = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<DiscordResponse>(result, JSON_SETTINGS)!;
    }

    private static readonly JsonSerializerOptions JSON_SETTINGS = new()
    {
        PropertyNamingPolicy = new DiscordJsonNaming(),
        IgnoreReadOnlyProperties = false,
        DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull,
        Converters =
        {
            new DiscordColorConverter(),
            new DiscordTimestampConverter(),
            new AllowedMentionTypeConverter()
        }
    };
}