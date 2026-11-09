using AutoMapper;
using Examen_MVC.Data;
using Examen_MVC.Models;
using Examen_MVC.ViewModels.Cart;
using Microsoft.AspNetCore.Mvc;

namespace Examen_MVC.Controllers
{
    public class CartController : Controller
    {
        private IMapper _mapper;
        private UnitOfWork _ouw;

        public CartController(UnitOfWork unitOfWork, IMapper mapper)
        {
            _ouw = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            // TODO: Haal user Id op
            string userId = "";

            Cart[] models = await _ouw.CartRepository.GetItemsInCartForUser(userId);
            GetItemInCartVM[] dtos = _mapper.Map<GetItemInCartVM[]>(models);

            CartVM cartVM = new CartVM();
            cartVM.ItemsInCart = dtos;

            return View(cartVM);
        }

        [HttpPost]
        public async Task<IActionResult> AddItemToCart(int id)
        {
            // TODO: Zoek of het item al in de winkelwagen staat voor de gebruiker
            Cart? existingItem = null;

            // TODO: Haal user Id op
            string userId = "";

            if (existingItem != null)
            {
                // TODO: Als het item al bestaat, verhoog de quantity met 1
            }
            else
            {
                // TODO: Als het item niet bestaat, voeg dan een nieuw item toe
                Cart model = new Cart
                {
                    CoffeeId = id,
                    Quantity = 1,
                    UserId = userId,
                };
            }

            // TODO: Sla op in DB

            return RedirectToAction("Index", "Coffee");
        }

        [HttpPost]
        public async Task<IActionResult> RemoveItem(int id)
        {
            // TODO: Verwijder item uit cart
            return RedirectToAction("Index", "Cart");
        }

        public async Task<IActionResult> Checkout()
        {
            // TODO: Haal user Id op
            string? userId = "";

            await _ouw.CartRepository.ClearCartAsync(userId);
            await _ouw.SaveChangesAsync();

            return RedirectToAction("Index", "Cart");
        }
    }
}