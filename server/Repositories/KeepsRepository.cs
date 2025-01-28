using keepr.Interfaces;

namespace keepr.Repositories;

public class KeepsRepository : IRepository<Keep>
{

  public KeepsRepository(IDbConnection db)
  {
    _db = db;
  }
  private readonly IDbConnection _db;

  public Keep Create(Keep keepData)
  {
    string sql = @"
      INSERT INTO keeps(name, description, img, creator_id)
      VALUES(@Name, @Description, @Img, @CreatorId);
      
      SELECT
      keeps.*,
      accounts.*
      FROM keeps
      JOIN accounts ON accounts.id = keeps.creator_id
      WHERE keeps.id = LAST_INSERT_ID()";

    Keep keep = _db.Query(sql, (Keep keep, Profile account) =>
    {
      keep.Creator = account;
      return keep;
    }, keepData).SingleOrDefault();

    return keep;
  }

  public List<Keep> GetAll()
  {
    string sql = @"
      SELECT 
      keeps.*,
      accounts.* 
      FROM keeps
      JOIN accounts ON accounts.id = keeps.creator_id;";

    List<Keep> keeps = _db.Query(sql, (Keep keep, Profile account) =>
    {
      keep.Creator = account;
      return keep;
    }).ToList();
    return keeps;
  }

  public Keep GetById(int id)
  {
    string sql = @"
      SELECT
      keeps.*,
      accounts.*
      FROM keeps
      JOIN accounts on accounts.id = keeps.creator_id
      WHERE keeps.id = @id;";

    Keep keep = _db.Query(sql, (Keep keep, Profile Account) =>
    {
      keep.Creator = Account;
      return keep;
    }, new { id }).SingleOrDefault();
    return keep;
  }

  public void Update(Keep updateData)
  {
    string sql = @"
    UPDATE keeps
    SET
    name = @Name,
    description = @Description
    WHERE id = @Id LIMIT 1;";

    int rowsAffected = _db.Execute(sql, updateData);

    if (rowsAffected != 1) throw new Exception($"{rowsAffected} were updated");
  }

  public void Delete(int id)
  {
    string sql = "DELETE FROM keeps WHERE id = @id LIMIT 1;";

    int rowsAffected = _db.Execute(sql, new { id });

    if (rowsAffected != 1) throw new Exception($"{rowsAffected} were deleted");
  }

  public List<Keep> GetAll(int id)
  {
    throw new NotImplementedException();
  }

  public void Update(int id)
  {
    throw new NotImplementedException();
  }

  internal void IncrementVisits(Keep keep)
  {
    string sql = @"
    UPDATE keeps
    SET views = @Views
    WHERE id = @Id
    LIMIT 1;";
    int rowsAffected = _db.Execute(sql, keep);
    if (rowsAffected != 1) throw new Exception($"{rowsAffected} were updated");
  }
}