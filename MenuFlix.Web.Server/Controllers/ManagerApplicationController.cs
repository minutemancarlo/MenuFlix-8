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

    public class ManagerApplicationController: ControllerBase
    {
        private readonly IManagementApiClient _managementApiClient;

        public ManagerApplicationController(IManagementApiClient managementApiClient)
        {
            _managementApiClient = managementApiClient;
        }


        [HttpPost]
        public async Task<ActionResult> UpdateOwner(string[] owner)
        {
            try
            {
                AssignUsersRequest assignUsersRequest = new AssignUsersRequest();   
                assignUsersRequest.Users = owner;
                await _managementApiClient.Roles.AssignUsersAsync("rol_2sspXXoOPDR0bMiF", assignUsersRequest);
                return Ok("Owner updated successfully.");
        }
            catch (Exception ex)
            {                
                return StatusCode(500, $"Error updating owner: {ex.Message}");
    }
}
    }
}