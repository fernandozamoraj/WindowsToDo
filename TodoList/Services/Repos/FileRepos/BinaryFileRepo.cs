using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using TodoList.Model;

namespace TodoList.Services.Repos.FileRepos
{
    public class BinaryFileRepo : FileRepo
    {
        public BinaryFileRepo(IFileLocator fileLocator)
            : base(fileLocator)
        {
        }

        public BinaryFileRepo(IFileLocator fileLocator, RepoBase repoToClone)
            : base(fileLocator, repoToClone.GetRawList())
        {
        }

        public override void Fill()
        {
            base.ClearList();

            try
            {
                if (File.Exists(_fileLocator.GetFilePath()))
                {
                      List<TodoTask> todoTasks;
                      Stream stream = File.Open(_fileLocator.GetFilePath(), FileMode.Open);
                      BinaryFormatter bFormatter = new BinaryFormatter();
                      todoTasks = (List<TodoTask>)bFormatter.Deserialize(stream);
                      stream.Close();

                    foreach (TodoTask todoTask in todoTasks)
                    {
                        Add(todoTask);   
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
                    Stream stream = File.Open(_fileLocator.GetFilePath(), FileMode.Create);
                    BinaryFormatter bFormatter = new BinaryFormatter();
                    bFormatter.Serialize(stream, CloneToList(GetRawList()));
                    stream.Close();
                }
            }
            catch (Exception exception)
            {
                throw new ApplicationException("Failed to save to: " + _fileLocator.GetFilePath(), exception);
            }

        }

        private List<TodoTask> CloneToList(IList<TodoTask> rawList)
        {
            List<TodoTask> newList = new List<TodoTask>();
 
            foreach(TodoTask task in rawList)
            {
                newList.Add(task);
            }

            return newList;
        }
    }
}