using AutoMapper;
using Examen_MVC.Data;
using Examen_MVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace Examen_MVC.Controllers
{
    public class CoffeeController : Controller
    {
        private UnitOfWork _uow;
        private IMapper _mapper;

        public CoffeeController(UnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            // TODO: Toon alle koffies in DB
            return View();
        }

        public async Task<IActionResult> Details(int id)
        {
            // TODO: Toon detail van geselecteerde koffie
            return View();
        }

        public async Task<IActionResult> Create()
        {
            AddCoffeeVM viewModel = new AddCoffeeVM();

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAsync(AddCoffeeVM vm)
        {
            // TODO: Voeg nieuwe koffie toe
            if (ModelState.IsValid)
            {
                var coffee = _mapper.Map<Coffee>(vm);
                coffee.Image = "unknown.jpg";
                await _uow.CoffeeRepository.AddAsync(coffee);

                return RedirectToAction("Index", "Coffee");
            }
            else
            {
                return View(vm);
            }
        }
    }
}