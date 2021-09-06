using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging; 

namespace ProyFutbol.Models
{
    public class PaisDto
    {
        public int PaisId {get; set;}
        public string Nombre {get; set;}
        public List<FutbolistaDto> Futbolistas {get; set;}  
        public List<LigaDto> Ligas {get; set;}  
        public List<EquipoDto> Equipos {get; set;}  
    }
}