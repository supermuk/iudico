using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Linq.Expressions;
using IUDICO.Common.Models.Interfaces;

namespace IUDICO.Common.Models
{
    public class MockableTable<TEntity> : IMockableTable<TEntity>
    {
        private readonly ITable _Table;
        private readonly IQueryable<TEntity> _Queryable;

        public MockableTable(ITable table, IQueryable<TEntity> queryable)
        {
            _Table = table;
            _Queryable = queryable;
        }

        public MockableTable(ITable table)
            : this(table, (IQueryable<TEntity>)table)
        {
        }

        public IEnumerator<TEntity> GetEnumerator()
        {
            return _Queryable.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)_Queryable).GetEnumerator();
        }

        public Expression Expression
        {
            get { return _Queryable.Expression; }
        }

        public Type ElementType
        {
            get { return _Queryable.ElementType; }
        }

        public IQueryProvider Provider
        {
            get { return _Queryable.Provider; }
        }

        public void InsertOnSubmit(object entity)
        {
            _Table.InsertOnSubmit(entity);
        }

        public void InsertAllOnSubmit(IEnumerable entities)
        {
            _Table.InsertAllOnSubmit(entities);
        }

        public void Attach(object entity)
        {
            _Table.Attach(entity);
        }

        public void Attach(object entity, bool asModified)
        {
            _Table.Attach(entity, asModified);
        }

        public void Attach(object entity, object original)
        {
            _Table.Attach(entity, original);
        }

        public void AttachAll(IEnumerable entities)
        {
            _Table.AttachAll(entities);
        }

        public void AttachAll(IEnumerable entities, bool asModified)
        {
            _Table.AttachAll(entities, asModified);
        }

        public void DeleteOnSubmit(object entity)
        {
            _Table.DeleteOnSubmit(entity);
        }

        public void DeleteAllOnSubmit(IEnumerable entities)
        {
            _Table.DeleteAllOnSubmit(entities);
        }

        public object GetOriginalEntityState(object entity)
        {
            return _Table.GetOriginalEntityState(entity);
        }

        public ModifiedMemberInfo[] GetModifiedMembers(object entity)
        {
            return _Table.GetModifiedMembers(entity);
        }

        public DataContext Context
        {
            get { return _Table.Context; }
        }

        public bool IsReadOnly
        {
            get { return _Table.IsReadOnly; }
        }
    }
}
