using System;

namespace TodoList.Model
{
    public class TodoTask
    {
        public string Description { get; set; }
        public DateTime DateEntered { get; set; }
        public string Notes { get; set; }
        public Importance Importance { get; set; }
        public DateTime DateCompleted { get; set; }

        public bool IsCompleted
        {
            get { return DateCompleted > DateEntered; }
        }

        public override string ToString()
        {
            string dateCompleted = IsCompleted ? DateCompleted.ToShortDateString() : string.Empty;

            return string.Format("{0}   {1}  ", DateEntered.ToShortDateString(), Description).PadRight(50) +
                string.Format("  {0} {1}", dateCompleted, Notes).PadLeft(30);
        }
    }
}
