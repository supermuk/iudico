using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Linq.Expressions;
using IUDICO.Common.Models.Interfaces;

namespace IUDICO.Common.Models
{
    public class MemoryTable<TEntity> : IMockableTable<TEntity>
        where TEntity : class
    {
        protected List<TEntity> _Data;
        protected List<TEntity> _TempData = new List<TEntity>();
        protected string _IdPropertyName;
        protected int _LastIndex;

        public MemoryTable() : this(new List<TEntity>()) { }

        public MemoryTable(string idPropertyName) : this(new List<TEntity>(), idPropertyName) { }

        public MemoryTable(IEnumerable<TEntity> initialData)
            : this(initialData, null)
        {
        }

        public MemoryTable(IEnumerable<TEntity> initialData, string idPropertyName)
        {
            _Data = new List<TEntity>(initialData);
            _IdPropertyName = idPropertyName;
            _LastIndex = initialData.Count();
        }

        #region Implementation of IEnumerable

        IEnumerator<TEntity> IEnumerable<TEntity>.GetEnumerator()
        {
            return _Data.GetEnumerator();
        }

        public IEnumerator GetEnumerator()
        {
            return _Data.GetEnumerator();
        }

        #endregion

        #region Implementation of IQueryable

        public Expression Expression
        {
            get { return _Data.AsQueryable().Expression; }
        }

        public Type ElementType
        {
            get { return _Data.AsQueryable().ElementType; }
        }

        public IQueryProvider Provider
        {
            get { return _Data.AsQueryable().Provider; }
        }

        #endregion

        #region Implementation of ITable

        public void InsertOnSubmit(object entity)
        {
            var data = (TEntity)entity;

            if (entity == null || data == null)
            {
                throw new ArgumentException("Not null argument of type " + typeof(TEntity).Name + " is needed");
            }

            _Data.Add(data);
            //var a=Activator.CreateInstance(// typeof(TEntity).cop
            ////auto-setting id property(Identity Specification)
            if (_IdPropertyName != null)
            {
                _LastIndex++;
                typeof(TEntity).GetProperty(_IdPropertyName).SetValue(data, _LastIndex, null);
            }
        }

        public void InsertAllOnSubmit(IEnumerable entities)
        {
            foreach (var entity in entities)
            {
                try
                {
                    var data = (TEntity)entity;

                    if (entity == null || data == null)
                    {
                        throw new ArgumentException("Not null argument of type " + typeof(TEntity).Name + " is needed");
                    }

                    _TempData.Add(data);
                }
                catch (Exception)
                {
                    _TempData.Clear();

                    throw;
                }
            }

            _Data.AddRange(_TempData);
            //auto-setting id property(Identity Specification)
            if (_IdPropertyName != null)
            {
                _TempData.ForEach(data => 
                    {
                        _LastIndex++;
                        typeof(TEntity).GetProperty(_IdPropertyName).SetValue(data, _LastIndex, null);
                    });
            }
            _TempData.Clear();
        }

        public void Attach(object entity)
        {
            throw new NotImplementedException();
        }

        public void Attach(object entity, bool asModified)
        {
            throw new NotImplementedException();
        }

        public void Attach(object entity, object original)
        {
            throw new NotImplementedException();
        }

        public void AttachAll(IEnumerable entities)
        {
            throw new NotImplementedException();
        }

        public void AttachAll(IEnumerable entities, bool asModified)
        {
            throw new NotImplementedException();
        }

        public void DeleteOnSubmit(object entity)
        {
            var data = (TEntity)entity;

            if (entity == null || data == null)
            {
                throw new ArgumentException("Not null argument of type " + typeof(TEntity).Name + " is needed");
            }

            _Data.Remove(data);
        }

        public void DeleteAllOnSubmit(IEnumerable entities)
        {
            foreach (var entity in entities)
            {
                try
                {
                    var data = (TEntity)entity;

                    if (entity == null || data == null)
                    {
                        throw new ArgumentException("Not null argument of type " + typeof(TEntity).Name + " is needed");
                    }

                    _TempData.Add(data);
                }
                catch (Exception)
                {
                    _TempData.Clear();

                    throw;
                }
            }

            foreach (var entity in _TempData)
            {
                _Data.Remove(entity);
            }
        }

        public object GetOriginalEntityState(object entity)
        {
            throw new NotImplementedException();
        }

        public ModifiedMemberInfo[] GetModifiedMembers(object entity)
        {
            throw new NotImplementedException();
        }

        public DataContext Context
        {
            get { throw new NotImplementedException(); }
        }

        public bool IsReadOnly
        {
            get { throw new NotImplementedException(); }
        }

        #endregion
    }
}
