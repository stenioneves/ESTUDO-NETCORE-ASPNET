using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASPNETMVC.Models
{
    public class HomeModel
    {
        public int Id { set; get; }
        public string Nome { set; get; }

        public List<HomeModel> GetAll()
        {
            HomeModel item;
            List<HomeModel> lista = new List<HomeModel>();
            item = new HomeModel();
            item.Id = 1;
            item.Nome = "Stenio Neves";
            lista.Add(item);

            item = new HomeModel();
            item.Id = 2;
            item.Nome = "Silva";
            lista.Add(item);
            
        
            return lista;
        }

    }
}
