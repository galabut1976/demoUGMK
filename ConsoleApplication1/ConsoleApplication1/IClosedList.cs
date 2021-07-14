using System;
using System.Collections.Generic;

namespace ConsoleApplication1
{
    public interface IClosedList<T> : IList<T>
    {
        void MoveNext(int step = 1);
        void MoveBack(int step = 1);
        T Head { get; }
        T Current { get; }
        T Previous { get; }
        T Next { get; }
        event EventHandler<T> HeadReached;
    }
}
