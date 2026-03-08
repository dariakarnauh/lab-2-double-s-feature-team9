
using lab_1_double_s.Domain.Interface;
using lab_2_double_s_Feature_team9.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace lab_2_double_s_Feature_team9.Domain
{
    public abstract class BaseItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public BaseItem(int id, string title)
        {
            Id = id;
            Title = title;
        }
        public abstract string GetInfo();
    }
}

namespace lab_1_double_s.Domain.Interface
{
    public interface ISortable
    {
        double GetPriority();
    }
}


namespace lab_1_double_s.Domain.Tasks
{
    public class TodoTask : BaseItem, ISortable, IComparable
    {
        public bool IsDone { get; set; }
        public int Priority { get; set; }
        public DateTime Date { get; set; }

        public TodoTask(int id, string title, int priority, DateTime date) : base(id, title)
        {
            Priority = priority;
            Date = date;
            IsDone = false;
        }

        public double GetPriority()
        {
            return Priority;
        }

        public override string GetInfo()
        {
            string status;
            if (IsDone == true)
            {
                status = "[done]";
            }
            else
            {
                status = "[undone ]";
            }
            return $"{Id}. {status} {Title} | Пріоритет: {Priority} | Дедлайн: {Date.ToShortDateString()}";
        }
        public int CompareTo(object obj)
        {
            if (obj == null) return 1;
            if (obj is TodoTask otherTask)
            {
                string title1 = this.Title ?? "";
                string title2 = otherTask.Title ?? "";
                return string.Compare(title1, title2, StringComparison.OrdinalIgnoreCase);
            }
            throw new ArgumentException("Об'єкт не є TodoTask");
        }
    }
}
