using System.Collections.Generic;
using TodoList.Model;

namespace TodoList.Services.Repos.Filters
{
    public interface ITaskFilter
    {
        IList<TodoTask> GetUnfinishedTasks(Importance importance, IEnumerable<TodoTask> taskList);
        IList<TodoTask> GetCompletedAndUnfinishedTasks(Importance importance, IEnumerable<TodoTask> taskList);
        IList<TodoTask> GetCompletedTasks(Importance importance, IEnumerable<TodoTask> taskList);
    }
}