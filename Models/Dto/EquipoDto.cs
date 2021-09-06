using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging; 

namespace ProyFutbol.Models
{
    public class EquipoDto
    {
        public int EquipoId {get; set;}
        public string Nombre {get; set;}   
        public List<FutbolistaDto> Futbolistas {get; set;}    
        public string Liga {get; set;}
        public string Pais {get;set;}
        public string PartidosGanados {get; set;}
        public string PartidosEmpatados {get; set;}
        public string PartidosPerdidos {get; set;}
    }
}