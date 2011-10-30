﻿using System;
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

            return queryable.GetEnumerator();

        }



        IEnumerator IEnumerable.GetEnumerator()
        {

            return ((IEnumerable)queryable).GetEnumerator();

        }



        public Expression Expression
        {

            get { return queryable.Expression; }

        }



        public Type ElementType
        {

            get { return queryable.ElementType; }

        }



        public IQueryProvider Provider
        {

            get { return queryable.Provider; }

        }



        public void InsertOnSubmit(object entity)
        {

            table.InsertOnSubmit(entity);

        }



        public void InsertAllOnSubmit(IEnumerable entities)
        {

            table.InsertAllOnSubmit(entities);

        }



        public void Attach(object entity)
        {

            table.Attach(entity);

        }



        public void Attach(object entity, bool asModified)
        {

            table.Attach(entity, asModified);

        }



        public void Attach(object entity, object original)
        {

            table.Attach(entity, original);

        }



        public void AttachAll(IEnumerable entities)
        {

            table.AttachAll(entities);

        }



        public void AttachAll(IEnumerable entities, bool asModified)
        {

            table.AttachAll(entities, asModified);

        }



        public void DeleteOnSubmit(object entity)
        {

            table.DeleteOnSubmit(entity);

        }



        public void DeleteAllOnSubmit(IEnumerable entities)
        {

            table.DeleteAllOnSubmit(entities);

        }



        public object GetOriginalEntityState(object entity)
        {

            return table.GetOriginalEntityState(entity);

        }



        public ModifiedMemberInfo[] GetModifiedMembers(object entity)
        {

            return table.GetModifiedMembers(entity);

        }



        public DataContext Context
        {

            get { return table.Context; }

        }



        public bool IsReadOnly
        {

            get { return table.IsReadOnly; }

        }

    }
}
