using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using System.Diagnostics;
using Using_API.HttpClientServices;
using Using_API.Models;

namespace Using_API.Controllers
{
    public class HomeController : Controller
    {
        private readonly HTTPService _service;

        public HomeController(HTTPService service)
        {
            _service = service;
        }
        [HttpGet]
        public async Task<ActionResult> Index()
        {
            var result = await _service.GetAll<Movie>();    
            return View(result);
        }
        [HttpGet]
        public async Task<ActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Create(NewMovie movie)
        {
            var result = await _service.Create<NewMovie>(movie);
            return RedirectToAction("Index");
            //return Ok(result);
        }
        
        [HttpGet]
        public async Task<ActionResult> Delete(int id)
        {
            var result = await _service.GetById<Movie>(id);
            return View(result);
        }

        [HttpPost]
        public async Task<ActionResult> DeleteById(int id)
        {
            var result = await _service.Delete<Movie>(id);

            //return Ok(result);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<ActionResult> Update(int id)
        {
            var result = await _service.GetById<Movie>(id);
            return View(result);
        }

        [HttpPost]
        public async Task<ActionResult> UpdateById(Movie movie)
        {
            var result = await _service.Update<Movie>(movie, movie.Id);

            //return Ok(result);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<ActionResult> Details(int id)
        {
            var result = await _service.GetById<Movie>(id);
            return View(result);
        }
    }
}