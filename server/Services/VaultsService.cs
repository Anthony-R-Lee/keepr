

using Microsoft.AspNetCore.Http.HttpResults;

namespace keepr.Services;

public class VaultsService
{

  public VaultsService(VaultsRepository repository)
  {
    _repository = repository;
  }
  private readonly VaultsRepository _repository;

  internal Vault CreateVault(Vault vaultData)
  {
    Vault vault = _repository.Create(vaultData);
    return vault;
  }


  private Vault GetVaultById(int vaultId)
  {
    Vault vault = _repository.GetById(vaultId);

    if (vault == null) throw new Exception($"Invalid vault id: {vaultId}");

    return vault;
  }

  internal Vault GetVaultById(int vaultId, string profileId)
  {
    Vault vault = GetUserVaultKeepsById(vaultId);

    if (vault.CreatorId != profileId && vault.IsPrivate == true) throw new Exception($"Invalid user vault id: {vaultId}");

    return vault;
  }

  internal Vault UpdateVault(int vaultId, string userId, Vault vaultData)
  {
    Vault vault = GetVaultById(vaultId);

    if (vault.CreatorId != userId) throw new Exception("This is not your vault to edit!");

    vault.Name = vaultData.Name ?? vault.Name;
    vault.IsPrivate = vaultData.IsPrivate ?? vault.IsPrivate;

    _repository.Update(vault);

    return vault;
  }

  internal string DeleteVault(int vaultId, string userId)
  {
    Vault vault = GetVaultById(vaultId);

    if (vault.CreatorId != userId) throw new Exception("This is not your vault to delete!");

    _repository.Delete(vaultId);

    return $"Deleted {vault.Name}";
  }

  internal List<Vault> GetMyVaults(string userId)
  {
    List<Vault> vaults = _repository.GetMyVaults(userId);

    return vaults;
  }


  private Vault GetUserVaultKeepsById(int vaultId)
  {
    Vault vault = _repository.GetById(vaultId);

    if (vault == null) throw new Exception($"Invalid vault id: {vaultId}");

    return vault;
  }




}