using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace MultiSpel.NetModule.Model
{
    public  class SyncArrayList : ArrayList
    {
        private ArrayList _list;
        private object _root;

        public override int Capacity
        {
            get
            {
                int capacity;
                lock (this._root)
                {
                    capacity = this._list.Capacity;
                }
                return capacity;
            }
            set
            {
                lock (this._root)
                {
                    this._list.Capacity = value;
                }
            }
        }

        public override int Count
        {
            get
            {
                int count;
                lock (this._root)
                {
                    count = this._list.Count;
                }
                return count;
            }
        }

        public override bool IsReadOnly
        {
            get
            {
                return this._list.IsReadOnly;
            }
        }

        public override bool IsFixedSize
        {
            get
            {
                return this._list.IsFixedSize;
            }
        }

        public override bool IsSynchronized
        {
            get
            {
                return true;
            }
        }

        public override object this[int index]
        {
            get
            {
                object result;
                lock (this._root)
                {
                    result = this._list[index];
                }
                return result;
            }
            set
            {
                lock (this._root)
                {
                    this._list[index] = value;
                }
            }
        }

        public override object SyncRoot
        {
            get
            {
                return this._root;
            }
        }

        //internal SyncArrayList(ArrayList list)
        //    : base(false)
        //{
        //    this._list = list;
        //    this._root = list.SyncRoot;
        //}

        public override int Add(object value)
        {
            int result;
            lock (this._root)
            {
                result = this._list.Add(value);
            }
            return result;
        }

        public override void AddRange(ICollection c)
        {
            lock (this._root)
            {
                this._list.AddRange(c);
            }
        }

        public override int BinarySearch(object value)
        {
            int result;
            lock (this._root)
            {
                result = this._list.BinarySearch(value);
            }
            return result;
        }

        public override int BinarySearch(object value, IComparer comparer)
        {
            int result;
            lock (this._root)
            {
                result = this._list.BinarySearch(value, comparer);
            }
            return result;
        }

        public override int BinarySearch(int index, int count, object value, IComparer comparer)
        {
            int result;
            lock (this._root)
            {
                result = this._list.BinarySearch(index, count, value, comparer);
            }
            return result;
        }

        public override void Clear()
        {
            lock (this._root)
            {
                this._list.Clear();
            }
        }

        //public override object Clone()
        //{
        //    object result;
        //    lock (this._root)
        //    {
        //        result = new ArrayList.SyncArrayList((ArrayList)this._list.Clone());
        //    }
        //    return result;
        //}

        public override bool Contains(object item)
        {
            bool result;
            lock (this._root)
            {
                result = this._list.Contains(item);
            }
            return result;
        }

        public override void CopyTo(Array array)
        {
            lock (this._root)
            {
                this._list.CopyTo(array);
            }
        }
        public override void CopyTo(Array array, int index)
        {
            lock (this._root)
            {
                this._list.CopyTo(array, index);
            }
        }
        public override void CopyTo(int index, Array array, int arrayIndex, int count)
        {
            lock (this._root)
            {
                this._list.CopyTo(index, array, arrayIndex, count);
            }
        }
        public override IEnumerator GetEnumerator()
        {
            IEnumerator enumerator;
            lock (this._root)
            {
                enumerator = this._list.GetEnumerator();
            }
            return enumerator;
        }
        public override IEnumerator GetEnumerator(int index, int count)
        {
            IEnumerator enumerator;
            lock (this._root)
            {
                enumerator = this._list.GetEnumerator(index, count);
            }
            return enumerator;
        }
        public override int IndexOf(object value)
        {
            int result;
            lock (this._root)
            {
                result = this._list.IndexOf(value);
            }
            return result;
        }
        public override int IndexOf(object value, int startIndex)
        {
            int result;
            lock (this._root)
            {
                result = this._list.IndexOf(value, startIndex);
            }
            return result;
        }
        public override int IndexOf(object value, int startIndex, int count)
        {
            int result;
            lock (this._root)
            {
                result = this._list.IndexOf(value, startIndex, count);
            }
            return result;
        }
        public override void Insert(int index, object value)
        {
            lock (this._root)
            {
                this._list.Insert(index, value);
            }
        }
        public override void InsertRange(int index, ICollection c)
        {
            lock (this._root)
            {
                this._list.InsertRange(index, c);
            }
        }
        public override int LastIndexOf(object value)
        {
            int result;
            lock (this._root)
            {
                result = this._list.LastIndexOf(value);
            }
            return result;
        }
        public override int LastIndexOf(object value, int startIndex)
        {
            int result;
            lock (this._root)
            {
                result = this._list.LastIndexOf(value, startIndex);
            }
            return result;
        }
        public override int LastIndexOf(object value, int startIndex, int count)
        {
            int result;
            lock (this._root)
            {
                result = this._list.LastIndexOf(value, startIndex, count);
            }
            return result;
        }
        public override void Remove(object value)
        {
            lock (this._root)
            {
                this._list.Remove(value);
            }
        }
        public override void RemoveAt(int index)
        {
            lock (this._root)
            {
                this._list.RemoveAt(index);
            }
        }
        public override void RemoveRange(int index, int count)
        {
            lock (this._root)
            {
                this._list.RemoveRange(index, count);
            }
        }
        public override void Reverse(int index, int count)
        {
            lock (this._root)
            {
                this._list.Reverse(index, count);
            }
        }
        public override void SetRange(int index, ICollection c)
        {
            lock (this._root)
            {
                this._list.SetRange(index, c);
            }
        }
        public override ArrayList GetRange(int index, int count)
        {
            ArrayList range;
            lock (this._root)
            {
                range = this._list.GetRange(index, count);
            }
            return range;
        }
        public override void Sort()
        {
            lock (this._root)
            {
                this._list.Sort();
            }
        }
        public override void Sort(IComparer comparer)
        {
            lock (this._root)
            {
                this._list.Sort(comparer);
            }
        }
        public override void Sort(int index, int count, IComparer comparer)
        {
            lock (this._root)
            {
                this._list.Sort(index, count, comparer);
            }
        }
        public override object[] ToArray()
        {
            object[] result;
            lock (this._root)
            {
                result = this._list.ToArray();
            }
            return result;
        }
        public override Array ToArray(Type type)
        {
            Array result;
            lock (this._root)
            {
                result = this._list.ToArray(type);
            }
            return result;
        }
        public override void TrimToSize()
        {
            lock (this._root)
            {
                this._list.TrimToSize();
            }
        }
    }

}
