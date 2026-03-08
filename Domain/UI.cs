
using lab_1_double_s.Domain.Tasks;
using lab_2_double_s_Feature_team9.Domain;

namespace lab_2_double_s_Feature_team9.Domain
{
    public class ConsoleApp
    {
        private TaskManager _manager = new TaskManager();

        public void Run()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("To-Do Manager");
                Console.WriteLine("1. Додати задачу");
                Console.WriteLine("2. Показати всі задачі ");
                Console.WriteLine("3. Редагувати задачу ");
                Console.WriteLine("4. Видалити задачу ");
                Console.WriteLine("5. Відмітити як виконане ");
                Console.WriteLine("6. Фільтр (Виконані / Невиконані)");
                Console.WriteLine("7. Сортувати за пріоритетністю");
                Console.WriteLine("8. Звіт по прогресу ");
                Console.WriteLine("9. Вивести список (Enumerator)");
                Console.WriteLine("10. Сортувати за назвою (IComparable)");
                Console.WriteLine("11. Сортувати за датою (IComparer)");
                Console.WriteLine("0. Вихід");
                Console.Write("\n Оберіть дію: ");

                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        AddNewTask();
                        break;
                    case "2":
                        ShowAllTasks();
                        break;
                    case "3":
                        EditExistingTask();
                        break;
                    case "4":
                        DeleteExistingTask();
                        break;
                    case "5":
                        MarkTaskAsDone();
                        break;
                    case "6":
                        FilterTasks();
                        break;
                    case "7":
                        SortTasks();
                        break;
                    case "8":
                        ShowProgressReport();
                        break;
                    case "9":
                        ShowWithEnumerator();
                        break;
                    case "10":
                        Array.Sort(_manager.Tasks, 0, _manager.TaskCount);
                        Console.WriteLine("Відсортовано за назвою.");
                        ShowAllTasks();
                        break;
                    case "11":
                        Array.Sort(_manager.Tasks, 0, _manager.TaskCount, new Comparers.DateComparer());
                        Console.WriteLine("Відсортовано за датою.");
                        ShowAllTasks();
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Невірний вибір ");
                        break;
                }
                Console.ReadKey();
            }
        }

        private void AddNewTask()
        {
            Console.Write("Введіть назву задачі: ");
            string title = Console.ReadLine();
            Console.Write("Введіть пріоритет (1 - Високий, 2 - Низький): ");
            int priority = int.Parse(Console.ReadLine());
            Console.Write("Введіть кількість днів для дедлайну: ");
            int days = int.Parse(Console.ReadLine());

            _manager.AddTask(title, priority, days);
            Console.WriteLine("Задачу додано!");
        }

        private void EditExistingTask()
        {
            ShowAllTasks();
            Console.WriteLine("Введіть ID задачі для редагування: ");
            if (!int.TryParse(Console.ReadLine(), out int editId)) return;

            if (_manager.FindIndexById(editId) != -1)
            {
                Console.WriteLine("Що ви хочете змінити?");
                Console.WriteLine("1. Назву");
                Console.WriteLine("2. Пріоритет");
                Console.WriteLine("3. Кількість днів (дедлайн)");
                Console.WriteLine("0. Скасувати");
                Console.Write("Ваш вибір: ");

                string subChoice = Console.ReadLine();
                switch (subChoice)
                {
                    case "1":
                        Console.Write("Введіть нову назву: ");
                        _manager.EditTaskTitle(editId, Console.ReadLine());
                        Console.WriteLine("Назву змінено.");
                        break;
                    case "2":
                        Console.Write("Введіть новий пріоритет (1 - Високий, 2 - Низький): ");
                        _manager.EditTaskPriority(editId, int.Parse(Console.ReadLine()));
                        Console.WriteLine("Пріоритет змінено.");
                        break;
                    case "3":
                        Console.Write("Введіть нову кількість днів від сьогодні: ");
                        _manager.EditTaskDate(editId, int.Parse(Console.ReadLine()));
                        Console.WriteLine("Дату змінено.");
                        break;
                    default:
                        Console.WriteLine("Редагування скасовано.");
                        break;
                }
            }
            else
            {
                Console.WriteLine("Задачу з таким ID не знайдено.");
            }
        }

        private void DeleteExistingTask()
        {
            ShowAllTasks();
            Console.Write("Введіть ID задачі для видалення: ");
            int delId = int.Parse(Console.ReadLine());

            if (_manager.DeleteTask(delId))
            {
                Console.WriteLine("Видалено.");
            }
            else
            {
                Console.WriteLine("Задачу не знайдено.");
            }
        }

        private void MarkTaskAsDone()
        {
            Console.Write("Введіть ID виконаної задачі: ");
            if (int.TryParse(Console.ReadLine(), out int doneId))
            {
                if (_manager.MarkDone(doneId))
                    Console.WriteLine("Статус оновлено.");
                else
                    Console.WriteLine("Задачу не знайдено.");
            }
        }

        private void FilterTasks()
        {
            Console.WriteLine("1. Показати виконані");
            Console.WriteLine("2. Показати невиконані");
            string filterChoice = Console.ReadLine();
            bool showDone = filterChoice == "1";

            Console.WriteLine($"\n Результати фільтру (Done: {showDone}) ");
            for (int i = 0; i < _manager.TaskCount; i++)
            {
                if (_manager.Tasks[i].IsDone == showDone)
                {
                    Console.WriteLine(_manager.Tasks[i].GetInfo());
                }
            }
        }

        private void SortTasks()
        {
            _manager.SortTasks();
            Console.WriteLine("Відсортовано за пріоритетом.");
            ShowAllTasks();
        }

        private void ShowProgressReport()
        {
            if (_manager.TaskCount == 0)
            {
                Console.WriteLine("Задач немає.");
            }
            else
            {
                int doneCount = 0;
                for (int i = 0; i < _manager.TaskCount; i++)
                {
                    if (_manager.Tasks[i].IsDone) doneCount++;
                }
                double percentage = (double)doneCount / _manager.TaskCount * 100;
                Console.WriteLine($"\nЗвіт виконання: {percentage:F2}% (Виконано {doneCount} з {_manager.TaskCount})");
            }
        }

        private void ShowAllTasks()
        {
            Console.WriteLine("\n    Список задач    ");
            if (_manager.TaskCount == 0)
            {
                Console.WriteLine("Список порожній.");
                return;
            }
            for (int i = 0; i < _manager.TaskCount; i++)
            {
                if (_manager.Tasks[i] != null)
                {
                    Console.WriteLine(_manager.Tasks[i].GetInfo());
                }
            }
        }
        private void ShowWithEnumerator()
        {
            var collection = new TaskCollection();
            for (int i = 0; i < _manager.TaskCount; i++)
            {
                collection.Add(_manager.Tasks[i]);
            }

            Console.WriteLine("\n--- Перелік (через Enumerator) ---");
            var it = collection.GetEnumerator();
            while (it.MoveNext())
            {
                TodoTask task = (TodoTask)it.Current;
                Console.WriteLine(task.GetInfo());
            }
        }
    }
}
