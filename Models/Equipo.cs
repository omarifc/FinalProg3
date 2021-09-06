using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging; 

namespace ProyFutbol.Models
{
    public class Equipo
    {
        public int EquipoId {get; set;}
        public string Nombre {get; set;}   
        public virtual IEnumerable<Futbolista> Futbolistas {get; set;}    
        public virtual Liga Liga {get; set;}
        public virtual Pais Pais {get;set;} // Agregado
        public string PartidosGanados {get; set;}
        public string PartidosEmpatados {get; set;}
        public string PartidosPerdidos {get; set;}
    }
}