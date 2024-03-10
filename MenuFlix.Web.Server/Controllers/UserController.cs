using Auth0.ManagementApi;
using Auth0.ManagementApi.Models;
using Auth0.ManagementApi.Paging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MenuFlix.Web.Shared;
using MenuFlix.Web.Shared.Models;
using Auth0.AuthenticationApi.Models;


namespace MenuFlix.Web.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize(Roles = "Administrator")]

    public class UserController : ControllerBase
    {
        private readonly IManagementApiClient _managementApiClient;
     

        public UserController(IManagementApiClient managementApiClient)
        {
            _managementApiClient = managementApiClient;            
        }

        [HttpGet("approved")]
        public async Task<IEnumerable<UserDto.Index>> GetUsersApproved()
        {
            var users = await _managementApiClient.Roles.GetUsersAsync("rol_O1X7YVFrmhnrATqS");
            return users.Select(x => new UserDto.Index
            {
                Email = x.Email,
                FullName = x.FullName,                
                UserId = x.UserId.Split('|')[1],
                Provider = x.UserId.Split('|')[0],                
                Auth0Id = x.UserId
            });
        }

        [HttpGet("applications")]
        public async Task<IEnumerable<UserDto.Index>> GetUsersApplicants()
        {
            var users = await _managementApiClient.Roles.GetUsersAsync("rol_BB2wX2pOyF0TBKUc");
            var users_removed = await _managementApiClient.Roles.GetUsersAsync("rol_O1X7YVFrmhnrATqS");
            
            var removedUserIds = users_removed.Select(x => x.UserId);
            var request = new AssignRolesRequest
            {
                Roles = ["rol_BB2wX2pOyF0TBKUc"]
            };
            foreach (var userId in removedUserIds)
            {
                await _managementApiClient.Users.RemoveRolesAsync(userId, request);
            }
            
            // Filter users that are not in users_removed
            var filteredUsers = users.Where(x => !removedUserIds.Contains(x.UserId));

            // Create UserDto.Index objects for filtered users
            var result = filteredUsers.Select(x => new UserDto.Index
            {
                Email = x.Email,
                FullName = x.FullName,
                UserId = x.UserId.Split('|')[1],
                Provider = x.UserId.Split('|')[0],
                Auth0Id = x.UserId
            });

            return result;
        }

       

        
    }
}