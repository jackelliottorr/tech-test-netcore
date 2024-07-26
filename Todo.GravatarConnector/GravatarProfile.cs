using System.Text.Json.Serialization;

namespace Todo.GravatarConnector;

public class GravatarProfile
{
    [JsonPropertyName("hash")]
    public string Hash { get; set; }

    [JsonPropertyName("display_name")]
    public string DisplayName { get; set; }

    [JsonPropertyName("profile_url")]
    public string ProfileUrl { get; set; }

    [JsonPropertyName("avatar_url")]
    public string AvatarUrl { get; set; }
}
