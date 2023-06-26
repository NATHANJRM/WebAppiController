using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using WebAppiController.Models;

namespace WebAppiController.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            List<peliculas> list = new List<peliculas>();
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:53041/");
                //ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12; // cuando se trabaje con http
                var request = client.GetAsync("api/Values").Result;

                if (request.IsSuccessStatusCode)
                {                    
                    string resultado = request.Content.ReadAsStringAsync().Result;
                    list = JsonConvert.DeserializeObject<List<peliculas>>(resultado);
                }
            }
            return View(list);
        }

        public ActionResult obtenerid (int id)
        {
            peliculas pel = new peliculas();
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:53041/");
                var request = client.GetAsync($"api/Values/{id}").Result;

                if (request.IsSuccessStatusCode)
                {
                    string result = request.Content.ReadAsStringAsync().Result;
                    pel = JsonConvert.DeserializeObject<peliculas>(result);
                }
                return View(pel);
            }            
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View("Create");
        }

        [HttpPost]
        public ActionResult Create(peliculas pelis)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:53041/");
                var request = client.PostAsJsonAsync("api/Values", pelis).Result;

                if (request.IsSuccessStatusCode)
                {
                    TempData["mensaje"] = "se agrego correctamente";
                    return RedirectToAction("Index");
                }

                TempData["mensaje"] = "error al ejecutar api controller";
                return View();
            }
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            peliculas pel = new peliculas();
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:53041/");
                var request = client.GetAsync($"api/Values/{id}").Result;

                if (request.IsSuccessStatusCode)
                {
                    string result = request.Content.ReadAsStringAsync().Result;
                    pel = JsonConvert.DeserializeObject<peliculas>(result);
                }
                return View(pel);
            }
            
        }

        [HttpPost]
        public ActionResult Edit(peliculas pel)
        {
            using(HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:53041/");
                var request = client.PutAsJsonAsync("api/Values", pel).Result;

                if (request.IsSuccessStatusCode)
                {
                    TempData["mensaje"] = "se edito correctamente";
                    return RedirectToAction("Index");
                }
                TempData["error"] = "error al ejecutar el api";
                return View();
            }
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            peliculas pel = new peliculas();
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:53041/");
                var request = client.GetAsync($"api/Values/{id}").Result;

                if (request.IsSuccessStatusCode)
                {
                    string result = request.Content.ReadAsStringAsync().Result;
                    pel = JsonConvert.DeserializeObject<peliculas>(result);
                }
                return View(pel);
            }
        }

        [HttpPost]
        public ActionResult Delete(peliculas pel)
        {
            using(HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:53041/");
                var request = client.DeleteAsync($"api/Values/{pel.id}").Result;

                if (request.IsSuccessStatusCode)
                {
                    TempData["mensaje"] = "Se elimino corectamente";
                    return RedirectToAction("Index");
                }
                TempData["error"] = "error al ejecutar el api";
                return View("Index");
            }
        }
    }
}
