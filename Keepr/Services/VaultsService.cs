namespace Keepr.Services;
public class VaultsService
{
  public readonly VaultsRepository _repo;

  public VaultsService(VaultsRepository repo)
  {
    _repo = repo;
  }

  public Vault CreateVault(Vault vaultData)
  {
    return _repo.Create(vaultData);
  }

  internal Vault GetVaultById(int id)
  {
    Vault vault = _repo.GetById(id);
    if (vault == null)
    {
      throw new Exception("This vault doesn't exist anymore");
    }
    return vault;
  }

  internal Vault UpdateVault(Vault vaultData, string userId)
  {
    Vault original = GetVaultById(vaultData.Id);
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

  internal void DeleteVault(int id, Account userInfo)
  {
    Vault vault = GetVaultById(id);
    if (vault.CreatorId != userInfo.Id)
    {
      throw new Exception("you cannot delete this go away");
    }
    _repo.Delete(id);
  }
}