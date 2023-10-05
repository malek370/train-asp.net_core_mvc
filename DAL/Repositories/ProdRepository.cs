using BOL.Models;
using DAL.Repositories.Irepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication2.BOL.Models;
using WebApplication2.DAL.data;

namespace DAL.Repositories
{
    public class ProdRepository : IProdRepository
    {
        public readonly appDbContext appdb;
        public ProdRepository(appDbContext appdb)
        {
            this.appdb = appdb;
        }

        public bool Add(Product item)
        {
            try
            {
                appdb.products.Add(item);
                return true;
            }
            catch { return false; }
        }

        public Product Get(int id)
        {
            return appdb.products.FirstOrDefault(c => c.Id == id);
        }

        public IEnumerable<Product> GetAll()
        {
            return appdb.products.ToList();
        }

        public bool Remove(int id)
        {
            try
            {
                appdb.products.Remove(Get(id));
                return true;
            }
            catch { return false; }
        }

        public bool Save()
        {
            try
            {
                appdb.SaveChanges();
                return true;
            }
            catch { return false; }
        }

        public bool Update(Product prod)
        {
            try
            {
                appdb.products.Update(prod);
                return true;
            }
            catch { return false; }
        }
    }
}
