using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlueprintBackend.Interfaces;

namespace BlueprintBackend
{
    public class BlueprintService : IBlueprintService
    {
        /*User*/
        public Task<string> GetUsername(string userId)
        {
            throw new NotImplementedException();
        }

        public Task<string> CreateUser(string username, string email, string password)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetUserProjects(string userId)
        {
            throw new NotImplementedException();
        }

        /*Project*/

        public Task AddProjectToUser(string userId, string projectName)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetProjectData(string projectId)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetProjectName(string projectId)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetProjectFile(string projectId, string projectFile)
        {
            throw new NotImplementedException();
        }

        public Task UpdateProjectFile(string projectId, string projectFile)
        {
            throw new NotImplementedException();
        }

        public Task DeleteProject(string projectId)
        {
            throw new NotImplementedException();
        }
    }
}
