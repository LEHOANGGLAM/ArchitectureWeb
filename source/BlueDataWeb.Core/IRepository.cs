using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;

namespace BlueDataWeb.Core
{

    public class DB_Entities : DbContext
    {
        public DB_Entities() : base("BlueDataWebEntities") { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer<DB_Entities>(null); 
            base.OnModelCreating(modelBuilder);
        }
    }



    //The Generic Interface Repository for Performing Read/Add/Delete operations
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T GetById(int id);
        void Insert(T obj);
        void Update(T obj);
        void Delete(int obj);
        void Save();

    }


    public class GenericRepository<T> : IRepository<T> where T : class
    {

        public DB_Entities _context = null;
        public DbSet<T> table = null;
        public GenericRepository()
        {

            this._context = new DB_Entities();
            this._context.Configuration.ValidateOnSaveEnabled = false;
            this.table = _context.Set<T>();
        }
        public GenericRepository(DB_Entities _context)
        {
            this._context = _context;
            this.table = _context.Set<T>();
        }
        public IEnumerable<T> GetAll()
        {

            return table.ToList();
        }

        public T GetById(int id)
        {
            return table.Find(id);
        }
        public void Insert(T obj)
        {
            table.Add(obj);
        }
        public void Update(T obj)
        {
            table.Attach(obj);
            _context.Entry(obj).State = EntityState.Modified;
        }
        public void Delete(int id)
        {
            var data = table.Find(id);
            table.Remove(data);

        }
        public void Save()
        {
            _context.SaveChanges();
        }

    }


}
