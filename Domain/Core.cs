using lab_1_double_s.Domain.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab_2_double_s_Feature_team9.Domain
{
    public class TaskManager
    {
        public TodoTask[] Tasks = new TodoTask[200];
        public int TaskCount = 0;

        public bool AddTask(string title, int priority, int days)
        {
            if (TaskCount >= 200) return false;

            DateTime date = DateTime.Now.AddDays(days);
            Tasks[TaskCount] = new TodoTask(TaskCount + 1, title, priority, date);
            TaskCount++;
            return true;
        }

        public int FindIndexById(int id)
        {
            for (int i = 0; i < TaskCount; i++)
            {
                if (Tasks[i].Id == id) return i;
            }
            return -1;
        }

        public bool DeleteTask(int id)
        {
            int idx = FindIndexById(id);
            if (idx != -1)
            {
                for (int i = idx; i < TaskCount - 1; i++)
                {
                    Tasks[i] = Tasks[i + 1];
                }
                Tasks[TaskCount - 1] = null;
                TaskCount--;
                return true;
            }
            return false;
        }

        public bool EditTaskTitle(int id, string newTitle)
        {
            int idx = FindIndexById(id);
            if (idx == -1) return false;
            Tasks[idx].Title = newTitle;
            return true;
        }

        public bool EditTaskPriority(int id, int newPriority)
        {
            int idx = FindIndexById(id);
            if (idx == -1) return false;
            Tasks[idx].Priority = newPriority;
            return true;
        }

        public bool EditTaskDate(int id, int days)
        {
            int idx = FindIndexById(id);
            if (idx == -1) return false;
            Tasks[idx].Date = DateTime.Now.AddDays(days);
            return true;
        }

        public bool MarkDone(int id)
        {
            int idx = FindIndexById(id);
            if (idx != -1)
            {
                Tasks[idx].IsDone = true;
                return true;
            }
            return false;
        }

        public void SortTasks()
        {
            for (int i = 0; i < TaskCount - 1; i++)
            {
                for (int j = 0; j < TaskCount - 1; j++)
                {
                    if (Tasks[j].GetPriority() > Tasks[j + 1].GetPriority())
                    {
                        var temp = Tasks[j];
                        Tasks[j] = Tasks[j + 1];
                        Tasks[j + 1] = temp;
                    }
                }
            }
        }
    }
}
