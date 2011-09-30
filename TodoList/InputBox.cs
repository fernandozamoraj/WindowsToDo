using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TodoList
{
    public partial class InputBox : Form, IInput
    {
        public InputBox()
        {
            InitializeComponent();
        }

        private void InputBox_Load(object sender, EventArgs e)
        {

        }

        public string GetEntry(string windowText, string label)
        {
            this.Text = windowText;
            this.label1.Text = label;

            if(DialogResult.OK == ShowDialog())
            {
                return this.textBox1.Text;
            }

            return string.Empty;
        }
    }
}
