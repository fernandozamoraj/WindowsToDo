using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using TodoList.Services;
using TodoList.Services.Repos.Filters;

namespace TodoList
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            RegisterDependencies();
            string[] args = Environment.GetCommandLineArgs();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Application.Run(new MainForm(args));
        }

        private static void RegisterDependencies()
        {
            ServiceLocator.Register<ITaskFilter, TaskFilter>(new TaskFilter());
        }
    }
}
