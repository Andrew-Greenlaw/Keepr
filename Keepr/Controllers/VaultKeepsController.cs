namespace Keepr.Controllers;
[ApiController]
[Route("api/[controller]")]
public class VaultKeepsController : ControllerBase
{
  public readonly VaultKeepsService _vks;
  public readonly Auth0Provider _auth0provider;

  public VaultKeepsController(VaultKeepsService vks, Auth0Provider auth0Provider)
  {
    _vks = vks;
    _auth0provider = auth0Provider;
  }
  [HttpPost]
  [Authorize]
  public async Task<ActionResult<VaultKeep>> CreateVaultKeep([FromBody] VaultKeep data)
  {
    try
    {
      Account userInfo = await _auth0provider.GetUserInfoAsync<Account>(HttpContext);
      data.CreatorId = userInfo.Id;
      VaultKeep keep = _vks.CreateVaultKeep(data);
      return Ok(keep);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }
}