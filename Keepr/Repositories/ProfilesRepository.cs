namespace Keepr.Repositories;
public class ProfilesRepository : BaseRepository
{
  public ProfilesRepository(IDbConnection db) : base(db)
  {
  }

  internal Profile GetById(string id)
  {
    string sql = @"
    SELECT
    *
    FROM accounts
    WHERE id = @id
    ;";
    return _db.Query<Profile>(sql, new { id }).FirstOrDefault();
  }

  internal List<Keep> GetKeeps(string id)
  {
    string sql = @"
    SELECT
    k.*,
    a.*
    FROM keeps k
    JOIN accounts a ON a.Id = k.CreatorId
    WHERE k.CreatorId = @id
    ;";
    return _db.Query<Keep, Profile, Keep>(sql, (k, p) =>
    {
      k.Creator = p;
      return k;
    }, new { id }).ToList();
  }

  internal List<Vault> GetUserVaults(string userId)
  {
    string sql = @"
    SELECT
    v.*,
    a.*
    FROM vaults v
    JOIN accounts a ON a.Id = v.CreatorId
    WHERE v.CreatorId = @userId AND v.IsPrivate = 0;
    ;";
    return _db.Query<Vault, Profile, Vault>(sql, (v, p) =>
    {
      v.Creator = p;
      return v;
    }, new { userId }).ToList();
  }
}