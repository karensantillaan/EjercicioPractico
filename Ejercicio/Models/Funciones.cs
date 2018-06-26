using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ejercicio.Models
{
    public class Funciones
    {
        public static List<Calificaciones> ObtenerDatos(DataRowCollection rows)
        {
            List<Calificaciones> layoutList = new List<Calificaciones>();
            for (int i = 0; i < rows.Count; i++)
            {                
                Calificaciones layout = new Calificaciones();
                var element = rows[i].ItemArray;
                //var nombres = element[0];
                //var apellidoPaterno = element[1];
                //var apellidoMaterno = element[2];
                //var Grado = element[3];
                //var Grupo = element[4];
                //var Calificacion = element[5];

                layout.Nombres = element[0].ToString();
                layout.ApellidoPaterno = element[1].ToString();
                layout.ApellidoMaterno = element[2].ToString();
                layout.Grado = Convert.ToInt32(element[3]);
                layout.Grupo = element[4].ToString();
                layout.Calificacion = Convert.ToDecimal(element[5]);
                    
                layoutList.Add(layout);               
                
            }
            return layoutList;
        }

        public static string CalificacionMayor(List<Calificaciones> lista)
        {
            var MejorAlumno = "";
            decimal MaxCalificacion = 0; 
           
            for (int i = 0; i < lista.Count; i++)
            {
                if (lista[i].Calificacion > MaxCalificacion)
                {
                    MaxCalificacion = lista[i].Calificacion;
                    MejorAlumno = lista[i].Nombres + " " + lista[i].ApellidoPaterno + " " + lista[i].ApellidoMaterno;
                }
            }

            return MejorAlumno;
        }

        public static string CalificacionMenor(List<Calificaciones> lista)
        {
            var PeorAlumno = "";
            decimal MaxCalificacion = 10; 
           
            for (int i = 0; i < lista.Count; i++)
            {
                if (lista[i].Calificacion < MaxCalificacion)
                {
                    MaxCalificacion = lista[i].Calificacion;
                    PeorAlumno = lista[i].Nombres + " " + lista[i].ApellidoPaterno + " " + lista[i].ApellidoMaterno;
                }
            }

            return PeorAlumno;
        }

        public static decimal PromedioFinal(List<Calificaciones> lista)
        {
            decimal promedio = 0;
            decimal sumaCalif = 0;
            int totalAlumnos = lista.Count(); 

            for (int i = 0; i < lista.Count; i++)
            {
                sumaCalif += lista[i].Calificacion;
            }

            promedio = sumaCalif / totalAlumnos;

            return promedio; 
        }

        public static List<Promedios> PromediosGrupos(List<Calificaciones> lista)
        {
            decimal sumaCalif1 = 0;
            decimal sumaCalif2 = 0;
            decimal sumaCalif3 = 0;
            int total1 = 0;
            int total2 = 0;
            int total3 = 0;

            List<Promedios> promediosGrupos = new List<Promedios>();
            Promedios promedios = new Promedios();
            for (int i = 0; i < lista.Count; i++)
            {
                switch (lista[i].Grado)
                {
                    case 1:
                        sumaCalif1 += lista[i].Calificacion;
                        total1++;
                    break;
                    case 2:
                        sumaCalif2 += lista[i].Calificacion;
                        total2++;
                    break;
                    case 3:
                        sumaCalif3 += lista[i].Calificacion;
                        total3++;
                    break;

                }                
            }

            promedios.Promedio1 = sumaCalif1 / total1;
            promedios.Promedio2 = sumaCalif2 / total2;
            promedios.Promedio3 = sumaCalif3 / total3;
            promediosGrupos.Add(promedios);

            return promediosGrupos;
        }

        public class Calificaciones
        {
            public string ApellidoPaterno { get; set; }
            public string ApellidoMaterno { get; set; }
            public string Nombres { get; set; }
            public int Grado { get; set; }
            public string Grupo { get; set; }
            public decimal Calificacion { get; set; }
        }

        public class Promedios
        {
            public decimal Promedio1 { get; set; }
            public decimal Promedio2 { get; set; }
            public decimal Promedio3 { get; set; }
        }
    }
}