using Core.Entity;
using Core.Service;
using Model.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Service.Base
{
    public class BaseService<T> : ICoreService<T> where T : CoreEntity
    {
        private static ProjectContext _context;


        public ProjectContext context
        {
            get
            {
                if (_context == null)
                    _context = new ProjectContext();
                return _context;
            }
            set
            {
                _context = value;
            }
        }

        public bool Add(T item)
        {
            context.Set<T>().Add(item);
            return Save() > 0;
        }

        public bool Add(List<T> item)
        {
            context.Set<T>().AddRange(item);
            return Save() > 0;

        }

        public bool Any(Expression<Func<T, bool>> exp)
        {
            return context.Set<T>().Any(exp);
        }

        public List<T> GetActive()
        {
            return context.Set<T>().Where(m => m.Status != Core.Entity.Enum.Status.Deleted).ToList();
        }

        public List<T> GetAll()
        {
            return context.Set<T>().ToList();
        }

        public T GetByDefault(Expression<Func<T, bool>> exp)
        {
            return context.Set<T>().Where(exp).FirstOrDefault();
        }

        public T GetByID(Guid id)
        {
            return context.Set<T>().Find(id);
        }

        public List<T> GetDefault(Expression<Func<T, bool>> exp)
        {
            return context.Set<T>().Where(exp).ToList();
        }

        public bool Remove(T item)
        {
            item.Status = Core.Entity.Enum.Status.Deleted;
            return Update(item);
        }

        public bool Remove(Guid id)
        {
            T item = context.Set<T>().Find(id);
            item.Status = Core.Entity.Enum.Status.Deleted;
            DbEntityEntry entry = context.Entry(item);
            entry.CurrentValues.SetValues(item);
            return Save() > 0;
        }

        public bool RemoveAll(Expression<Func<T, bool>> exp)
        {
            var x = GetDefault(exp);
            int recordingCount = x.Count;
            int successCount = 0;
            List<T> rList = new List<T>();
            foreach (var item in x)
            {
                item.Status = Core.Entity.Enum.Status.Deleted;
                bool result = Update(item);
                if (result)
                {
                    rList.Add(item);
                    successCount++;

                }
            }
            if (successCount == recordingCount)
            {
                rList = null;
                return true;
            }
            else
            {
                foreach (var item in rList)
                {
                    item.Status = Core.Entity.Enum.Status.Updated;
                }
                rList = null;
                return false;

            }

        }

        public int Save()
        {
            return context.SaveChanges();
        }

        public bool Update(T item)
        {
            T updated = GetByID(item.ID);
            updated.Status = Core.Entity.Enum.Status.Updated;
            DbEntityEntry entry = context.Entry(updated);
            entry.CurrentValues.SetValues(item);
            return Save() > 0;
        }
        public void DetachEntity(T item)
        {
            context.Entry<T>(item).State = System.Data.Entity.EntityState.Detached;
        }

        public List<T> OrderByGet(Expression<Func<T, bool>> exp)
        {
            return context.Set<T>().OrderBy(exp).ToList();
        }

        public List<T> OrderByGet(Expression<Func<T, decimal?>> exp)
        {
            return context.Set<T>().OrderBy(exp).ToList();

        }

        public List<T> OrderByGet(Expression<Func<T, string>> exp)
        {
            return context.Set<T>().OrderBy(exp).ToList();

        }
    }
}
