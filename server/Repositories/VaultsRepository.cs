using keepr.Interfaces;

namespace keepr.Repositories;

public class VaultsRepository : IRepository<Vault>
{

  public VaultsRepository(IDbConnection db)
  {
    _db = db;
  }
  private readonly IDbConnection _db;

  public Vault Create(Vault rawData)
  {
    string sql = @"
    INSERT INTO vaults(name, description, img, is_private, creator_id)
    VALUES(@Name, @Description, @Img, @IsPrivate, @CreatorId);

    SELECT 
    vaults.*,
    accounts.*
    FROM vaults
    JOIN accounts ON accounts.id = vaults.creator_id
    WHERE vaults.id = LAST_INSERT_ID();";

    Vault vault = _db.Query(sql, (Vault vault, Profile account) =>
    {
      vault.Creator = account;
      return vault;
    }, rawData).SingleOrDefault();
    return vault;
  }
  public Vault GetById(int id)
  {
    string sql = @"
    SELECT
    vaults.*,
    accounts.*
    FROM vaults
    JOIN accounts ON accounts.id = vaults.creator_id
    WHERE vaults.id = @id;";

    Vault vault = _db.Query(sql, (Vault vault, Profile account) =>
    {
      vault.Creator = account;
      return vault;
    }, new { id }).SingleOrDefault();
    return vault;
  }

  public void Update(Vault vaultData)
  {
    string sql = @"
    UPDATE vaults
    SET
    name = @Name,
    is_private = @IsPrivate
    WHERE id = @Id LIMIT 1;";

    int rowsAffected = _db.Execute(sql, vaultData);

    if (rowsAffected != 1) throw new Exception($"{rowsAffected} were updated");
  }

  public void Delete(int id)
  {
    string sql = "DELETE FROM vaults WHERE id = @Id LIMIT 1;";

    int rowsAffected = _db.Execute(sql, new { id });

    if (rowsAffected != 1) throw new Exception($"{rowsAffected} were deleted");
  }

  internal List<Vault> GetMyVaults()
  {
    string sql = @"
      SELECT 
      vaults.*,
      accounts.* 
      FROM vaults
      JOIN accounts ON accounts.id = vaults.creator_id
      WHERE vaults.creator_id = accounts.id;";

    List<Vault> vaults = _db.Query(sql, (Vault vault, Profile account) =>
    {
      vault.Creator = account;
      return vault;
    }).ToList();
    return vaults;
  }

  public void Update(int id)
  {
    throw new NotImplementedException();
  }
  public List<Vault> GetAll(int id)
  {
    throw new NotImplementedException();
  }

}