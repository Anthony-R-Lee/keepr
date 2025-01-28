

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

  internal Vault GetVaultById(int vaultId)
  {
    Vault vault = _repository.GetById(vaultId);

    if (vault == null || vault.IsPrivate == true) throw new Exception($"Invalid vault id: {vaultId} or vault is private");

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

  internal List<Vault> GetMyVaults()
  {
    List<Vault> vaults = _repository.GetMyVaults();
    return vaults;
  }

  internal Vault GetVaultById(int vaultId, string userId)
  {
    Vault vault = _repository.GetById(vaultId);

    if (vault.IsPrivate == true && vault.CreatorId != userId || vault == null) throw new Exception($"Vault is private");

    return vault;
  }
}