using System.Linq.Expressions;
using AS.Core;
using AS.Core.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace AS.Data
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class, IEntity, new()
    {
        private bool _disposed;

         private  readonly EfDbContext _context;
        private readonly DbSet<TEntity> _dbSet;
        private IDbContextTransaction _transaction;

        public Repository(IDbContextTransaction transaction)
        {
            _transaction = transaction;
        }
        public void BeginTransaction()
        {
            if (_context.Database.CurrentTransaction == null)
            {
                _context.Database.BeginTransaction();
            }
        }

        public async Task CommitAsync()
        {
            var transaction = _context.Database.CurrentTransaction;
            if (transaction != null)
            {
                await _context.SaveChangesAsync();
                transaction.Commit();
            }
        }

        public void Rollback()
        {
            var transaction = _context.Database.CurrentTransaction;
            if (transaction != null)
            {
                transaction.Rollback();
            }
        }
        public Repository()
        {
            //  _context = context;

           //  using var context = new EfDbContext();
            //   _dbSet = context.Set<TEntity>();
             // _context    = context;
          //  _dbSet = _context.Set<TEntity>();
        }

        public Repository(EfDbContext context)
        {
            _context = context;
           _dbSet = _context.Set<TEntity>();
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            _disposed = true;
        }


        public IQueryable<TEntity> GetSql(string sql)
        {
           return _dbSet.FromSqlRaw(sql);
        }

        public async Task<IQueryable<TEntity>> GetSqlAsync(string sql)
        {
             return await Task.FromResult(GetSql(sql));
        }

        public TEntity Get(Expression<Func<TEntity, bool>> filter, bool asNoTracking = false)
        {
            return asNoTracking ? _dbSet.AsNoTracking().FirstOrDefault(filter) : _dbSet.FirstOrDefault(filter);
        }

         public async Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> filter, bool asNoTracking = false)
        {
       
                return asNoTracking ?
                await _dbSet.AsNoTracking().FirstOrDefaultAsync(filter)
                : await _dbSet.FirstOrDefaultAsync(filter);
            
        }

        /// <summary>
        /// AsNoTracking kullanırsak yaptığımız select üzerinde herhangi bir update işlemi uygulayamıyoruz. 
        /// Yani değişikliği yaptıktan sonra entity.SaveChanges() diyerek update işlemi yapamayacağız.
        /// </summary>
        /// <param name="asNoTracking"></param>
        /// <returns></returns>
        public async Task<IQueryable<TEntity>> GetAll( bool asNoTracking = false)
        {
       
               var dbSet = _context.Set<TEntity>();
                    return asNoTracking
               ? dbSet.AsNoTracking()
               : dbSet;
            
       
        }

        /// <summary>
        /// AsNoTracking kullanırsak yaptığımız select üzerinde herhangi bir update işlemi uygulayamıyoruz. 
        /// Yani değişikliği yaptıktan sonra entity.SaveChanges() diyerek update işlemi yapamayacağız.
        /// </summary>
        /// <param name="asNoTracking"></param>
        /// <returns></returns>
        public async Task<IQueryable<TEntity>> GetAll(Expression<Func<TEntity, bool>> filter, bool asNoTracking = false)
        {

            return asNoTracking
                ? _dbSet.Where(filter).AsNoTracking()
                : _dbSet.Where(filter);


            //using (var context = new EfDbContext())
            //{
            //    var dbSet = _context.Set<TEntity>();
            //    return asNoTracking
            //   ? dbSet.Where(filter).AsNoTracking()
            //   : dbSet.Where(filter);
            //}

        }



        //public IIncludableJoin<TEntity, TProperty> Join<TProperty>(Expression<Func<TEntity, TProperty>> navigationProperty)
        //{
        //    var query = _dbSet.Join(navigationProperty);
        //    return query;
        //}

        public bool IsExist(Expression<Func<TEntity, bool>> filter)
        {

          
                return _context.Set<TEntity>().AsNoTracking().Any(filter);
            

        }
        public Task<bool> IsExistAsync(Expression<Func<TEntity, bool>> filter)
        {
                return _dbSet.AsNoTracking().AnyAsync(filter);  
        }

        public TEntity Insert(TEntity entity, bool autoSaveIsNotActive = false)
        {
        

                var entry = _dbSet.Add(entity);
                if (!autoSaveIsNotActive)
                {
                    SaveChanges();
                }
            return entry.Entity;
        

         }

    public virtual void InsertRange(IEnumerable<TEntity> entities, bool autoSaveIsNotActive = false)
        {

            _dbSet.AddRange(entities);
            if (!autoSaveIsNotActive)
            {
                _context.SaveChanges();
            }

        }
        public async Task<int> InsertRangeAsync(IEnumerable<TEntity> entities)
        {


                _dbSet.AddRangeAsync(entities);    
               
                 return await SaveChangesAsync();
  
        }
        public async Task<TEntity> InsertAsync(TEntity entity, bool autoSaveIsNotActive = false)
        {

            var entry = await _dbSet.AddAsync(entity);
            if (!autoSaveIsNotActive)
            {
                await SaveChangesAsync();
            }
            return entry.Entity;
        }

        public TEntity Update(TEntity entity, bool autoSaveIsNotActive = false)
        {
            var entry = _dbSet.Update(entity);
            if (!autoSaveIsNotActive)
            {
                SaveChanges();
            }

            return entry.Entity;
        }



        public virtual void UpdateRange(IEnumerable<TEntity> entities, bool autoSaveIsNotActive = false)
        {
            _dbSet.UpdateRange(entities);
            if (!autoSaveIsNotActive)
            {
                _context.SaveChanges();
            }
        }

        public async Task<TEntity> UpdateAsync(TEntity entity, bool autoSaveIsNotActive = false)
        {
            var affectedEntity = Task.FromResult(Update(entity,true));

            if (!autoSaveIsNotActive)
            {
                await SaveChangesAsync();
            }
            return await affectedEntity;
        }

        public void Delete(Guid Id, bool autoSaveIsNotActive = false)
        {
            var entity = Get(p => p.Id == Id);
            entity.IsDeleted=true;
            entity.UpdateTime=DateTime.Now;
            _dbSet.Update(entity);
            if (!autoSaveIsNotActive)
            {
                SaveChanges();
            }
        }

        public async Task DeleteAsync(TEntity entity, bool autoSaveIsNotActive = false)
        {
            entity.IsDeleted = true;
            entity.UpdateTime=DateTime.Now;
            _dbSet.Update(entity);
            if (!autoSaveIsNotActive)
            {
                await SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(Guid Id, bool autoSaveIsNotActive = false)
        {
            try
            {
                var entity = await GetAsync(p => p.Id == Id);
                entity.IsDeleted=true;
                entity.UpdateTime=DateTime.Now;
                _dbSet.Update(entity);
                if (!autoSaveIsNotActive)
                {
                    await SaveChangesAsync();
                }
            }
            catch (Exception ex )
            {

                throw;
            }
           
        }
        public async Task DeleteRangeAsync(IEnumerable<TEntity> entities, bool autoSaveIsNotActive = false)
        {

            foreach (var entity in entities)
            {
                entity.IsDeleted = true;
                entity.UpdateTime=DateTime.Now;
            }
            _dbSet.UpdateRange(entities);

            if (!autoSaveIsNotActive)
            {
                await SaveChangesAsync();
            }
        }

        public void DeleteRange(IEnumerable<TEntity> entities, bool autoSaveIsNotActive = false)
        {
           
            foreach (var entity in entities)
            {
                entity.IsDeleted = true;
                entity.UpdateTime = DateTime.Now;
            }
            _dbSet.UpdateRange(entities);

            if (!autoSaveIsNotActive)
            {
                _context.SaveChanges();
            }
        }


        public int SaveChanges()
        {
            return _context.SaveChanges();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }


        ~Repository()
        {
            Dispose(false);
        }

    }
}
