namespace Keepr.Controllers;
[ApiController]
[Route("api/{controller}")]
public class VaultsController : ControllerBase
{
  private readonly VaultsService _vs;
  private readonly Auth0Provider _auth0provider;

  public VaultsController(VaultsService vs, Auth0Provider auth0provider)
  {
    _vs = vs;
    _auth0provider = auth0provider;
  }
  [HttpPost]
  [Authorize]
  public async Task<ActionResult<Vault>> CreateVault([FromBody] Vault vaultData)
  {
    try
    {
      Account userInfo = await _auth0provider.GetUserInfoAsync<Account>(HttpContext);
      vaultData.CreatorId = userInfo?.Id;
      Vault vault = _vs.CreateVault(vaultData);
      return Ok(vault);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }
  [HttpGet("{id}/keeps")]
  public ActionResult<List<KeepInVault>> GetAllKeepsInVault(int id)
  {
    try
    {
      List<KeepInVault> keepsInVault = _vs.GetAllKeeps(id);
      return Ok(keepsInVault);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }
  [HttpGet("{id}")]
  public ActionResult<Vault> GetVaultById(int id)
  {
    try
    {
      Vault vault = _vs.GetVaultById(id, "");
      return Ok(vault);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [HttpPut("{id}")]
  [Authorize]
  public async Task<ActionResult<Vault>> UpdateVault([FromBody] Vault data, int id)
  {
    try
    {
      Account userInfo = await _auth0provider.GetUserInfoAsync<Account>(HttpContext);
      data.Id = id;
      Vault vault = _vs.UpdateVault(data, userInfo.Id);
      return Ok(vault);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }
  [HttpDelete("{id}")]
  [Authorize]
  public async Task<ActionResult<string>> DeleteVault(int id)
  {
    try
    {
      Account userInfo = await _auth0provider.GetUserInfoAsync<Account>(HttpContext);
      _vs.DeleteVault(id, userInfo);
      return Ok("Deleted Vault");
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }
}