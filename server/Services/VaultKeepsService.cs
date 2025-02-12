



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
    Vault vault = _vaultsService.GetVaultById(vaultKeepData.VaultId, vaultKeepData.CreatorId);

    if (vault.CreatorId != vaultKeepData.CreatorId) throw new Exception($"Invalid vault id: {vaultKeepData.VaultId}!");

    VaultKeep vaultkeep = _repository.Create(vaultKeepData);
    return vaultkeep;
  }

  internal List<VaultKept> GetKeepsInVault(int vaultId, string userId)
  {
    _vaultsService.GetVaultById(vaultId, userId);

    List<VaultKept> vaultKeeps = _repository.GetKeepsInVault(vaultId);

    return vaultKeeps;
  }

  private VaultKeep GetVaultKeepById(int vaultKeepId)
  {
    VaultKeep vaultKeep = _repository.GetVaultKeepById(vaultKeepId);

    if (vaultKeep == null) throw new Exception($"Invalid VaultKeep Id");

    return vaultKeep;
  }

  internal string DeleteVaultKeep(int vaultKeepId, string userId)
  {
    VaultKeep vaultKeep = GetVaultKeepById(vaultKeepId);

    if (vaultKeep.CreatorId != userId) throw new Exception("This is not your vault keep to delete!");

    _repository.Delete(vaultKeepId);

    return $"Deleted {vaultKeep.Id}";
  }
}