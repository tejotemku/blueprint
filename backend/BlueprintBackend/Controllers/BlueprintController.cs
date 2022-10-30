using Microsoft.AspNetCore.Mvc;
using BlueprintBackend.Interfaces;
using BlueprintBackend.Models;
using BlueprintBackend.Utils;
using Microsoft.AspNetCore.Authorization;

namespace BlueprintBackend.Controllers;
[Route("api")]
[ApiController]
public class BlueprintController : ControllerBase
{
    private readonly IDataBase _database;
    private readonly BlueprintUtils _utils;

    public BlueprintController(IDataBase database, BlueprintUtils utils)
    {
        _database = database;
        _utils = utils;
    }

    [HttpPost("projects"), Authorize]
    public async Task<ActionResult<int>> PostProject(CreateProjectDto request)
    {
        try
        {
            _utils.AuthenticateToken(
               Request.Headers.Authorization,
               out string tokenUsername,
               out string tokenEmail);
            if (tokenUsername != request.username)
                throw new Exception("User credentials do not match creator");
            int id = _database.CreateProject(request.projectName, request.projectFile, request.username);
            return Ok(id);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("projects-info/{username}"), Authorize]
    public async Task<ActionResult<List<ProjectInfoDto>>> UsersProjectsInfo(string username)
    {
        try
        {
            _utils.AuthenticateToken(
               Request.Headers.Authorization,
               out string tokenUsername,
               out string tokenEmail);
            if (tokenUsername != username)
                throw new Exception("User credentials do not match creator");
            var data = _database.GetUsersProjectsData(username);
            return Ok(data);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("projects/{projectId}/file"), Authorize]
    public async Task<ActionResult<string>> GetProjectFile(int projectId)
    {
        try
        {
            _utils.AuthenticateToken(
               Request.Headers.Authorization,
               out string tokenUsername,
               out string tokenEmail);
            if (tokenUsername != _database.GetProjectOwner(projectId))
                throw new Exception("User credentials do not match creator");
            var data = _database.GetProjectFile(projectId);
            return Ok(data);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("projects/{projectId}/file"), Authorize]
    public async Task<ActionResult> UpdateProjectFile(int projectId, string projectFile)
    {
        try
        {
            _utils.AuthenticateToken(
               Request.Headers.Authorization,
               out string tokenUsername,
               out string tokenEmail);
            if (tokenUsername != _database.GetProjectOwner(projectId))
                throw new Exception("User credentials do not match creator");
            _database.UpdateProjectFile(projectId, projectFile);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("projects/{projectId}"), Authorize]
    public async Task<ActionResult> DeleteProject(int projectId)
    {
        try
        {
            _utils.AuthenticateToken(
               Request.Headers.Authorization,
               out string tokenUsername,
               out string tokenEmail);
            if (tokenUsername != _database.GetProjectOwner(projectId))
                throw new Exception("User credentials do not match creator");
            _database.DeleteProject(projectId);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }




/*      
    [HttpGet("test")]
    public string GetTest()
    {
        return "Ok :)";
    }


    [HttpGet("test-token")]
    public Dictionary<string, string> GetTokenTest()
    {
        Dictionary<string, string> requestHeaders = new();
        foreach (var header in Request.Headers)
        {
            if(header.Key == "Authorization") 
                requestHeaders.Add(header.Key, header.Value);
        }
        return requestHeaders;
    }
*/
}

