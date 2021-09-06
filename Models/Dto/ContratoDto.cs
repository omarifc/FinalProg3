using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging; 

namespace ProyFutbol.Models
{
    public class ContratoDto
    {
        public int ContratoId {get; set;}
        public string Descripcion {get; set;}
        public double Sueldo {get; set;}
        public DateTime Inicio {get; set;}
        public int Duracion {get; set;}
        public int FutbolistaId {get; set;}
        public string Futbolista {get; set;}
    }
}