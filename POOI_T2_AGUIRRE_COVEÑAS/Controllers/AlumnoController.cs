using Newtonsoft.Json;
using POOI_T2_AGUIRRE_COVEÑAS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace POOI_T2_AGUIRRE_COVEÑAS.Controllers
{
    public class AlumnoController : Controller
    {
        // Definiendo la lista vacía
        static string jListAlumnos = @"[]";

        // Implementando el ActionResult Index

        public ActionResult Index()
        {
            try
            {
                List<Alumno> listIntermedia = JsonConvert.DeserializeObject<List<Alumno>>(jListAlumnos);
                return View(listIntermedia);
            }
            catch (Exception ex)
            {
                ViewBag.Mensaje = ex.Message;
                return View();
            }
        }

        // Implementando el ActionResult Agregar

        public ActionResult Agregar()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Agregar(Alumno alumno)
        {
            try
            {
                // 1. Deserializar el jListAlumnos
                List<Alumno> temporal = JsonConvert.DeserializeObject<List<Alumno>>(jListAlumnos);
                // 2. Verificar que el dni no se repita
                Alumno existente = temporal.Find(a => a.dni == alumno.dni);
                if (existente != null)
                {
                    ViewBag.Mensaje = "El DNI ya existe";
                    return View(alumno);
                }
                // 3. Si no existe, agregar el alumno
                temporal.Add(alumno);
                // 4. Serializar la lista y sobrescribir sobre jListAlumnos
                jListAlumnos = JsonConvert.SerializeObject(temporal);
                ViewBag.Mensaje = "Guardado de manera exitosa";
                return View(alumno);
            }
            catch (Exception ex)
            {
                ViewBag.Mensaje = ex.Message;
                return View();
            }
        }

        // Implementando el ActionResult Eliminar
        public ActionResult Eliminar(string dni)
        {
            // 1. Deserializar jListAlumnos
            List<Alumno> temporal = JsonConvert.DeserializeObject<List<Alumno>>(jListAlumnos);
            // 2. Buscar el alumno por dni
            Alumno alumnoEncontrado = temporal.Find(a => a.dni == dni);
            // 3. Eliminar de la lista
            if (alumnoEncontrado != null)
            {
                temporal.Remove(alumnoEncontrado);
            }
            // 4. Serializar de List<Alumno>
            jListAlumnos = JsonConvert.SerializeObject(temporal);
            // 5. Redireccionar a la vista Index
            return RedirectToAction("Index");
        }

        // Implementando el ActionResult Detalles

        public ActionResult Detalles(string dni)
        {
            // 1. Deserializar jListAlumnos
            List<Alumno> temporal = JsonConvert.DeserializeObject<List<Alumno>>(jListAlumnos);
            // 2. Buscar alumno por dni
            Alumno alumnoEncontrado = temporal.Find(a => a.dni == dni);
            return View(alumnoEncontrado);
        }

        // Implementando el ActionResult Actualizar

        public ActionResult Actualizar(string dni)
        {
            // 1. Deserializar jListAlumnos -> List<Alumno> temporal
            List<Alumno> temporal = JsonConvert.DeserializeObject<List<Alumno>>(jListAlumnos);
            // 2. Buscar el alumno por dni
            Alumno alumnoEncontrado = temporal.Find(a => a.dni == dni);
            return View(alumnoEncontrado);
        }

        [HttpPost]
        public ActionResult Actualizar(Alumno alumno)
        {
            // 1. Deserializar jListAlumnos -> List<Alumno> temporal
            List<Alumno> temporal = JsonConvert.DeserializeObject<List<Alumno>>(jListAlumnos);
            // 2. Buscar la posición del alumno por dni
            int index = temporal.FindIndex(a => a.dni == alumno.dni);
            if (index == -1)
            {
                ViewBag.Mensaje = "No existe un alumno con ese DNI";
                return View(alumno);
            }
            // 3. Reemplazar el elemento por el alumno actualizado
            temporal[index] = alumno;
            // 4. Serializar List<Alumno> -> jListAlumnos
            jListAlumnos = JsonConvert.SerializeObject(temporal);
            ViewBag.Mensaje = "Alumno actualizado";
            return View(alumno);
        }
    }
}