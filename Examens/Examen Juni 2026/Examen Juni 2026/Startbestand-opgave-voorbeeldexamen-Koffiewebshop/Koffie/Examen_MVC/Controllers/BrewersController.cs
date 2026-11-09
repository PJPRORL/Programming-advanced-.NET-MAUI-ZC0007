using AutoMapper;
using Examen_MVC.Data;
using Examen_MVC.Models;
using Examen_MVC.ViewModels.Brewer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Examen_MVC.Controllers
{
    public class BrewersController : Controller
    {
        private UnitOfWork _uow;
        private IMapper _mapper;

        public BrewersController(UnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _uow.BrewerRepository.GetAllAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Brewer? brewer = await _uow.BrewerRepository.GetByIdAsync(id);
            if (brewer == null)
            {
                return NotFound();
            }

            return View(brewer);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AddBrewerVM dto)
        {
            if (ModelState.IsValid)
            {
                Brewer brewer = _mapper.Map<Brewer>(dto);
                await _uow.BrewerRepository.AddAsync(brewer);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(dto);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Brewer? brewer = await _uow.BrewerRepository.GetByIdAsync(id.Value);
            EditBrewerDTO dto = _mapper.Map<EditBrewerDTO>(brewer);
            if (brewer == null)
            {
                return NotFound();
            }

            return View(dto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EditBrewerDTO dto)
        {
            if (id != dto.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    Brewer brewer = _mapper.Map<Brewer>(dto);
                    await _uow.BrewerRepository.UpdateAsync(brewer);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _uow.BrewerRepository.ItemExists(dto.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(dto);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Brewer? brewer = await _uow.BrewerRepository.GetByIdAsync(id.Value);
            if (brewer == null)
            {
                return NotFound();
            }

            return View(brewer);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            Brewer? brewer = await _uow.BrewerRepository.GetByIdAsync(id);
            if (brewer != null)
            {
                await _uow.BrewerRepository.DeleteAsync(brewer);
            }

            return RedirectToAction(nameof(Index));
        }
    }
}