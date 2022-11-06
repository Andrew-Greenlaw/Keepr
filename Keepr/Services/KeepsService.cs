namespace Keepr.Services;
public class KeepsService
{
  private readonly KeepsRepository _repo;

  public KeepsService(KeepsRepository repo)
  {
    _repo = repo;
  }

  public Keep CreateKeep(Keep keepData)
  {
    return _repo.Create(keepData);
  }

  public List<Keep> GetAllKeeps()
  {
    return _repo.Get();
  }

  public Keep GetById(int id)
  {
    Keep keep = _repo.GetById(id);
    if (keep == null)
    {
      throw new Exception("this no longer exists");
    }
    return keep;
  }
  public void UpdateKeep(Keep k)
  {
    k.Views++;
    _repo.Update(k);
  }

  public Keep UpdateKeep(Keep keepData, string id)
  {
    Keep originalKeep = GetById(keepData.Id);
    if (originalKeep.CreatorId != id)
    {
      throw new Exception("this is not yours leave!");
    }
    originalKeep.Name = keepData.Name ?? keepData.Name;
    originalKeep.Description = keepData.Description ?? originalKeep.Description;
    originalKeep.Img = keepData.Img ?? originalKeep.Img;
    originalKeep.Views = originalKeep.Views;
    Keep updated = _repo.Update(originalKeep);
    return updated;
  }

  public void Delete(int id, string userId)
  {
    Keep keep = GetById(id);
    if (keep.CreatorId != userId)
    {
      throw new Exception("this is not yours go away");
    }
    _repo.Delete(id);
  }
}