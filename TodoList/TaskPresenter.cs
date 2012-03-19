using System;
using System.Collections.Generic;
using TodoList.Model;
using TodoList.Services.Repos;
using TodoList.Services.Repos.FileRepos;

namespace TodoList
{
    public class TaskPresenter
    {
        private IFileHistoryRepo _fileHistoryRepo;
        private ITaskRepo _taskRepo;
        private IInput _input;
        private IPrompt _prompt;
        private bool _filled = false;
        private bool _hasChanges;

        public TaskPresenter(ITaskRepo taskRepo, IInput input, IPrompt prompt, IFileHistoryRepo fileHistoryRepo)
        {
            _taskRepo = taskRepo;
            _input = input;
            _prompt = prompt;
            _fileHistoryRepo = fileHistoryRepo;
        }

        public ITaskRepo Repo
        {
            get { return _taskRepo; }
        }

        public IList<TodoTask> GetUnfinishedTasks(Importance importance)
        {
            if (!_filled)
            {
                _taskRepo.Fill();
                _filled = true;
            }

            return _taskRepo.GetUnfinishedTasks(importance);
        }



        public IList<TodoTask> GetHistory(Importance importance)
        {
            return _taskRepo.GetCompletedTasks(importance);
        }


        public void AddTask(TodoTask todoTask)
        {
            _taskRepo.Add(todoTask);
            _hasChanges = true;
        }

        public string GetLastFileAccessed()
        {
            return _fileHistoryRepo.LastFileAccessed;
        }

        public void SaveAll()
        {
            _taskRepo.SaveAll();
            _fileHistoryRepo.AddFileName(_taskRepo.RepoName);
            _fileHistoryRepo.Save();
            _hasChanges = false;
        }

        public void CompleteTask(TodoTask todoTask)
        {
            todoTask.Notes = _input.GetEntry("Question", "Resolution Notes");
            todoTask.DateCompleted = DateTime.Now;
            _hasChanges = true;
        }

        public void PromptForSaveChanges()
        {
            if (_hasChanges)
            {
                if (_prompt.Show("Warning", "Would you like to save updated entries?"))
                {
                    SaveAll();
                }
            }
        }

        public string GetDataSourceName()
        {
            return _taskRepo.RepoName;
        }

        public void ChangeRepo(ITaskRepo taskRepo)
        {
            _taskRepo = taskRepo;
        }

        
    }
}