﻿namespace PrimeritaConsola
{
    public class Program
    {
        public static void Main()
        {
            IEjecutarEjercicio ejercicio;

           ejercicio= new EjercicioArrayPrimos();
           ejercicio.Ejecutar();

        }
    }

}

