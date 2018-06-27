using ClosedXML.Excel;
using Ejercicio.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
//using Microsoft.Office.Interop.Excel;
using System.Web.Http;
using System.Data.OleDb;
using System.Data;

namespace Ejercicio.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About(string Mejor, string Peor, decimal? Promedio, List<Funciones.Promedios> PromediosGrupos, string[] Nombres, decimal[] Calificaciones)
        {
            ViewBag.Mejor = Mejor;
            ViewBag.Peor = Peor;
            ViewBag.Promedio = Promedio;

            decimal[] data = new decimal[3];
            data[0] = PromediosGrupos[0].Promedio1;
            data[1] = PromediosGrupos[0].Promedio2;
            data[2] = PromediosGrupos[0].Promedio3;

            ViewBag.Promedios = data;
            ViewBag.Nombres = Nombres;
            ViewBag.Calificaciones = Calificaciones;

            return View();
        }

        public ActionResult CargarExcel(string path)
        {
            //path = @"C:\Users\Karen\Downloads\Calificaciones.xlsx";
            //path = @"C:\Users\CAHE\Downloads\Calificaciones.xlsx";
            var file = ReadExcelFile("Sheet1", path);
            var rows = file.Rows;

            var datos = Funciones.ObtenerDatos(rows);

            var calificacionMayor = Funciones.CalificacionMayor(datos);

            var calificacionMenor = Funciones.CalificacionMenor(datos);

            var promedioCalificaciones = Funciones.PromedioFinal(datos);

            var promediosGrupos = Funciones.PromediosGrupos(datos);

            var nombres = Funciones.NombresAlumnos(datos);
            var calificaciones = Funciones.CalificacionesAlumnos(datos);

            About(calificacionMayor, calificacionMenor, promedioCalificaciones, promediosGrupos, nombres, calificaciones);

            return View("About");
        }

        private DataTable ReadExcelFile(string sheetName, string path)
        {
            using (OleDbConnection conn = new OleDbConnection())
            {
                DataTable dt = new DataTable();
                string Import_FileName = path;
                string fileExtension = Path.GetExtension(Import_FileName);
                if (fileExtension == ".xls")
                    conn.ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Import_FileName + ";" + "Extended Properties='Excel 8.0;HDR=YES;'";
                if (fileExtension == ".xlsx")
                    conn.ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + Import_FileName + ";" + "Extended Properties='Excel 12.0 Xml;HDR=YES;'";
                using (OleDbCommand comm = new OleDbCommand())
                {
                    comm.CommandText = "Select * from [" + sheetName + "$]";

                    comm.Connection = conn;

                    using (OleDbDataAdapter da = new OleDbDataAdapter())
                    {
                        da.SelectCommand = comm;
                        da.Fill(dt);
                        return dt;
                    }
                }
            }
        }
    }
}