using System;
using System.Collections.Generic;
using System.IO;
using TodoList.Model;

namespace TodoList.Services
{
    public class FlatFileRepo : ITaskRepo
    {
        private List<TodoTask> _list;
        private IFileLocator _fileLocator;

        public FlatFileRepo(IFileLocator fileLocator)
        {
            _fileLocator = fileLocator;
        }

        public FlatFileRepo(IFileLocator fileLocator, FlatFileRepo repoToClone)
        {
            _fileLocator = fileLocator;
            _list = repoToClone._list;
        }

        public void Fill()
        {
             _list = new List<TodoTask>();

            try
            {
                if (File.Exists(_fileLocator.GetFilePath()))
                {
                    using (StreamReader reader = new StreamReader(_fileLocator.GetFilePath()))
                    {
                        while (reader.Peek() != -1)
                        {
                            string line = reader.ReadLine();

                            string[] items = line.Split(',');

                            TodoTask task = new TodoTask();

                            DateTime dateEntered;
                            DateTime.TryParse(items[0], out dateEntered);
                            DateTime dateCompleted;
                            DateTime.TryParse(items.Length > 3 ? items[3] : items[0], out dateCompleted);

                            task.DateEntered = dateEntered;
                            task.DateCompleted = dateCompleted;
                            task.Description = items[1];
                            task.Importance = GetImportance(items.Length > 2 ? items[2] : "1");
                            task.Notes = items.Length > 4 ? items[4] : string.Empty;

                            _list.Add(task);

                        }
                    }

                }

            }
            catch (Exception exception)
            {
                throw new Exception("Failed while reading: " + _fileLocator.GetFilePath(), exception);
            }
        }

        private Importance GetImportance(string importance)
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

        public void SaveAll()
        {
            try
            {
                if (!string.IsNullOrEmpty(_fileLocator.GetFilePath()))
                {
                    using (StreamWriter writer = new StreamWriter(_fileLocator.GetFilePath()))
                    {
                        foreach (TodoTask todoTask in _list)
                        {
                            writer.WriteLine("{0},{1},{2},{3},{4}", todoTask.DateEntered, todoTask.Description,
                                             todoTask.Importance, todoTask.DateCompleted, todoTask.Notes);
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                throw new ApplicationException("Failed to save to: " + _fileLocator.GetFilePath(), exception);
            }

        }

        public IList<TodoTask> GetUnfinishedTasks(Importance importance)
        {
            IList<TodoTask> filteredTasks = new List<TodoTask>();

            foreach (TodoTask todoTask in _list)
            {
                if (((todoTask.Importance & importance) == todoTask.Importance) &&
                    !todoTask.IsCompleted)
                {
                    filteredTasks.Add(todoTask);
                }
            }

            return filteredTasks;
        }

        public IList<TodoTask> GetCompletedAndUnfinishedTasks(Importance importance)
        {
            IList<TodoTask> filteredTasks = new List<TodoTask>();

            foreach (TodoTask todoTask in _list)
            {
                if (((todoTask.Importance & importance) == todoTask.Importance))
                {
                    filteredTasks.Add(todoTask);
                }
            }

            return filteredTasks;
        }

        public IList<TodoTask> GetCompletedTasks(Importance importance)
        {
            IList<TodoTask> filteredTasks = new List<TodoTask>();

            foreach (TodoTask todoTask in _list)
            {
                if (((todoTask.Importance & importance) == todoTask.Importance) &&
                    todoTask.IsCompleted)
                {
                    filteredTasks.Add(todoTask);
                }
            }

            return filteredTasks;
        }

        public void Add(TodoTask task)
        {
            _list.Add(task);
        }

        public string RepoName
        {
            get { return _fileLocator.GetFilePath(); }
        }
    }
}
