using System;
using System.IO;
using TodoList.Model;

namespace TodoList.Services.Repos.FileRepos
{
    public class FlatFileRepo : FileRepo
    {
        public FlatFileRepo(IFileLocator fileLocator) : base(fileLocator)
        {
        }

        public FlatFileRepo(IFileLocator fileLocator, RepoBase repoToClone) : base(fileLocator, repoToClone.GetRawList())
        {
        }

        public override void Fill()
        {
            base.ClearList();

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

                            Add(task);

                        }
                    }
                }
            }
            catch (Exception exception)
            {
                throw new Exception("Failed while reading: " + _fileLocator.GetFilePath(), exception);
            }
        }

        public override void SaveAll()
        {
            try
            {
                if (!string.IsNullOrEmpty(_fileLocator.GetFilePath()))
                {
                    using (StreamWriter writer = new StreamWriter(_fileLocator.GetFilePath()))
                    {
                        foreach (TodoTask todoTask in base.GetRawList())
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
    }
}
