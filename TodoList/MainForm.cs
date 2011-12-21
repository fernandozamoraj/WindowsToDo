using System;
using System.Text;
using System.Windows.Forms;
using TodoList.Model;
using TodoList.Services;
using TodoList.Services.Repos.FileRepos;
using System.Collections.Generic;

namespace TodoList
{
    public partial class MainForm : Form
    {
        TaskPresenter _presenter = new TaskPresenter(new FlatFileRepo(new FileLocator()), new InputBox(), new Prompt(), new FileHistoryRepo());

        public MainForm()
        {
            InitializeComponent();
        }

        public MainForm(string[] args)
        {
            InitializeComponent();

            if(args.Length > 1)
                _presenter = new TaskPresenter(new FlatFileRepo(new FileLocator(args[1])), new InputBox(), new Prompt(), new FileHistoryRepo());
        }

        private void btnAddNewTask_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtNewTask.Text))
            {
                TodoTask task = new TodoTask
                                    {
                                        DateCompleted = DateTime.Now,
                                        DateEntered = DateTime.Now,
                                        Description = txtNewTask.Text,
                                        Importance = Importance.Low,
                                        Notes = string.Empty
                                    };

                _presenter.AddTask(task);

                txtNewTask.Text = string.Empty;

                listBoxTasks.Items.Add(task);

                
                
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(_presenter.GetLastFileAccessed()))
                {
                    LoadFile(_presenter.GetLastFileAccessed());
                }
                else
                {
                    ReLoad();
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show("Failed to load. Addition info: " + exception.Message);
            }
        }

        private void ReLoad()
        {
            try
            {
                FillTodoTaskListBox(GetAllFilter());
                FillHistoryListBox(Importance.All);
                txtDataSource.Text = _presenter.GetDataSourceName();

            }
            catch (Exception exception)
            {
                MessageBox.Show("Failed to load data properlty additional information " + exception.Message);
            }
        }

        private Importance GetAllFilter()
        {
            return Importance.All;
        }

        private void FillTodoTaskListBox(Importance importance)
        {
            listBoxTasks.Items.Clear();

            foreach (TodoTask todoTask in _presenter.GetUnfinishedTasks(importance))
            {
                listBoxTasks.Items.Add(todoTask);
            }
        }

        private void FillHistoryListBox(Importance importance)
        {
            List<TodoTask> tasks = new List<TodoTask>();

            foreach (TodoTask todoTask in _presenter.GetHistory(importance))
            {
                tasks.Add(todoTask);
            }
                                   
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = tasks;
            dataGridView1.Columns["IsCompleted"].Visible = false;
            dataGridView1.Columns["Importance"].Visible = false;
            dataGridView1.Columns["Description"].Width = 360;
            dataGridView1.Columns["Notes"].Width = 350;
            dataGridView1.Columns["DateEntered"].Width = 140;
            dataGridView1.Columns["DateCompleted"].Width = 140;
        }

        private void btnComplete_Click(object sender, EventArgs e)
        {
            try
            {
                if (listBoxTasks.SelectedIndex > -1)
                {
                    _presenter.CompleteTask(listBoxTasks.SelectedItem as TodoTask);
                    FillTodoTaskListBox(GetFilterValues());
                    FillHistoryListBox(Importance.All);
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show("Failed to complete the task. Addition info: " + exception.Message);
            }

        }

        private Importance GetFilterValues()
        {
            Importance importance = Importance.None;

            //if (checkBoxLow.Checked)
            //    importance |= Importance.Low;
            //if (checkBoxMedium.Checked)
            //    importance |= Importance.Medium;
            //if(checkBoxHigh.Checked)
            //    importance |= Importance.High;
            //if (checkBoxHighest.Checked)
            //    importance |= Importance.Highest;
            //if (checkBoxAll.Checked)
            //    importance = Importance.All;

            if (importance == Importance.None)
                importance = Importance.All;

            return importance;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty( _presenter.GetDataSourceName()) )
            {
                SaveAs();
            }
            else
            {
                _presenter.SaveAll();
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(this.DialogResult != System.Windows.Forms.DialogResult.OK)
            {
                _presenter.PromptForSaveChanges();
            }
        }

        private void btnLoadFromFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Todo Files (*.tod) | *.tod";
            openFileDialog.DefaultExt = "tod";

            if(openFileDialog.ShowDialog() == DialogResult.OK)
            {
                LoadFile(openFileDialog.FileName);
            }
        }

        private void LoadFile(string file)
        {
            _presenter = new TaskPresenter(new FlatFileRepo(new FileLocator(file)), new InputBox(), new Prompt(), new FileHistoryRepo());
            ReLoad();
        }

        private void btnSaveAs_Click(object sender, EventArgs e)
        {
            SaveAs();
        }

        private void SaveAs()
        {
            try
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();

                saveFileDialog.Filter = "Todo Files (*.tod) | *.tod";
                saveFileDialog.DefaultExt = "tod";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    _presenter.ChangeRepo(new FlatFileRepo(new FileLocator(saveFileDialog.FileName), _presenter.Repo as FlatFileRepo));
                    _presenter.SaveAll();
                    txtDataSource.Text = _presenter.GetDataSourceName();
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show("Failed to save file. Additional Info: " + exception.Message);
            }

        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            StringBuilder data = new StringBuilder();

            data.Append(Environment.NewLine + "*****To-Do Task List Items******");

            foreach(TodoTask todoTask in listBoxTasks.Items)
            {
                data.Append(Environment.NewLine + todoTask);
            }

            Clipboard.SetDataObject(data.ToString(), true);

            MessageBox.Show("Your data is ready to be pasted.");
        }
    }
}
