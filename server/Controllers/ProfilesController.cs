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
}