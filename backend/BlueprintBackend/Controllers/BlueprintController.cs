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
            if (tokenUsername != request.owner)
                throw new Exception("User credentials do not match creator");
            int id = _database.CreateProject(request.name, request.file, request.description, request.owner);
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
    public async Task<ActionResult> UpdateProjectFile(int projectId, UpdateProjetFileDto dto)
    {
        try
        {
            _utils.AuthenticateToken(
               Request.Headers.Authorization,
               out string tokenUsername,
               out string tokenEmail);
            if (tokenUsername != _database.GetProjectOwner(projectId))
                throw new Exception("User credentials do not match creator");
            _database.UpdateProjectFile(projectId, dto.file);
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
}

