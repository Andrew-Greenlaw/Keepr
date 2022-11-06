namespace Keepr.Controllers;
[ApiController]
[Route("api/[controller]")]
public class ProfilesController : ControllerBase
{
  private readonly ProfilesService _ps;
  private readonly Auth0Provider _auth0provider;

  public ProfilesController(ProfilesService ps, Auth0Provider auth0provider)
  {
    _ps = ps;
    _auth0provider = auth0provider;
  }
  [HttpGet("{id}")]
  public ActionResult<Profile> GetProfile(string id)
  {
    try
    {
      Profile profile = _ps.GetProfile(id);

      return Ok(profile);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }
  [HttpGet("{userId}/keeps")]
  public ActionResult<List<Keep>> GetUserKeeps(string userId)
  {
    try
    {
      List<Keep> keeps = _ps.GetUserKeeps(userId);
      return Ok(keeps);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }
  [HttpGet("{userId}/vaults")]
  public ActionResult<List<Vault>> GetUserVaults(string userId)
  {
    try
    {
      List<Vault> vaults = _ps.GetUserVaults(userId);
      return Ok(vaults);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }
}