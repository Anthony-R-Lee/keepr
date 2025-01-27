
using Dapper;

namespace keepr.Repositories;

public class ProfilesRepository
{

  public ProfilesRepository(IDbConnection db)
  {
    _db = db;
  }
  private readonly IDbConnection _db;

  internal Profile GetUserProfile(string profileId)
  {
    string sql = @"
    SELECT
    *
    FROM accounts
    WHERE id = @profileId;";

    Profile profile = _db.Query<Profile>(sql, new { profileId }).SingleOrDefault();
    return profile;
  }

  internal List<Keep> GetUserKeeps(string profileId)
  {
    string sql = @"
      SELECT 
      keeps.*,
      accounts.*
      FROM keeps
      JOIN accounts ON accounts.id = keeps.creator_id
      WHERE accounts.id = @profileId;
      ";

    List<Keep> keeps = _db.Query(sql, (Keep keep, Profile account) =>
    {
      keep.Creator = account;
      return keep;
    }, new { profileId }).ToList();
    return keeps;
  }
}