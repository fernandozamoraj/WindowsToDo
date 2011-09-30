using System.Collections.Generic;
using TodoList.Model;

namespace TodoList.Services.Repos
{
    public interface ITaskRepo
    {
        void Fill();
        void SaveAll();
        IList<TodoTask> GetUnfinishedTasks(Importance importance);
        IList<TodoTask> GetCompletedAndUnfinishedTasks(Importance importance);
        IList<TodoTask> GetCompletedTasks(Importance importance);
        void Add(TodoTask task);
        string RepoName { get; }
    }
}