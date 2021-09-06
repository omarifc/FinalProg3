using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging; 

namespace ProyFutbol.Models
{
    public class Posicion
    {
        public int PosicionId {get; set;}
        public string Nombre {get; set;}        
        public virtual IEnumerable<FutbolistaPosicion> FutbolistaPosiciones {get; set;} 
    }
}