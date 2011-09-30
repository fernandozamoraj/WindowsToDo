using System.Windows.Forms;

namespace TodoList
{
    public interface IPrompt
    {
        bool Show(string windowText, string message);
    }

    public class Prompt : IPrompt
    {
        public bool Show(string windowText, string message)
        {
            if(MessageBox.Show(message, windowText, MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                return true;
            }

            return false;
        }
    }
}