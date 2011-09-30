namespace TodoList.Services.Repos.FileRepos
{
    /// <summary>
    /// FileLocator: The purpose of this class is provide the location to a file.
    /// By doing it through a class it allows to inject the dependency instead of hard coding it.
    /// Also to support dependency injection we use an abstraction instead(the IFileLocator interface)
    /// </summary>
    public class FileLocator : IFileLocator
    {
        private string _filePath = "";

        public FileLocator(string path)
        {
            _filePath = path;
        }

        public FileLocator()
        {

        }

        public string GetFilePath()
        {
            return _filePath;
        }
    }
}