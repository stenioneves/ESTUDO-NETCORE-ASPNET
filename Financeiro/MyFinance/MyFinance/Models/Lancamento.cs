using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using MyFinance.Util;


namespace MyFinance.Models
{
    public class Lancamento
    {

        private int Id { get; set; }
        private string Nome { get; set; }

        private double Valor { get; set; }

        private string Data { get; set; }

        private string classificacao { get; set; }


        //Atributo somente para relacionament com a transação.
        private int IdTransacao { get; set; }
       
        //localização para uso Online e o bot
         long lon { get; set; } //Longetude
         long lat { get; set; } //Latitude


    }
}