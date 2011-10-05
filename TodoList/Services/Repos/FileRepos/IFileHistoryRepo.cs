using System.Collections.Generic;

namespace TodoList.Services.Repos.FileRepos
{
    public interface IFileHistoryRepo
    {
        IList<string> FileNames { get; }
        string LastFileAccessed { get; }
        void AddFileName(string fileName);
        void Save();
    }
}