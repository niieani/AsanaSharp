using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsanaSharp
{
    [Serializable]
    public class AsanaReadOnlyCollection<T> : IEnumerable<T>, INotifyCollectionChanged
    {
        internal readonly ObservableCollection<T> Collection;
        public bool Fetched { get { return _fetched; } }
        private bool _fetched;

        public int Count
        {
            get { return Collection.Count; }
        }

        public AsanaReadOnlyCollection(IEnumerable<T> source)
        {
            Collection = new ObservableCollection<T>(source);
            WireUp();

            _fetched = true;
        }

        public AsanaReadOnlyCollection()
        {
            Collection = new ObservableCollection<T>();
            WireUp();
        }

        private void WireUp()
        {
            Collection.CollectionChanged += (sender, args) =>
            {
                if (!ReferenceEquals(CollectionChanged, null))
                    CollectionChanged.Invoke(this, args);
            };
        }

        internal bool Import(IEnumerable<T> source, bool skipRemoving = false)
        {
            var objectsAdded = source.Except(Collection).ToArray();

            foreach (var added in objectsAdded)
                Collection.Add(added);

            var objectsRemoved = new T[0];

            if (!skipRemoving)
            {
                objectsRemoved = Collection.Except(source).ToArray();
                foreach (var removed in objectsRemoved)
                    Collection.Remove(removed);
            }

            _fetched = true;

            // was there a change?
            return objectsRemoved.Length > 0 || objectsAdded.Length > 0;
        }

        internal void EventedAdd(T item)
        {
            if (!Collection.Contains(item))
                Collection.Add(item);
        }
        internal void EventedRemove(T item)
        {
            Collection.Remove(item);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return Collection.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public event NotifyCollectionChangedEventHandler CollectionChanged;
    }

    public class AsanaCollection<T> : AsanaReadOnlyCollection<T>
    {
        public AsanaCollection(IEnumerable<T> source) : base(source)
        {
        }

        public AsanaCollection() : base()
        {
        }

        private readonly List<T> _bufferAdd = new List<T>();
        private readonly List<T> _bufferRemove = new List<T>();

        public int BufferCount
        {
            get { return _bufferAdd.Count + _bufferRemove.Count; }
        }

        public void Add(T item)
        {
            _bufferAdd.Add(item);
            _bufferRemove.Remove(item);
        }
        public void AddRange(IEnumerable<T> collection)
        {
            var enumerable = collection as T[] ?? collection.ToArray();
            _bufferAdd.AddRange(enumerable);
            _bufferRemove.RemoveAll(enumerable.Contains);
        }
        public void Remove(T item)
        {
            if (this.Contains(item))
                _bufferRemove.Add(item);
            _bufferAdd.Remove(item);
        }
        public void RemoveRange(IEnumerable<T> collection)
        {
            var enumerable = collection as T[] ?? collection.ToArray();

            foreach (var item in enumerable)
            {
                Remove(item);
            }
        }
        public void Clear()
        {
            _bufferRemove.AddRange(this);
        }

        internal Buffer GetBufferAndPurge()
        {
            var add = _bufferAdd.Except(this).Distinct().ToArray();
            _bufferAdd.Clear();
            var remove = _bufferRemove.Distinct().ToArray();
            _bufferRemove.Clear();
            return new Buffer(add, remove);
        }

        internal class Buffer
        {
            public readonly T[] AddBuffer;
            public readonly T[] RemoveBuffer;

            public Buffer(T[] add, T[] remove)
            {
                AddBuffer = add;
                RemoveBuffer = remove;
            }
        }
    }
}
