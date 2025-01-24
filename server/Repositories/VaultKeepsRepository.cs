using keepr.Interfaces;

namespace keepr.Repositories;

public class VaultKeepsRepository : IRepository<VaultKeep>
{

  public VaultKeepsRepository(IDbConnection db)
  {
    _db = db;
  }
  private readonly IDbConnection _db;


  public VaultKeep Create(VaultKeep rawData)
  {
    string sql = @"
    INSERT INTO vaultkeeps(keep_id, vault_id, creator_id)
    VALUES(@KeepId, @VaultId, @CreatorId);
    
    SELECT
    vaultkeeps.*,
    accounts.*
    FROM vaultkeeps
    JOIN accounts ON accounts.id = vaultkeeps.creator_id
    WHERE vaultkeeps.id = last_insert_id();";

    VaultKeep vaultKeep = _db.Query(sql, (VaultKeep vaultKeep, Profile account) =>
    {
      vaultKeep.CreatorId = account.Id;
      return vaultKeep;
    }, rawData).SingleOrDefault();
    return vaultKeep;
  }

  public void Delete(int id)
  {
    throw new NotImplementedException();
  }

  public List<VaultKeep> GetAll(int id)
  {
    throw new NotImplementedException();
  }

  public VaultKeep GetById(int id)
  {
    throw new NotImplementedException();
  }

  public void Update(int id)
  {
    throw new NotImplementedException();
  }
}