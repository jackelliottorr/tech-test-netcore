namespace Todo.GravatarConnector;

public interface IProfileService
{
    Task<GravatarProfile> GetProfileAsync(string profileIdentifier);
}