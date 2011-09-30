using System.Collections.Generic;
using TodoList.Model;

namespace TodoList.Services.Repos.Filters
{
    public class TaskFilter : ITaskFilter
    {
        public IList<TodoTask> GetUnfinishedTasks(Importance importance, IEnumerable<TodoTask> taskList)
        {
            IList<TodoTask> filteredTasks = new List<TodoTask>();

            foreach (TodoTask todoTask in taskList)
            {
                if (((todoTask.Importance & importance) == todoTask.Importance) &&
                    !todoTask.IsCompleted)
                {
                    filteredTasks.Add(todoTask);
                }
            }

            return filteredTasks;
        }

        public IList<TodoTask> GetCompletedAndUnfinishedTasks(Importance importance, IEnumerable<TodoTask> taskList)
        {
            IList<TodoTask> filteredTasks = new List<TodoTask>();

            foreach (TodoTask todoTask in taskList)
            {
                if (((todoTask.Importance & importance) == todoTask.Importance))
                {
                    filteredTasks.Add(todoTask);
                }
            }

            return filteredTasks;
        }

        public IList<TodoTask> GetCompletedTasks(Importance importance, IEnumerable<TodoTask> taskList)
        {
            IList<TodoTask> filteredTasks = new List<TodoTask>();

            foreach (TodoTask todoTask in taskList)
            {
                if (((todoTask.Importance & importance) == todoTask.Importance) &&
                    todoTask.IsCompleted)
                {
                    filteredTasks.Add(todoTask);
                }
            }

            return filteredTasks;
        }
    }
}
