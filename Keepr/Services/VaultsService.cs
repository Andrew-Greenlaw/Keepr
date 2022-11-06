namespace Keepr.Services;
public class VaultsService
{
  private readonly VaultsRepository _repo;
  private readonly VaultKeepsRepository _vaultKeepsRepo;

  public VaultsService(VaultsRepository repo, VaultKeepsRepository vaultKeepsRepo)
  {
    _repo = repo;
    _vaultKeepsRepo = vaultKeepsRepo;
  }

  public Vault CreateVault(Vault vaultData)
  {
    return _repo.Create(vaultData);
  }

  internal Vault GetVaultById(int id, string userId)
  {
    Vault vault = _repo.GetById(id);
    if (vault == null)
    {
      throw new Exception("This vault doesn't exist anymore");
    }
    if (vault.IsPrivate == true && vault.CreatorId != userId)
    {
      throw new Exception("This Vault is Private");
    }
    return vault;
  }

  internal Vault UpdateVault(Vault vaultData, string userId)
  {
    Vault original = GetVaultById(vaultData.Id, userId);
    if (original.CreatorId != userId)
    {
      throw new Exception("this isnt yours dont update it");
    }
    original.Name = vaultData.Name;
    original.Description = vaultData.Description ?? original.Description;
    original.Img = vaultData.Img == "https://images.unsplash.com/photo-1634497885778-152eb6fd543d?ixlib=rb-4.0.3&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=1770&q=80" ? original.Img : vaultData.Img;
    original.IsPrivate = vaultData.IsPrivate;
    Vault updated = _repo.Update(original);
    return updated;
  }

  internal List<KeepInVault> GetAllKeeps(int id)
  {
    Vault vault = GetVaultById(id, "");
    return _vaultKeepsRepo.Get(id);
  }

  internal void DeleteVault(int id, Account userInfo)
  {
    Vault vault = GetVaultById(id, "");
    if (vault.CreatorId != userInfo.Id)
    {
      throw new Exception("you cannot delete this go away");
    }
    _repo.Delete(id);
  }
}