using Auth0.ManagementApi;
using Auth0.ManagementApi.Models;
using Auth0.ManagementApi.Paging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MenuFlix.Web.Shared;

namespace MenuFlix.Web.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize(Roles = "Administrator")]

    public class ApplicationsController: ControllerBase
    {
        private readonly IManagementApiClient _managementApiClient;

        public ApplicationsController(IManagementApiClient managementApiClient)
        {
            _managementApiClient = managementApiClient;
        }


        [HttpPost("updateowner")]
        public async Task<ActionResult> UpdateOwner(string[] owner)
        {
            try
            {
                

                AssignUsersRequest assignUsersRequest = new AssignUsersRequest();
                assignUsersRequest.Users = owner;
                await _managementApiClient.Roles.AssignUsersAsync("rol_O1X7YVFrmhnrATqS", assignUsersRequest);
                return Ok("Owner updated successfully.");
        }
            catch (Exception ex)
            {                
                return StatusCode(500, $"Error updating owner: {ex.Message}");
    }
}
    }
}