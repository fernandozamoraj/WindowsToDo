using System;

namespace TodoList.Model
{
    [Serializable]
    public class TodoTask : IComparable
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

            return string.Format("{0}  {1}  {2}  ", DateEntered.ToShortDateString(), this.Importance, Description).PadRight(50) +
                string.Format("  {0} {1}", dateCompleted, Notes).PadLeft(30);
        }

        public int CompareTo(object obj)
        {
            if(obj is TodoTask)
            {
                TodoTask other = obj as TodoTask;

                if(this.Importance != other.Importance)
                {
                    return other.Importance.CompareTo(this.Importance);
                }
                else if(this.DateEntered.Date != other.DateEntered.Date)
                {
                    return this.DateEntered.CompareTo(other.DateEntered);
                }
                else
                {
                    return this.Description.CompareTo(other.Description);
                }
            }

            return 0;
        }
    }
}
