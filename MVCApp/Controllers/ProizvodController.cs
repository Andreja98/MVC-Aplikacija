using MVCApp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCApp.Controllers
{
    public class ProizvodController : Controller
    {
        public ActionResult Index()
        {
            List<Proizvod> proizvodi = new List<Proizvod>();
            JSONReadWrite readWrite = new JSONReadWrite();
            proizvodi = JsonConvert.DeserializeObject<List<Proizvod>>(readWrite.Read("proizvodi.json", "data"));
            return View("~/Views/Proizvod/View.cshtml", proizvodi);
        }

        [HttpPost]
        public ActionResult Index(Proizvod proizvodi)
        {
            List<Proizvod> listaProzivoda = new List<Proizvod>();
            JSONReadWrite readWrite = new JSONReadWrite();
            listaProzivoda = JsonConvert.DeserializeObject<List<Proizvod>>(readWrite.Read("proizvodi.json", "data"));

            Proizvod proizvod = listaProzivoda.FirstOrDefault(x => x.Id == proizvodi.Id);

            if (proizvod == null)
            {
                listaProzivoda.Add(proizvodi);
            }
            else
            {
                int index = listaProzivoda.FindIndex(x => x.Id == proizvodi.Id);
                listaProzivoda[index] = proizvodi;
            }

            string jSONString = JsonConvert.SerializeObject(listaProzivoda);
            readWrite.Write(jSONString);

            return View("~/Views/Proizvod/View.cshtml", listaProzivoda);
        }
    }

    public class JSONReadWrite
    {
        public JSONReadWrite() { }

        public string Read(string fileName, string location)
        {
            var path = System.Web.HttpContext.Current.Server.MapPath("data/proizvodi.json");

            string jsonResult;

            using (StreamReader streamReader = new StreamReader(path))
            {
                jsonResult = streamReader.ReadToEnd();
            }
            return jsonResult;
        }

        public void Write(string jSONString)
        {

            var path = System.Web.HttpContext.Current.Server.MapPath("data/proizvodi.json");

            using (var streamWriter = File.CreateText(path))
            {
                streamWriter.Write(jSONString);
            }
        }
    }
}