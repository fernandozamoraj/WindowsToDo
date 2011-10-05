using System;
using System.Collections.Generic;
using System.IO;

namespace TodoList.Services.Repos.FileRepos
{
    public class FileHistoryRepo : IFileHistoryRepo
    {
        private IList<string> _fileNames = new List<string>();
 
        public IList<string> FileNames
        {
            get
            {
                if(_fileNames.Count == 0)
                {
                    LoadFileNames();
                }

                return _fileNames;
            }
        }

        public string LastFileAccessed
        {
            get 
            { 
                string lastFile = string.Empty;

                if(FileNames.Count > 0)
                {
                    lastFile = FileNames[_fileNames.Count - 1];
                }

                return lastFile;
            }
        }
 
        public void AddFileName(string fileName)
        {
            if(!FileNames.Contains(fileName))
            {
                FileNames.Add(fileName);
            }
        }

        public void Save()
        {
            try
            {
                using (StreamWriter streamWriter = new StreamWriter(GetFilePath()))
                {
                    foreach (string fileName in _fileNames)
                    {
                        streamWriter.WriteLine(fileName);
                    }
                }
            }
            catch (Exception exception)
            {
                throw new Exception("Failed to save file history " + GetFilePath());
            }
        }

        private void LoadFileNames()
        {
            try
            {
                _fileNames = new List<string>();

                string filePath = GetFilePath();

                if (File.Exists(filePath))
                {
                    using (StreamReader reader = new StreamReader(GetFilePath()))
                    {
                        while (reader.Peek() != -1)
                        {
                            string fileName = reader.ReadLine();

                            if (!_fileNames.Contains(fileName))
                            {
                                _fileNames.Add(fileName);
                            }
                        }
                    }
                }
            }
            catch
            {
                throw new Exception("Failed to read file history from " + GetFilePath());
            }
        }

        private string GetFilePath()
        {
            return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "FileHistory.dat");
        }
    }
}