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
        protected List<TEntity> data;
        protected List<TEntity> tempData = new List<TEntity>();
        protected string propertyId;
        protected int lastIndex;

        public MemoryTable() : this(new List<TEntity>()) { }

        public MemoryTable(string propertyId) : this(new List<TEntity>(), propertyId) { }

        public MemoryTable(IEnumerable<TEntity> initialData)
            : this(initialData, null)
        {
        }

        public MemoryTable(IEnumerable<TEntity> initialData, string propertyId)
        {
            this.data = new List<TEntity>(initialData);
            this.propertyId = propertyId;
            this.lastIndex = initialData.Count();
        }

        #region Implementation of IEnumerable

        IEnumerator<TEntity> IEnumerable<TEntity>.GetEnumerator()
        {
            return this.data.GetEnumerator();
        }

        public IEnumerator GetEnumerator()
        {
            return this.data.GetEnumerator();
        }

        #endregion

        #region Implementation of IQueryable

        public Expression Expression
        {
            get { return this.data.AsQueryable().Expression; }
        }

        public Type ElementType
        {
            get { return this.data.AsQueryable().ElementType; }
        }

        public IQueryProvider Provider
        {
            get { return this.data.AsQueryable().Provider; }
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

            this.data.Add(data);
            // var a=Activator.CreateInstance(// typeof(TEntity).cop
            // auto-setting id property(Identity Specification)
            if (this.propertyId != null)
            {
                this.lastIndex++;
                typeof(TEntity).GetProperty(this.propertyId).SetValue(data, this.lastIndex, null);
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

                    this.tempData.Add(data);
                }
                catch (Exception)
                {
                    this.tempData.Clear();

                    throw;
                }
            }

            this.data.AddRange(this.tempData);
            // auto-setting id property(Identity Specification)
            if (this.propertyId != null)
            {
                this.tempData.ForEach(data =>
                {
                    this.lastIndex++;
                    typeof(TEntity).GetProperty(this.propertyId).SetValue(data, this.lastIndex, null);
                });
            }
            this.tempData.Clear();
        }

        public void Attach(object entity)
        {
            throw new NotImplementedException();
        }

        public void Attach(object entity, bool modified)
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

        public void AttachAll(IEnumerable entities, bool modified)
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

            this.data.Remove(data);
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

                    this.tempData.Add(data);
                }
                catch (Exception)
                {
                    this.tempData.Clear();

                    throw;
                }
            }

            foreach (var entity in this.tempData)
            {
                this.data.Remove(entity);
            }
            this.tempData.Clear();
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
    /*
    //  public class MemoryTable<TEntity> : IMockableTable<TEntity>
    //    where TEntity : class
    //  {
    //    protected List<TEntity> _Data;
    //    protected List<TEntity> _TempData = new List<TEntity>();

    //    public MemoryTable(IEnumerable<TEntity> initialData)
    //    {
    //        _Data = new List<TEntity>(initialData);
    //    }

    //    #region Implementation of IEnumerable

    //    IEnumerator<TEntity> IEnumerable<TEntity>.GetEnumerator()
    //    {
    //        return _Data.GetEnumerator();
    //    }

    //    public IEnumerator GetEnumerator()
    //    {
    //        return _Data.GetEnumerator();
    //    }

    //    #endregion

    //    #region Implementation of IQueryable

    //    public Expression Expression
    //    {
    //        get { return _Data.AsQueryable().Expression; }
    //    }

    //    public Type ElementType
    //    {
    //        get { return _Data.AsQueryable().ElementType; }
    //    }

    //    public IQueryProvider Provider
    //    {
    //        get { return _Data.AsQueryable().Provider; }
    //    }

    //    #endregion

    //    #region Implementation of ITable

    //    public void InsertOnSubmit(object entity)
    //    {
    //        var data = (TEntity) entity;

    //        if (entity == null || data == null)
    //        {
    //            throw new ArgumentException("Not null argument of type " + typeof(TEntity).Name + " is needed");
    //        }

    //        _Data.Add(data);
    //    }

    //    public void InsertAllOnSubmit(IEnumerable entities)
    //    {
    //        foreach (var entity in entities)
    //        {
    //            try
    //            {
    //                var data = (TEntity)entity;

    //                if (entity == null || data == null)
    //                {
    //                    throw new ArgumentException("Not null argument of type " + typeof(TEntity).Name + " is needed");
    //                }

    //                _TempData.Add(data);
    //            }
    //            catch (Exception)
    //            {
    //                _TempData.Clear();

    //                throw;
    //            }
    //        }

    //        _Data.AddRange(_TempData);
    //        _TempData.Clear();
    //    }

    //    public void Attach(object entity)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public void Attach(object entity, bool asModified)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public void Attach(object entity, object original)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public void AttachAll(IEnumerable entities)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public void AttachAll(IEnumerable entities, bool asModified)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public void DeleteOnSubmit(object entity)
    //    {
    //        var data = (TEntity)entity;

    //        if (entity == null || data == null)
    //        {
    //            throw new ArgumentException("Not null argument of type " + typeof(TEntity).Name + " is needed");
    //        }

    //        _Data.Remove(data);
    //    }

    //    public void DeleteAllOnSubmit(IEnumerable entities)
    //    {
    //        foreach (var entity in entities)
    //        {
    //            try
    //            {
    //                var data = (TEntity)entity;

    //                if (entity == null || data == null)
    //                {
    //                    throw new ArgumentException("Not null argument of type " + typeof(TEntity).Name + " is needed");
    //                }

    //                _TempData.Add(data);
    //            }
    //            catch (Exception)
    //            {
    //                _TempData.Clear();

    //                throw;
    //            }
    //        }

    //        foreach (var entity in _TempData)
    //        {
    //            _Data.Remove(entity);
    //        }
    //    }

    //    public object GetOriginalEntityState(object entity)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public ModifiedMemberInfo[] GetModifiedMembers(object entity)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public DataContext Context
    //    {
    //        get { throw new NotImplementedException(); }
    //    }

    //    public bool IsReadOnly
    //    {
    //        get { throw new NotImplementedException(); }
    //    }

    //    #endregion
    // }
    */
}
