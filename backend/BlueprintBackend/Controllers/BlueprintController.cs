using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BlueprintBackend.Interfaces;
using BlueprintBackend.Models;

namespace BlueprintBackend.Controllers
{

    [Route("api")]
    [ApiController]
    public class BlueprintController : ControllerBase
    {
        private readonly IDataBase _database;

        public BlueprintController(IDataBase database)
        {
            _database = database;
        }

        [HttpPost("projects")]
        public async Task<ActionResult<int>> PostProject(CreateProjectDto request)
        {
            try
            {
                int id = _database.CreateProject(request.projectName, request.projectFile, request.username);
                return Ok(id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("projects-info")]
        public async Task<ActionResult<List<(int, string)>>> UsersProjectsInfo(string username)
        {
            try 
            {
                var data = _database.GetUsersProjectsData(username);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("projects/{projectId}")]
        public async Task<ActionResult<string>> GetProjectFile(int projectId)
        {
            try
            {
                var data = _database.GetProjectFile(projectId);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



        /*        [HttpGet("test")]
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
                }*/

        /*        [HttpPost("prescriptions/{prescriptionId}/fulfill")]
                public async Task<PrescriptionDto> FulFillPrescription(string prescriptionId)
                {
                    return await _pharmacistService.FulFillPrescription(prescriptionId);
        }*/
    }
}

