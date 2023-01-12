using AutoMapper;
using Autoskola.Core.Models.ExceptionHandling;
using Autoskola.Repository.Data;
using Autoskola.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using PagedList;
using PagedList.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Autoskola.Repository.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly DbContext _context;
        private readonly DbSet<TEntity> _entities;

        public Repository(DbContext context)
        {
            _context = context;
            _entities = context.Set<TEntity>();
        }

        public async virtual Task<TEntity> Get(int id)
        {
            try
            {
                return await _entities.FindAsync(id);
            }
            catch(Exception ex)
            {
                throw new HttpException("Database connection error", 500);
            }
        }

        public async virtual Task<IEnumerable<TEntity>> GetAll(int pageNumber = 1, int pageSize = 100)
        {
            try
            {
                return await _entities.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
            }
            catch (Exception ex)
            {
                throw new HttpException("Database connection error", 500);
            }
        }

        public async virtual Task<IEnumerable<TEntity>> Find(Expression<Func<TEntity, bool>> predicate)
        {
            try
            {
                return await _entities.Where(predicate).ToListAsync();
            }
            catch (Exception ex)
            {
                throw new HttpException("Database connection error", 500);
            }
        }

        public async virtual Task<TEntity> SingleOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            try
            {
                return await _entities.SingleOrDefaultAsync(predicate);
            }
            catch (Exception ex)
            {
                throw new HttpException("Database connection error", 500);
            }
        }

        public async virtual void Add(TEntity entity)
        {
            try
            {
                await _entities.AddAsync(entity);
            }
            catch (Exception ex)
            {
                throw new HttpException("Database connection error", 500);
            }
        }

        public async virtual void AddRange(IEnumerable<TEntity> entities)
        {
            try
            {
                await _entities.AddRangeAsync(entities);
            }
            catch (Exception ex)
            {
                throw new HttpException("Database connection error", 500);
            }
        }

        public virtual void Remove(TEntity entity)
        {
            try
            {
                _entities.Remove(entity);
            }
            catch (Exception ex)
            {
                throw new HttpException("Database connection error", 500);
            }
        }

        public virtual void RemoveRange(IEnumerable<TEntity> entities)
        {
            try
            {
                _entities.RemoveRange(entities);
            }
            catch (Exception ex)
            {
                throw new HttpException("Database connection error", 500);
            }
        }
    }
}
