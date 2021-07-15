using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    public class MyList2<T> : Collection<T>, IClosedList<T>
    {
        int ptr = 0; // указатель

        public void MoveNext(int step = 1)
        {
            if (step < 0)
            {
                throw new Exception("Шаг должен быть положительным числом");
            }

            int p = ptr;
            ptr = (ptr + step) % this.Count;

            if (HeadReached != null && p + step >= this.Count)
            {
                HeadReached(this, this[ptr]);
            }
        }

        public void MoveBack(int step = 1)
        {
            if (step < 0)
            {
                throw new Exception("Шаг должен быть положительным числом");
            }

            int p = ptr;
            ptr = (this.Count + ptr - step % this.Count) % this.Count;

            if (HeadReached != null && p - step <= 0)
            {
                HeadReached(this, this[ptr]);
            }
        }

        public T Head
        {
            get
            {
                if (this.Count == 0)
                {
                    return default(T);
                }
                return this[0];
            }
        }

        public T Current
        {
            get
            {
                if (this.Count == 0)
                {
                    return default(T);
                }
                return this[ptr];
            }
        }

        public T Previous
        {
            get
            {
                if (this.Count == 0)
                {
                    return default(T);
                }

                if (ptr == 0)
                {
                    return this[this.Count - 1];
                }

                return this[ptr - 1];
            }
        }

        public T Next
        {
            get
            {
                if (this.Count == 0)
                {
                    return default(T);
                }

                if (ptr == this.Count - 1)
                {
                    return this[0];
                }

                return this[ptr + 1];
            }
        }

        public event EventHandler<T> HeadReached;

        public new void Insert(int index, T item)
        {
            base.Insert(index, item);
            if (index <= ptr) ptr++; // смещаем указатель при вставке элемента
        }

        public new void RemoveAt(int index)
        {
            base.RemoveAt(index);
            if (index <= ptr) // смещаем указатель при удалении
            {
                if (ptr > 0) ptr--;
                else ptr = this.Count - 1;
            }
        }

        public new void Remove(T item)
        {
            int index = base.IndexOf(item);
            base.Remove(item);

            if (index <= ptr) // смещаем указатель при удалении
            {
                if (ptr > 0) ptr--;
                else ptr = this.Count - 1;
            }
        }

        public new void Clear()
        {
            base.Clear();
            ptr = 0;
        }
    }
}
