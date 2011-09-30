namespace TodoList.Services
{
    public class FileLocator : IFileLocator
    {
        private string _filePath = "tasks.data";

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