namespace Keepr.Services;

public class ProfilesService
{
  private readonly ProfilesRepository _repo;

  public ProfilesService(ProfilesRepository repo)
  {
    _repo = repo;
  }

  internal Profile GetProfile(string id)
  {
    Profile profile = _repo.GetById(id);
    if (profile == null)
    {
      throw new Exception("This Profile Doesnt exist");
    }
    return profile;
  }

  internal List<Keep> GetUserKeeps(string id)
  {
    return _repo.GetKeeps(id);
  }

  internal List<Vault> GetUserVaults(string userId)
  {
    return _repo.GetUserVaults(userId);
  }
}