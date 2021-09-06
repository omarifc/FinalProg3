using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging; 

namespace ProyFutbol.Models
{
    public class Liga
    {
        public int LigaId {get; set;}
        public string Nombre {get; set;}  
        public virtual Pais Pais{get; set;}      
        public virtual IEnumerable<Equipo> Equipos {get; set;}          
    }
}