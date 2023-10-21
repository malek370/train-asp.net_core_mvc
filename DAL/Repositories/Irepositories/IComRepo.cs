using BOL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.Irepositories
{
    public interface IComRepo
    {
        public void addCom(Commande c);
        public IEnumerable<Commande> getAll();
        public IEnumerable<Commande> getCom(string id);
    }
}
