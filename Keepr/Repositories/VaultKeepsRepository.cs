namespace Keepr.Repositories;

public class VaultKeepsRepository : BaseRepository
{
  public VaultKeepsRepository(IDbConnection db) : base(db)
  {
  }
  public VaultKeep Create(VaultKeep data)
  {
    string sql = @"
    INSERT INTO VaultKeeps(creatorId,keepId,vaultId)
    VALUES(@CreatorId,@KeepId,@VaultId);
    SELECT LAST_INSERT_ID()
    ;";
    int id = _db.Execute(sql, data);
    VaultKeep vaultKeep = GetById(id);
    return vaultKeep;
  }
  public VaultKeep GetById(int id)
  {
    string sql = @"
    SELECT * FROM vaultKeeps
    WHERE id = @id
    ;";
    return _db.Query<VaultKeep>(sql, new { id }).FirstOrDefault();
  }
  // public KeepInVault GetVaultKeepById(int id){
  //   string sql = @"
  //   SELECT
  //   vk.*,
  //   k.*,
  //   v.*
  //   FROM vaultKeeps vk
  //   JOIN keeps k ON k.id = vk.keepId
  //   JOIN vaults v ON v.id = vk.vaultId
  //   WHERE id = @id
  //   ;";
  //   return _db.Query<VaultKeep,KeepInVault,Vault,KeepInVault>(sql,(vk,k,v)=>{
  //     k.vaultKeepId = vk.Id;
  //     v.
  //   })
  // }
}