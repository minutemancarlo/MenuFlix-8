using Auth0.ManagementApi;
using Auth0.ManagementApi.Models;
using Auth0.ManagementApi.Paging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MenuFlix.Web.Shared;
using System.Data.SqlClient;
using Dapper;
using MenuFlix.Web.Shared.Models;
using System.Data;
using System.Threading;
using MenuFlix.Web.Server.Manager;

namespace MenuFlix.Web.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize(Roles = "Administrator")]

    public class ValuesController : ControllerBase
    {
        Connections _configuration = new Connections();

        [HttpPost("merge")]
        public async Task<IActionResult> MergeUserInfo([FromBody] string uid)
        {
            try
            {
                var dynamicParam = new DynamicParameters();

                dynamicParam.Add("@UserId", uid);
                using var sqlConnection = new SqlConnection(_configuration.DefaultConnectionString);
                await sqlConnection.OpenAsync();
                using var tran = await sqlConnection.BeginTransactionAsync();
                try
                {
                    _ = await sqlConnection.ExecuteAsync("usp_MergeUserId", dynamicParam, tran,
                        commandType: CommandType.StoredProcedure, commandTimeout: 3000);

                    await tran.CommitAsync();
                    return Ok("Ok");
                }
                catch (Exception ex)
                {
                    await tran.RollbackAsync();
                    return StatusCode(500, $"Error updating owner: {ex.Message}");
                }

            } catch (Exception ex)
            {
                return StatusCode(500, $"Error updating owner: {ex.Message}");
            }
        }

        [HttpPost("checkexist")]
        public async Task<IActionResult> Post([FromBody] string userId)
        {
            try
            {
                using var sqlConnection = new SqlConnection(_configuration.DefaultConnectionString);
                await sqlConnection.OpenAsync();
                using var tran = await sqlConnection.BeginTransactionAsync();
                try
                {
                    var result = await sqlConnection.QueryAsync("SELECT count(*) as count FROM UserInfo WHERE UserId = @UserId", new { UserId = userId }, tran, commandTimeout: 3000, commandType: CommandType.Text);
                    await tran.CommitAsync();
                    return Ok(result);
                }
                catch (Exception ex)
                {
                    await tran.RollbackAsync();
                    return StatusCode(500, $"Error retrieving user information: {ex.Message}");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error retrieving user information: {ex.Message}");
            }
        }


    }
}