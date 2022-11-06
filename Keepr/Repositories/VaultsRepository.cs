namespace Keepr.Repositories;

using System.Collections.Generic;
using Keepr.Interfaces;
public class VaultsRepository : BaseRepository, IRepository<Vault, int>
{
  public VaultsRepository(IDbConnection db) : base(db)
  {
  }
  public Vault Create(Vault data)
  {
    string sql = @"
    INSERT INTO vaults(name,description,img,isPrivate,creatorId)
    VALUES(@Name,@Description,@Img,@IsPrivate,@creatorId);
    SELECT LAST_INSERT_ID()
    ;";
    int vaultId = _db.ExecuteScalar<int>(sql, data);
    Vault vault = GetById(vaultId);
    return vault;
  }
  public Vault GetById(int id)
  {
    string sql = @"
    SELECT
    v.*,
    a.*
    FROM vaults v
    JOIN accounts a ON a.Id = v.creatorId
    WHERE v.id = @id
    ;";
    return _db.Query<Vault, Profile, Vault>(sql, (v, p) =>
    {
      v.Creator = p;
      return v;
    }, new { id }).FirstOrDefault();
  }

  public Vault Update(Vault data)
  {
    string sql = @"
    UPDATE vaults SET
    name = @name,
    description = @Description,
    img = @Img,
    isPrivate = IsPrivate
    WHERE id = @Id
    ;";
    int rowsEffected = _db.Execute(sql, data);
    if (rowsEffected == 0)
    {
      throw new Exception("Something went wrong nothing was changed");
    }
    return data;
  }
  public void Delete(int id)
  {
    string sql = "DELETE FROM vaults WHERE id = @Id;";
    _db.Execute(sql, new { id });
  }

  public List<Vault> Get()
  {
    throw new NotImplementedException();
  }
}