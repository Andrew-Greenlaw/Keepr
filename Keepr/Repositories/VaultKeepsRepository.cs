namespace Keepr.Repositories;

public class VaultKeepsRepository : BaseRepository
{
  public VaultKeepsRepository(IDbConnection db) : base(db)
  {
  }
  public VaultKeep Create(VaultKeep data)
  {
    string sql = @"
    INSERT INTO vaultKeeps(creatorId,keepId,vaultId)
    VALUES(@CreatorId,@KeepId,@VaultId);
    SELECT LAST_INSERT_ID()
    ;";
    int id = _db.ExecuteScalar<int>(sql, data);
    return GetById(id);

  }
  public VaultKeep GetById(int id)
  {
    string sql = @"
    SELECT * FROM vaultKeeps
    WHERE id = @id
    ;";
    return _db.Query<VaultKeep>(sql, new { id }).FirstOrDefault();
  }


  public List<KeepInVault> Get(int id)
  {
    string sql = @"
    SELECT
    vk.*,
    k.*,
    a.*
    FROM vaultKeeps vk
    JOIN keeps k ON k.Id = vk.KeepId
    JOIN accounts a ON a.Id = vk.CreatorId
    WHERE vk.VaultId = @id
    ;";
    return _db.Query<VaultKeep, KeepInVault, Profile, KeepInVault>(sql, (vk, k, p) =>
    {
      k.Creator = p;
      k.VaultKeepId = vk.Id;
      return k;
    }, new { id }).ToList();
  }
  internal void Delete(int id)
  {
    string sql = "DELETE FROM vaultKeeps WHERE Id = @id;";
    _db.Execute(sql, new { id });
  }
}