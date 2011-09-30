using System;
using System.Text;
using System.Windows.Forms;
using TodoList.Model;
using TodoList.Services;

namespace TodoList
{
    public partial class MainForm : Form
    {
        TaskPresenter _presenter = new TaskPresenter(new FlatFileRepo(new FileLocator()), new InputBox(), new Prompt());

        public MainForm()
        {
            InitializeComponent();
        }

        private void btnAddNewTask_Click(object sender, EventArgs e)
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

            listBoxTasks.Items.Add(task);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ReLoad();
        }

        private void ReLoad()
        {
            FillTodoTaskListBox(GetAllFilter());
            FillHistoryListBox(Importance.All);
            txtDataSource.Text = _presenter.GetDataSourceName();
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
            listBoxHistory.Items.Clear();

            foreach (TodoTask todoTask in _presenter.GetHistory(importance))
            {
                listBoxHistory.Items.Add(todoTask);
            }
        }

        private void btnComplete_Click(object sender, EventArgs e)
        {
            if(listBoxTasks.SelectedIndex > -1)
            {
                _presenter.CompleteTask(listBoxTasks.SelectedItem as TodoTask);
                FillTodoTaskListBox(GetFilterValues());
                FillHistoryListBox(Importance.All);
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
            _presenter.SaveAll();
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            FillTodoTaskListBox(GetFilterValues());
        }

        private void checkBox1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(this.DialogResult != System.Windows.Forms.DialogResult.OK)
            {
                _presenter.PromptForSaveChanges();
            }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnLoadFromFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            if(openFileDialog.ShowDialog() == DialogResult.OK)
            {
                _presenter = new TaskPresenter(new FlatFileRepo(new FileLocator(openFileDialog.FileName)), new InputBox(), new Prompt());
                ReLoad();
                
            }
        }

        private void btnSaveAs_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                _presenter.ChangeRepo(new FlatFileRepo(new FileLocator(saveFileDialog.FileName), _presenter.Repo as FlatFileRepo));
                txtDataSource.Text = _presenter.GetDataSourceName();
            }
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            StringBuilder data = new StringBuilder();

            foreach(TodoTask todoTask in listBoxTasks.Items)
            {
                data.Append(Environment.NewLine + todoTask);
            }

            Clipboard.SetDataObject(data.ToString(), true);
        }
    }
}
