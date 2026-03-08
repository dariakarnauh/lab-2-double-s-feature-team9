using System;
using System.Collections;
using lab_1_double_s.Domain.Tasks;

namespace lab_2_double_s_Feature_team9.Domain
{
    public class TaskCollection : IEnumerable
    {
        private TodoTask[] items = new TodoTask[200];
        private int count = 0;

        public int Count => count;

        public void Add(TodoTask item)
        {
            if (count < 200) items[count++] = item;
        }

        public TodoTask GetAt(int index) => items[index];
        public void SetAt(int index, TodoTask item) => items[index] = item;
        public IEnumerator GetEnumerator()
        {
            return new TaskEnumerator(items, count);
        }

        public void RemoveAt(int index)
        {
            if (index < 0 || index >= count) return;

            for (int i = index; i < count - 1; i++)
            {
                items[i] = items[i + 1];
            }

            items[--count] = null;
        }

        public class TaskEnumerator : IEnumerator
        {
            private TodoTask[] _items;
            private int _count;
            private int _position = -1;

            public TaskEnumerator(TodoTask[] items, int count)
            {
                _items = items;
                _count = count;
            }
            public bool MoveNext() => ++_position < _count;
            public object Current => _items[_position];
            public void Reset() => _position = -1;

        }
    }
}