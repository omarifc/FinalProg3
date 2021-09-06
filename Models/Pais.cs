using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging; 

namespace ProyFutbol.Models
{
    public class Pais
    {
        public int PaisId {get; set;}
        public string Nombre {get; set;}
        public virtual IEnumerable<Futbolista> Futbolistas {get; set;} 
        public virtual IEnumerable<Liga> Ligas {get; set;} 
        public virtual IEnumerable<Equipo> Equipos {get; set;}
    }
}