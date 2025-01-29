
namespace keepr.Services;

public class ProfilesService
{

  public ProfilesService(ProfilesRepository repository, VaultsService vaultsService)
  {
    _repository = repository;
    _vaultsService = vaultsService;
  }
  private readonly ProfilesRepository _repository;
  private readonly VaultsService _vaultsService;

  internal Profile GetUserProfile(string profileId)
  {
    Profile profile = _repository.GetUserProfile(profileId);

    if (profile == null) throw new Exception($"No profile at id: {profileId}!");

    return profile;
  }

  internal List<Keep> GetUserKeeps(string profileId)
  {
    List<Keep> keeps = _repository.GetUserKeeps(profileId);

    if (keeps == null) throw new Exception($"No profile keeps at id: {profileId}!");

    return keeps;
  }

  internal List<Vault> GetUserVaults(string profileId, string userId)
  {

    List<Vault> vaults = GetUserVaults(profileId);

    return vaults.FindAll(vault => vault.CreatorId == userId || vault.IsPrivate == false);
  }

  private List<Vault> GetUserVaults(string profileId)
  {
    List<Vault> vaults = _repository.GetUserVaults(profileId);

    return vaults;
  }
}