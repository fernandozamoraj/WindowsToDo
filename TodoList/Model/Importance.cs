using System;

namespace TodoList.Model
{
    [Flags]
    public enum Importance
    {
        None = 0,
        Low = 1,
        Medium = 2,
        High = 4,
        Highest = 8,
        All = 15
    }
}