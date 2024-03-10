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
        


        [HttpPost]
        public async Task<IActionResult> MergeUserInfo([FromBody] string uid)
        {


            //using var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            //var result = await connection.ExecuteAsync("INSERT INTO UserInfo (UserId) VALUES (@info)", info);
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
                    return Ok($"Received UID: {uid}");
                }
                catch (Exception ex)
                {
                    await tran.RollbackAsync();
                    return StatusCode(500, $"Error updating owner: {ex.Message}");
                }

            }catch (Exception ex)
            {
                return StatusCode(500, $"Error updating owner: {ex.Message}");
            }
        }
       
    }
}