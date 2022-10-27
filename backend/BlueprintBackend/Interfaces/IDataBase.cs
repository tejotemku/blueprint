namespace BlueprintBackend.Interfaces
{
    public interface IDataBase
    {
        public void InsertUser(string username, string email, string passwordHash, string passwordSalt);

        public void InsertProject(string projectName, string projectFile, string userID);

        public (string, string) GetUserPaswordHashAndSalt(string username);

        public string GetUserEmail(string username);

        public List<(string, DateTime)> GetUsersProjectsData(string userId);
    }
}
