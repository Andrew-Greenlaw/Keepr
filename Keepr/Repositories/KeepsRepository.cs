namespace Keepr.Repositories;

using System.Collections.Generic;
using System.Data;
using Keepr.Interfaces;
public class KeepsRepository : BaseRepository, IRepository<Keep, int>
{
  public KeepsRepository(IDbConnection db) : base(db)
  {
  }

  public Keep Create(Keep data)
  {
    string sql = @"
    INSERT INTO keeps(name,description,img,creatorId)
    VALUES(@Name,@Description,@Img,@CreatorId);
    SELECT LAST_INSERT_ID()
    ;";
    int keepId = _db.ExecuteScalar<int>(sql, data);
    Keep keep = GetById(keepId);
    return keep;

  }

  public void Delete(int id)
  {
    string sql = "DELETE FROM keeps WHERE Id = @id;";
    _db.Execute(sql, new { id });
  }

  public List<Keep> Get()
  {
    string sql = @"
    SELECT
    k.*,
    a.*
    FROM keeps k
    JOIN accounts a ON a.id = k.creatorId
    ;";
    return _db.Query<Keep, Profile, Keep>(sql, (k, p) =>
    {
      k.Creator = p;
      return k;
    }).ToList();
  }

  public Keep GetById(int id)
  {
    string sql = @"
    SELECT
    k.*,
    Count(k.Id) As Kept,
    a.*
    FROM keeps k
    JOIN accounts a ON a.Id = k.creatorId
    WHERE k.id = @id
    Group By k.Id
    ;";
    return _db.Query<Keep, Profile, Keep>(sql, (k, p) =>
    {
      k.Creator = p;
      return k;
    }, new { id }).FirstOrDefault();
  }

  public Keep Update(Keep data)
  {
    string sql = @"
    UPDATE keeps SET
    name = @Name,
    description = @Description,
    img = @Img,
    views = @Views
    WHERE Id = @Id
    ;";
    int rowsEffected = _db.Execute(sql, data);
    if (rowsEffected == 0)
    {
      throw new Exception("Something went wrong nothing was changed");
    }
    return data;
  }
}