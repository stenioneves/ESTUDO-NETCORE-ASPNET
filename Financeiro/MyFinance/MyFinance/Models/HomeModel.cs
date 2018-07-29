using MyFinance.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;

namespace MyFinance.Models
{
    public class HomeModel
    {
        public string LerNomeUsuario()
        {
            DAL objDAL = new DAL();
            DataTable dt = objDAL.RetDataTable("select nome,email from usuario");
             if(dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    return dt.Rows[0]["nome"].ToString();
                }
            }
            return "nome não encontrado!";
        }
    }
}
