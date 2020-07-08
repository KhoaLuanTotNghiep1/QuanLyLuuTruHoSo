﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using S3Train.Domain;
using X.PagedList;

namespace S3Train
{
    /// <summary>
    /// Base generic class for Service
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class GenenicServiceBase<T> : IGenenicServiceBase<T> where T : EntityBase
    {
        protected readonly ApplicationDbContext DbContext;

        protected readonly DbSet<T> EntityDbSet;

        protected GenenicServiceBase(ApplicationDbContext dbContext)
        {
            DbContext = dbContext;
            EntityDbSet = dbContext.Set<T>();
        }

        public IQueryable<T> Query()
        {
            return EntityDbSet;
        }

        /// <summary>
        /// Get all item in Entity
        /// </summary>
        /// <returns>List item in Entity</returns>
        public IList<T> GetAll()
        {
            return EntityDbSet.ToList();
        }

        /// <summary>
        /// Get all item in Entity and sort by them
        /// </summary>
        /// <param name="orderBy">sort order</param>
        /// <returns>List item was sorted</returns>
        public IList<T> GetAll(Func<IQueryable<T>, IOrderedQueryable<T>> orderBy)
        {
            return orderBy(EntityDbSet).ToList();
        }

        /// <summary>
        /// Get list item with the condition for attributes
        /// </summary>
        /// <param name="predicate">condition for attributes</param>
        /// <returns>List item fit the condition</returns>
        public IEnumerable<T> Gets(Expression<Func<T, bool>> predicate)
        {
            return EntityDbSet.Where(predicate).ToList();
        }

        /// <summary>
        /// Get list item with the condition for attributes and sort by them
        /// </summary>
        /// <param name="predicate">condition for attributes</param>
        /// <returns>List item fit the condition was sorted</returns>
        public IEnumerable<T> Gets(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy)
        {
            return orderBy(EntityDbSet.Where(predicate)).ToList();
        }

        /// <summary>
        /// Get item by id
        /// </summary>
        /// <param name="id">id item</param>
        /// <returns>item</returns>
        public T GetById(string id)
        {
            return EntityDbSet.Find(id);
        }

        /// <summary>
        /// Get first item with the condition for attributes
        /// </summary>
        /// <param name="predicate">condition for attributes</param>
        /// <returns>Item fit the condition</returns>
        public T Get(Expression<Func<T, bool>> predicate) 
        {
            return EntityDbSet.FirstOrDefault(predicate);
        }

        public T Get(IQueryable<T> list, Expression<Func<T, bool>> predicate)
        {
            return list.FirstOrDefault(predicate);
        }

        /// <summary>
        /// Get first item with the condition for attributes
        /// </summary>
        /// <param name="predicate">condition for attributes</param>
        /// <returns>Item fit the condition</returns>
        public T Get(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy)
        {
            return orderBy(EntityDbSet.Where(predicate)).FirstOrDefault();
        }

        /// <summary>
        /// Insert item on entity
        /// </summary>
        /// <param name="item">item</param>
        /// <returns>item</returns>
        public T Insert(T item)
        {
            if (string.IsNullOrEmpty(item.Id))
                item.Id = Guid.NewGuid().ToString();

            item.NgayTao = DateTime.Now;
            item.TrangThai = true;
            var result = EntityDbSet.Add(item);
            DbContext.SaveChanges();
            return result;
        }

        /// <summary>
        /// Insert list item in entity
        /// </summary>
        /// <param name="items">list item</param>
        public void Insert(List<T> items)
        {
            EntityDbSet.AddRange(items);
            DbContext.SaveChanges();
        }

        /// <summary>
        /// Update item
        /// </summary>
        /// <param name="item">item</param>
        /// <returns>item</returns>
        public T Update(T item)
        {
            item.NgayCapNhat = DateTime.Now;
            EntityDbSet.Attach(item);
            DbContext.Entry(item).State = EntityState.Modified;
            DbContext.SaveChanges();
            return item;
        }

        /// <summary>
        /// Remove item on entity
        /// </summary>
        /// <param name="item">item</param>
        /// <returns>item</returns>
        public T Remove(T item)
        {
            var result = EntityDbSet.Remove(item);
            DbContext.SaveChanges();
            return result;
        }

        /// <summary>
        /// Remove list item fit the condition for attributes
        /// </summary>
        /// <param name="predicate">condition for attributes</param>
        public void Remove(Expression<Func<T, bool>> predicate)
        {
            var items = EntityDbSet.Where(predicate);
            EntityDbSet.RemoveRange(items);
            DbContext.SaveChanges();
        }

        public IPagedList<T> GetAllPaged(int? pageIndex, int pageSize = 20, Expression<Func<T, bool>> where = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null)
        {
            var page = pageIndex ?? 1;

            var entity = EntityDbSet;

            var iQueryableDbSet = @where != null ? entity.Where(@where) : entity;

            return orderBy != null ? orderBy(iQueryableDbSet).ToPagedList(page, pageSize) : iQueryableDbSet.ToPagedList(page, pageSize);
        }

        public IPagedList<T> GetAllPaged(IQueryable<T> list, int? pageIndex, int pageSize = 20, Expression<Func<T, bool>> where = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null)
        {
            var page = pageIndex ?? 1;

            var iQueryableDbSet = @where != null ? list.Where(@where) : list;

            return orderBy != null ? orderBy(iQueryableDbSet).ToPagedList(page, pageSize) : iQueryableDbSet.ToPagedList(page, pageSize);
        }
    }
}
