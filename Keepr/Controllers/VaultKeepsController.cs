namespace Keepr.Controllers;
[Authorize]
[ApiController]
[Route("api/[controller]")]
public class VaultKeepsController : ControllerBase
{
  private readonly VaultKeepsService _vks;
  private readonly Auth0Provider _auth0provider;

  public VaultKeepsController(VaultKeepsService vks, Auth0Provider auth0Provider)
  {
    _vks = vks;
    _auth0provider = auth0Provider;
  }
  [HttpPost]
  public async Task<ActionResult<VaultKeep>> CreateVaultKeep([FromBody] VaultKeep data)
  {
    try
    {
      Account userInfo = await _auth0provider.GetUserInfoAsync<Account>(HttpContext);
      data.CreatorId = userInfo?.Id;
      VaultKeep keep = _vks.CreateVaultKeep(data);
      return Ok(keep);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }
  [HttpDelete("{id}")]
  public async Task<ActionResult<string>> DeleteVaultKeep(int id)
  {
    try
    {
      Account userInfo = await _auth0provider.GetUserInfoAsync<Account>(HttpContext);
      _vks.DeleteVaultKeep(id, userInfo?.Id);
      return Ok("You Deleted the VaultKeep");
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }
}