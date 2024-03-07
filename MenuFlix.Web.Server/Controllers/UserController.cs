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

    public class UserController : ControllerBase
    {
        private readonly IManagementApiClient _managementApiClient;

        public UserController(IManagementApiClient managementApiClient)
        {
            _managementApiClient = managementApiClient;
        }

        [HttpGet]
        public async Task<IEnumerable<UserDto.Index>> GetUsers()
        {
            var users = await _managementApiClient.Users.GetAllAsync(new GetUsersRequest(), new PaginationInfo());
            return users.Select(x => new UserDto.Index
            {
                Email = x.Email,
                FullName = x.FullName,
                Blocked = x.Blocked ?? false,
                UserId = x.UserId.Split('|')[1],
                Provider = x.UserId.Split('|')[0],
                LastLogin = x.LastLogin
            });
        }
    }
}