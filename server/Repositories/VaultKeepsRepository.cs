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

  internal List<VaultKept> GetKeepsInVault(int vaultId)
  {
    string sql = @"
      SELECT
      keeps.*,
      vaultkeeps.*,
      accounts.*
      FROM vaultkeeps
      JOIN keeps ON keeps.id = vaultkeeps.keep_id
      JOIN accounts ON accounts.id = keeps.creator_id
      WHERE vaultkeeps.vault_id = @vaultId;";

    List<VaultKept> vaultKepts = _db.Query(sql, (Keep keep, VaultKept vaultKept, Profile account) =>
    {
      vaultKept.CreatorId = keep.CreatorId;
      vaultKept.VaultKeepId = vaultKept.Id;
      vaultKept.Name = keep.Name;
      vaultKept.Description = keep.Description;
      vaultKept.Img = keep.Img;
      vaultKept.Id = keep.Id;
      vaultKept.Kept = keep.Kept;
      vaultKept.Creator = account;
      return vaultKept;
    }, new { vaultId }).ToList();

    return vaultKepts;
  }

  internal VaultKeep GetVaultKeepById(int vaultKeepId)
  {
    string sql = "SELECT * FROM vaultkeeps WHERE id = @vaultKeepId;";

    VaultKeep vaultKeep = _db.Query<VaultKeep>(sql, new { vaultKeepId }).SingleOrDefault();

    return vaultKeep;
  }
  public void Delete(int id)
  {
    string sql = "DELETE FROM vaultkeeps WHERE id = @id LIMIT 1;";

    int rowsAffected = _db.Execute(sql, new { id });

    if (rowsAffected != 1) throw new Exception($"{rowsAffected} were deleted");
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