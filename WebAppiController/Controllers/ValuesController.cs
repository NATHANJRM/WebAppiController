using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAppiController.Models;

namespace WebAppiController.Controllers
{
    public class ValuesController : ApiController
    {
        // GET api/values
        ge19EntitiesAPIUNO data = new ge19EntitiesAPIUNO();
        public List<peliculas> Get()
        {
            List<peliculas> list = data.peliculas.ToList();
            return list;
            
        }

        // GET api/values/5
        
        public peliculas Get(int id)
        {
            peliculas per = data.peliculas.Find(id);
            data.Dispose();
            return per;
        }

        // POST api/values
        public void Post(peliculas per)
        {
            per.emicion = DateTime.Now;
            data.peliculas.Add(per);
            data.SaveChanges();
            data.Dispose();
        }

        // PUT api/values/5
        public void Put(peliculas per)
        {
            peliculas p = data.peliculas.Find(per.id);

            p.nombre = per.nombre;
            p.genero = per.genero;
            p.director = per.director;
            p.emicion = DateTime.Now;

            data.SaveChanges();
            data.Dispose();            
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
            peliculas per = data.peliculas.Find(id);

            data.peliculas.Remove(per);
            data.SaveChanges();
            data.Dispose();
        }

    }
}
