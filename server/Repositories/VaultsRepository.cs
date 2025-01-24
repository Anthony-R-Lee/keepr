using keepr.Interfaces;

namespace keepr.Repositories;

public class VaultsRepository : IRepository<Vault>
{

  public VaultsRepository(IDbConnection db)
  {
    _db = db;
  }
  private readonly IDbConnection _db;

  public Vault GetById(int id)
  {
    throw new NotImplementedException();
  }

  public List<Vault> GetAll(int id)
  {
    throw new NotImplementedException();
  }

  public Vault Create(Vault rawData)
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