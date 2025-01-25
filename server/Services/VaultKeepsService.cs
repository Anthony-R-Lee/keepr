

namespace keepr.Services;

public class VaultKeepsService
{

  public VaultKeepsService(VaultKeepsRepository repository, VaultsService vaultsService)
  {
    _repository = repository;
    _vaultsService = vaultsService;
  }
  private readonly VaultKeepsRepository _repository;
  private readonly VaultsService _vaultsService;

  internal VaultKeep CreateVaultKeep(VaultKeep vaultKeepData)
  {
    // {
    //   Vault vault = _vaultsService.GetVaultById(vaultKeepData.VaultId, vaultKeepData.CreatorId);

    //   if (vault.CreatorId == vaultKeepData.CreatorId) throw new Exception("You can't add a keep to your own vault!");
    VaultKeep vaultkeep = _repository.Create(vaultKeepData);
    return vaultkeep;
  }

  internal List<VaultKeep> GetKeepsInPublicVault(int vaultId)
  {
    List<VaultKeep> vaultKeeps = _repository.GetKeepsInPublicVault(vaultId);
    return vaultKeeps;
  }
}