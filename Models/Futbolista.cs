using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging; 

namespace ProyFutbol.Models
{
    public class Futbolista
    {
        public int FutbolistaId {get; set;}
        public string Nombre {get; set;}
        public int Edad {get; set;}
        public virtual Pais Pais {get; set;}
        public string Descripcion {get; set;}
        public string Goles {get; set;}
        public virtual IEnumerable<FutbolistaPosicion> FutbolistaPosiciones {get; set;}
        public virtual Equipo Equipo{get; set;}
        public virtual Contrato Contrato {get; set;}
    }
}