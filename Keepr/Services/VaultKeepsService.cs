namespace Keepr.Services;
public class VaultKeepsService
{
  public readonly VaultKeepsRepository _repo;
  public readonly VaultsService _vaultsService;

  public VaultKeepsService(VaultKeepsRepository repo, VaultsService vaultsService)
  {
    _repo = repo;
    _vaultsService = vaultsService;
  }

  internal VaultKeep CreateVaultKeep(VaultKeep data)
  {
    Vault vault = _vaultsService.GetVaultById(data.VaultId, data.CreatorId);
    if (vault.CreatorId != data.CreatorId)
    {
      throw new Exception("You Cannot Put this in someone's Vault go away!");
    }
    return _repo.Create(data);
  }
  internal VaultKeep GetById(int id)
  {
    VaultKeep vaultKeep = _repo.GetById(id);
    if (vaultKeep == null)
    {
      throw new Exception("this Vault No Longer Exists");
    }
    return vaultKeep;
  }

  internal void DeleteVaultKeep(int id, string userId)
  {
    VaultKeep vaultKeep = GetById(id);
    if (vaultKeep.CreatorId != userId)
    {
      throw new Exception("You Cannot delete this go away");
    }
    _repo.Delete(id);
  }
}