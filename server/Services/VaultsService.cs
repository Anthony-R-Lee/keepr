

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

    if (vault == null) throw new Exception($"Invalid vault id: {vaultId}");

    return vault;
  }
}