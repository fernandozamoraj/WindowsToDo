namespace TodoList.Services
{
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