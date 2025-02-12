using System.Threading.Tasks;

namespace keepr.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProfilesController : ControllerBase
{

  public ProfilesController(ProfilesService profilesService, Auth0Provider auth0Provider)
  {
    _profilesService = profilesService;
    _auth0Provider = auth0Provider;
  }
  private readonly ProfilesService _profilesService;
  private readonly Auth0Provider _auth0Provider;

  [HttpGet("{profileId}")]
  public ActionResult<Profile> GetUserProfile(string profileId)
  {
    try
    {
      Profile profile = _profilesService.GetUserProfile(profileId);
      return Ok(profile);
    }
    catch (Exception exception)
    {
      return BadRequest(exception.Message);
    }
  }

  [HttpGet("{profileId}/keeps")]
  public ActionResult<List<Keep>> GetUserKeeps(string profileId)
  {
    try
    {
      List<Keep> keeps = _profilesService.GetUserKeeps(profileId);
      return Ok(keeps);
    }
    catch (Exception exception)
    {
      return BadRequest(exception.Message);
    }
  }

  [HttpGet("{profileId}/vaults")]
  public async Task<ActionResult<List<Vault>>> GetUserVaults(string profileId)
  {
    try
    {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      List<Vault> vaults = _profilesService.GetUserVaults(profileId, userInfo?.Id);
      return Ok(vaults);
    }
    catch (Exception exception)
    {
      return BadRequest(exception.Message);
    }
  }

}