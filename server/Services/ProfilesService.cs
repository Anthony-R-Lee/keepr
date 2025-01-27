
namespace keepr.Services;

public class ProfilesService
{

  public ProfilesService(ProfilesRepository repository)
  {
    _repository = repository;
  }
  private readonly ProfilesRepository _repository;

  internal Profile GetUserProfile(string profileId)
  {
    Profile profile = _repository.GetUserProfile(profileId);

    if (profile == null) throw new Exception($"No profile at id: {profileId}!");

    return profile;
  }
}