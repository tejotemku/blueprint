using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueprintBackend.Interfaces
{
    public interface IBlueprintService
    {
        /*User*/
        public Task<string> GetUsername(string userId);

        public Task<string> CreateUser(string username, string email, string password);
        
        public Task<string> GetUserProjects(string userId);

        /*Project*/
        
        public Task AddProjectToUser(string userId, string projectName);

        public Task<string> GetProjectData(string projectId);
        
        public Task<string> GetProjectName(string projectId);

        public Task<string> GetProjectFile(string projectId, string projectFile);

        public Task UpdateProjectFile(string projectId, string projectFile);

        public Task DeleteProject(string projectId);
    }
}
