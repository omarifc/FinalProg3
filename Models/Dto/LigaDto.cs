using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging; 

namespace ProyFutbol.Models
{
    public class LigaDto
    {
        public int LigaId {get; set;}
        public string Nombre {get; set;}  
        public string Pais {get;set;}     
        public List<EquipoDto> Equipos {get; set;}          
    }
}