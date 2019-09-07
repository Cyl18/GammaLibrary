using System;
using System.Collections.Generic;
using System.Text;

namespace GammaLibrary.Enhancements
{
    public class EnumerableQueue<T>
    {
        protected IEnumerator<T> _enumerator;
        // T _currentObject;

        public EnumerableQueue(IEnumerable<T> e)
        {
            _enumerator = e.GetEnumerator();
        }

        protected void MoveNext()
        {
            if (!_enumerator.MoveNext())
            {
                IsEmpty = true;
            }
        }

        public virtual T Dequeue()
        {
            if (IsEmpty) throw new InvalidOperationException("the queue is empty.");
            MoveNext();
            return _enumerator.Current;
        }

        public bool IsEmpty
        {
            get;
            private set;
        }
    }

    public class ConcurrentEnumerableQueue<T> : EnumerableQueue<T>
    {
        public ConcurrentEnumerableQueue(IEnumerable<T> e) : base(e)
        {
        }

        readonly object _locker = new object();

        public override T Dequeue()
        {
            lock (_locker)
            {
                if (IsEmpty) throw new InvalidOperationException("the queue is empty.");
                MoveNext();
                return _enumerator.Current;
            }
        }

    }
}
