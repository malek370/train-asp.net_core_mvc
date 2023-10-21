using BOL.Models;
using DAL.Repositories.Irepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication2.DAL.data;

namespace DAL.Repositories
{
    public class ComRepository : IComRepo
    {
        public readonly appDbContext _appDbContext;
        public ComRepository(appDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public void addCom(Commande commande)
        {
            commande.Id = Guid.NewGuid().ToString();
            _appDbContext.commandes.Add(commande);
            _appDbContext.SaveChanges();
        }
        public IEnumerable<Commande> getAll()
        {
            return _appDbContext.commandes.ToList();
        }

		public IEnumerable<Commande> getCom(string id)
		{
			return _appDbContext.commandes.Where(c => c.IdUser == id).ToList();
		}
	}
}
