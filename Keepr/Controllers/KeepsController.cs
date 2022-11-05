namespace Keepr.Controllers;
[ApiController]
[Route("api/[controller]")]
public class KeepsController : ControllerBase
{
  private readonly KeepsService _ks;
  private readonly Auth0Provider _auth0provider;

  public KeepsController(KeepsService ks, Auth0Provider auth0provider)
  {
    _ks = ks;
    _auth0provider = auth0provider;
  }
  [HttpPost]
  [Authorize]

  public async Task<ActionResult<Keep>> CreateKeep([FromBody] Keep keepData)
  {
    try
    {
      Account userInfo = await _auth0provider.GetUserInfoAsync<Account>(HttpContext);
      keepData.CreatorId = userInfo.Id;
      Keep keep = _ks.CreateKeep(keepData);
      return Ok(keep);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }
  [HttpGet]
  public ActionResult<List<Keep>> GetAllKeeps()
  {
    try
    {
      List<Keep> keeps = _ks.GetAllKeeps();
      return Ok(keeps);

    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }
  [HttpGet("{id}")]
  public ActionResult<Keep> GetKeepById(int id)
  {
    try
    {
      Keep keep = _ks.GetById(id);
      return Ok(keep);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }
  [HttpPut("{id}")]
  [Authorize]
  public async Task<ActionResult<Keep>> UpdateKeep([FromBody] Keep keepData, int id)
  {
    try
    {
      Account userInfo = await _auth0provider.GetUserInfoAsync<Account>(HttpContext);
      keepData.Id = id;
      Keep keep = _ks.UpdateKeep(keepData, userInfo?.Id);
      return Ok(keep);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }
  [HttpDelete("{id}")]
  [Authorize]
  public async Task<ActionResult<string>> DeleteKeep(int id)
  {
    try
    {
      Account userInfo = await _auth0provider.GetUserInfoAsync<Account>(HttpContext);
      _ks.Delete(id, userInfo?.Id);
      return Ok("You Deleted Successfully!");
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }
}