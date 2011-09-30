using System.Collections.Generic;
using TodoList.Model;

namespace TodoList.Services.Repos.FileRepos
{
    /// <summary>
    /// FileRepo: This is the base class for task reposiotries.
    /// There are some commonalities betwee all the different file repositories.
    /// Among these are the concep of relying on a file locator.
    /// Other types of repo such as database type repos do not depend on files.
    /// As such they should not inherit from FileRepo
    /// </summary>
    public abstract class FileRepo : RepoBase, ITaskRepo
    {
        protected IFileLocator _fileLocator;
        
        protected FileRepo(IFileLocator fileLocator)
        {
            _fileLocator = fileLocator;
        }

        protected FileRepo(IFileLocator fileLocator, IEnumerable<TodoTask> tasks )
        {
            _fileLocator = fileLocator;

            foreach (var task in tasks)
                Add(task);
        }

        public override string RepoName
        {
            get { return _fileLocator.GetFilePath(); }
        }
    }
}