using System;
using System.Collections;
using System.ComponentModel;
using IUDICO.DataModel.DB.Base;
using System.Collections.Generic;

namespace LEX.NewsProject.Model.DB
{
    public class CachedBindingList<TDataObject> : IBindingList
        where TDataObject : class, IIntKeyedDataObject, new()
    {
        public CachedBindingList(IDBOperator db, IList<int> ids)
        {
            Db = db;
            _Ids = ids;
        }

        #region Implementation of IEnumerable

        public IEnumerator GetEnumerator()
        {
            return new CachedBindingListEnumerator<TDataObject>(this);
        }

        #endregion

        #region Implementation of ICollection

        public void CopyTo(Array array, int index)
        {
            throw new InvalidOperationException();
        }

        public int Count
        {
            get
            {
                //return Db.Count<TDataObject>();
                return _Ids.Count;
            }
        }

        public object SyncRoot
        {
            get { return this; }
        }

        public bool IsSynchronized
        {
            get { return true; }
        }

        #endregion

        #region Implementation of IList

        public int Add(object value)
        {
            throw new InvalidOperationException();
        }

        public bool Contains(object value)
        {
            throw new InvalidOperationException();
        }

        public void Clear()
        {
            throw new InvalidOperationException();
        }

        public int IndexOf(object value)
        {
            throw new InvalidOperationException();
        }

        public void Insert(int index, object value)
        {
            throw new InvalidOperationException();
        }

        public void Remove(object value)
        {
            throw new InvalidOperationException();
        }

        public void RemoveAt(int index)
        {
            throw new InvalidOperationException();
        }

        public object this[int index]
        {
            get
            {
                // TODO: Optimize it
                //return Db.LoadRange<TDataObject>(index + 1, index + 1)[0];
                return Db.Load<TDataObject>(_Ids[index]);
            }
            set { throw new InvalidOperationException(); }
        }

        public bool IsReadOnly
        {
            get { return true; }
        }

        public bool IsFixedSize
        {
            get { return true; }
        }

        #endregion

        #region Implementation of IBindingList

        public event ListChangedEventHandler ListChanged;
        public object AddNew()
        {
            throw new InvalidOperationException();
        }

        public void AddIndex(PropertyDescriptor property)
        {
            throw new InvalidOperationException();
        }

        public void ApplySort(PropertyDescriptor property, ListSortDirection direction)
        {
            throw new InvalidOperationException();
        }

        public int Find(PropertyDescriptor property, object key)
        {
            throw new InvalidOperationException();
        }

        public void RemoveIndex(PropertyDescriptor property)
        {
            throw new InvalidOperationException();
        }

        public void RemoveSort()
        {
            throw new InvalidOperationException();
        }

        public bool AllowNew
        {
            get { return false; }
        }

        public bool AllowEdit
        {
            get { return false; }
        }

        public bool AllowRemove
        {
            get { return false;}
        }

        public bool SupportsChangeNotification
        {
            get { return false; }
        }

        public bool SupportsSearching
        {
            get { return false; }
        }

        public bool SupportsSorting
        {
            get { return false; }
        }

        public bool IsSorted
        {
            get { return false; }
        }

        public PropertyDescriptor SortProperty
        {
            get { throw new InvalidOperationException(); }
        }

        public ListSortDirection SortDirection
        {
            get { throw new InvalidOperationException(); }
        }

        #endregion

        protected readonly IDBOperator Db;
        protected readonly IList<int> _Ids;
    }

    internal class CachedBindingListEnumerator<TDataObject> : IEnumerator
        where TDataObject : class, IIntKeyedDataObject, new()
    {
        public CachedBindingListEnumerator(CachedBindingList<TDataObject> owner)
        {
            _Owner = owner;   
            Reset();
        }

        #region Implementation of IEnumerator

        public bool MoveNext()
        {
            return ++_CurrentIndex < _Owner.Count;
        }

        public void Reset()
        {
            _CurrentIndex = -1;
        }

        public object Current
        {
            get { return _Owner[_CurrentIndex]; }
        }

        #endregion

        private int _CurrentIndex;
        private readonly CachedBindingList<TDataObject> _Owner;
    }
}