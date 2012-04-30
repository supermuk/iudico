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

        private readonly ITable table;

        private readonly IQueryable<TEntity> queryable;



        public MockableTable(ITable table, IQueryable<TEntity> queryable)
        {

            this.table = table;

            this.queryable = queryable;

        }



        public MockableTable(ITable table)

            : this(table, (IQueryable<TEntity>)table)
        {

        }



        public IEnumerator<TEntity> GetEnumerator()
        {

            return this.queryable.GetEnumerator();

        }



        IEnumerator IEnumerable.GetEnumerator()
        {

            return ((IEnumerable)this.queryable).GetEnumerator();

        }



        public Expression Expression
        {

            get { return this.queryable.Expression; }

        }



        public Type ElementType
        {

            get { return this.queryable.ElementType; }

        }



        public IQueryProvider Provider
        {

            get { return this.queryable.Provider; }

        }



        public void InsertOnSubmit(object entity)
        {

            this.table.InsertOnSubmit(entity);

        }



        public void InsertAllOnSubmit(IEnumerable entities)
        {

            this.table.InsertAllOnSubmit(entities);

        }



        public void Attach(object entity)
        {

            this.table.Attach(entity);

        }



        public void Attach(object entity, bool modified)
        {

            this.table.Attach(entity, modified);

        }



        public void Attach(object entity, object original)
        {

            this.table.Attach(entity, original);

        }



        public void AttachAll(IEnumerable entities)
        {

            this.table.AttachAll(entities);

        }



        public void AttachAll(IEnumerable entities, bool modified)
        {

            this.table.AttachAll(entities, modified);

        }



        public void DeleteOnSubmit(object entity)
        {

            this.table.DeleteOnSubmit(entity);

        }



        public void DeleteAllOnSubmit(IEnumerable entities)
        {

            this.table.DeleteAllOnSubmit(entities);

        }



        public object GetOriginalEntityState(object entity)
        {

            return this.table.GetOriginalEntityState(entity);

        }



        public ModifiedMemberInfo[] GetModifiedMembers(object entity)
        {

            return this.table.GetModifiedMembers(entity);

        }



        public DataContext Context
        {

            get { return this.table.Context; }

        }



        public bool IsReadOnly
        {

            get { return this.table.IsReadOnly; }

        }

    }
}
