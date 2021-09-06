namespace ProyFutbol.Models
{
    public class FutbolistaPosicion
    {
        public int FutbolistaId {get; set;}
        public virtual Futbolista Futbolista {get; set;}
        public int PosicionId {get; set;}
        public virtual Posicion Posicion {get; set;}
    }
}