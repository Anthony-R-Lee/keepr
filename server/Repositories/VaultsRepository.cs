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
    INSERT INTO vaults(name, description, img, creator_id)
    VALUES(@Name, @Description, @Img, @CreatorId);

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
    throw new NotImplementedException();
  }

  public List<Vault> GetAll(int id)
  {
    throw new NotImplementedException();
  }


  public void Update(int id)
  {
    throw new NotImplementedException();
  }

  public void Delete(int id)
  {
    throw new NotImplementedException();
  }
}