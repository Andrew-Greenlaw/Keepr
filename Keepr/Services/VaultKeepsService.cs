namespace Keepr.Services;
public class VaultKeepsService
{
  public readonly VaultKeepsRepository _repo;

  public VaultKeepsService(VaultKeepsRepository repo)
  {
    this._repo = repo;
  }

  internal VaultKeep CreateVaultKeep(VaultKeep data)
  {
    return _repo.Create(data);
  }
}