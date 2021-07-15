using System;
using System.Collections.Generic;

namespace ConsoleApplication1
{
    public class MyList<T> : IClosedList<T>
    {
        const int increment = 16; // шаг прироста размера списка
        T[] arr = null; // массив
        int size = 0; // количество данных в массиве
        int ptr = 0; // указатель

        public MyList(int capacity = increment)
        {
            arr = new T[capacity];
        }

        public MyList(params T[] array)
        {
            size = array.Length;
            arr = new T[size];
            array.CopyTo(arr, 0);
        }

        public void MoveNext(int step = 1) // движение по кольцу вперед
        {
            if (step < 0)
            {
                throw new Exception("Шаг должен быть положительным числом");
            }

            int p = ptr;
            ptr = (ptr + step) % size;

            if (HeadReached != null && p + step >= size)
            {
                HeadReached(this, arr[ptr]);
            }
        }

        public void MoveBack(int step = 1) // движение по кольцу назад
        {
            if (step < 0)
            {
                throw new Exception("Шаг должен быть положительным числом");
            }

            int p = ptr;
            ptr = (size + (ptr - step) % size) % size;
            //ptr = (size * (int)(step / size + 1) + ptr - step) % size; // тоже работает

            if (HeadReached != null && p - step <= 0)
            {
                HeadReached(this, arr[ptr]);
            }
        }

        public T Head // элемент с нулевым индексом
        {
            get
            {
                if (size == 0)
                {
                    return default(T);
                }
                return arr[0];
            }
        }

        public T Current // текущий элемент
        {
            get
            {
                if (size == 0)
                {
                    return default(T);
                }
                return arr[ptr];
            }
        }

        public T Previous // предыдущий элемент
        {
            get
            {
                if (size == 0)
                {
                    return default(T);
                }

                if (ptr == 0)
                {
                    return arr[size - 1];
                }

                return arr[ptr - 1];
            }
        }

        public T Next // следующий элемент
        {
            get
            {
                if (size == 0)
                {
                    return default(T);
                }

                if (ptr == size - 1)
                {
                    return arr[0];
                }

                return arr[ptr + 1];
            }
        }

        public event EventHandler<T> HeadReached;

        public int IndexOf(T item) // определяем индекс элемента
        {
            for (int i = 0; i < size; i++)
            {
                if (arr[i].Equals(item))
                    return i;
            }
            return -1;
        }

        public void Insert(int index, T item) // вставляем элемент
        {
            if (index < 0 || index >= size)
            {
                throw new IndexOutOfRangeException("Индекс вне диапазона");
            }

            if (size == arr.Length) // увеличеваем массив
            {
                T[] tmp = new T[arr.Length + increment];
                arr.CopyTo(tmp, 0);
                arr = tmp;
            }

            for (int i = size; i > index; i--) // сдвигаем массив
            {
                arr[i] = arr[i - 1];
            }

            arr[index] = item;
            size++;

            if (index <= ptr) ptr++; // смещаем указатель при вставке элемента
        }

        public void RemoveAt(int index) // удаляем элемент по индексу
        {
            if (index < 0 || index >= size)
            {
                throw new IndexOutOfRangeException("Индекс вне диапазона");
            }

            for (int i = index; i < size - 1; i++) // смещаем массив
            {
                arr[i] = arr[i + 1];
            }

            size--;

            if (ptr >= size) // в случае если указатель выходит за верхнюю границу
            {
                ptr = size - 1;
            }

            if (index <= ptr) // смещаем указатель при удалении
            {
                if (ptr > 0) ptr--;
                else ptr = size - 1;    
            }
        }

        public T this[int index] // доступ к элементам списка по индексу
        {
            get
            {
                if (index < 0 || index >= size)
                {
                    throw new IndexOutOfRangeException("Индекс вне диапазона");
                }
                return arr[index];
            }
            set
            {
                if (index < 0 || index >= size)
                {
                    throw new IndexOutOfRangeException("Индекс вне диапазона");
                }
                arr[index] = value;
            }
        }

        public void Add(T item) // добавляем элемент
        {
            if (size == arr.Length)
            {
                T[] tmp = new T[arr.Length + increment];
                arr.CopyTo(tmp, 0);
                arr = tmp;
            }
            arr[size] = item;
            size++;
        }

        public void Clear() // очищаем список
        {
            arr = new T[increment];
            size = 0;
            ptr = 0;
        }

        public bool Contains(T item) // определяем содержит ли элемент
        {
            if (this.IndexOf(item) < 0)
            {
                return false;
            }
            return true;
        }

        public void CopyTo(T[] array, int arrayIndex) // копируем список, начиная с arrayIndex
        {
            if (arrayIndex < 0 || arrayIndex >= size)
            {
                throw new IndexOutOfRangeException("Индекс вне диапазона");
            }

            array = new T[size - arrayIndex];
            arr.CopyTo(array, arrayIndex);
        }

        public int Count // количество элементов
        {
            get
            {
                return size;
            }
        }

        public bool IsReadOnly // false
        {
            get
            {
                return false;
            }
        }

        public bool Remove(T item) // удаляем элемент
        {
            int index = this.IndexOf(item);
            if (index < 0) return false;
            this.RemoveAt(index);
            return true;
        }

        public IEnumerator<T> GetEnumerator() // возвращаем энумератор
        {
            return (IEnumerator<T>)arr.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return (System.Collections.IEnumerator)arr.GetEnumerator();
        }
    }
}
