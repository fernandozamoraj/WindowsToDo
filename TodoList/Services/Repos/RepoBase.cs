using System.Collections.Generic;
using TodoList.Model;
using TodoList.Services.Repos.Filters;

namespace TodoList.Services.Repos
{
    public abstract class RepoBase : ITaskRepo
    {
        private List<TodoTask> _list = new List<TodoTask>();
        private ITaskFilter _taskFilter;
        public abstract void SaveAll();
        public abstract string RepoName { get; }
        public abstract void Fill();

        protected RepoBase()
        {
            _taskFilter = ServiceLocator.GetInstance<ITaskFilter>();
        }


        public IList<TodoTask> GetUnfinishedTasks(Importance importance)
        {
            return _taskFilter.GetUnfinishedTasks(importance, GetRawList());
        }

        public IList<TodoTask> GetCompletedAndUnfinishedTasks(Importance importance)
        {
            return _taskFilter.GetCompletedAndUnfinishedTasks(importance, GetRawList());
        }

        public IList<TodoTask> GetCompletedTasks(Importance importance)
        {
            return _taskFilter.GetCompletedTasks(importance, GetRawList());
        }

        public void Add(TodoTask task)
        {
            _list.Add(task);
        }

        public IList<TodoTask> GetRawList()
        {
            return _list;
        }

        protected  void ClearList()
        {
            _list = null;
            _list = new List<TodoTask>();
        }

        protected Importance GetImportance(string importance)
        {
            if (importance == "0" || importance == "1")
                return Importance.Low;
            if (importance == "2")
                return Importance.Medium;
            if (importance == "3")
                return Importance.High;
            if (importance == "4")
                return Importance.Highest;

            return Importance.Low;
        }
    }


}