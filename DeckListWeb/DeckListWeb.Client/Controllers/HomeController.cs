using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

using DeckListWeb.Client.ViewModels;

using DeckListWeb.Model.Interfaces;
using DeckListWeb.Model.Models;

namespace DeckListWeb.Client.Controllers
{
    public class HomeController : Controller
    {
        private readonly IDeckListService _deckListService;

        public HomeController(IDeckListService deckService)
        {
            _deckListService = deckService;
        }
        [HttpGet]
        public IActionResult GetAllDecks(int page = 1)
        {
            return View(_deckListService.GetAllDecks(page));
        }

        [HttpGet]
        public async Task<IActionResult> GetDeck(int id = 1, int page = 1)
        {
            var deck = await _deckListService.GetDeckAsync(id);
            var model = new GetDeckViewModel { Deck = deck, Page = page };

            return View(model);
        }

        [HttpGet]
        public IActionResult AddDeck(int page = 1)
        {
            var model = new AddDeckViewModel { Page = page };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddDeck(AddDeckViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            await _deckListService.AddDeckAsync(model.Name);

            return RedirectToAction("GetAllDecks", "Home", new { page = model.Page });
        }

        [HttpGet]
        public async Task<IActionResult> DeleteDeck(int id, int page = 1, string previous = "GetAllDecks")
        {
            var deck = await _deckListService.GetDeckAsync(id);
            var model = new DeleteDeckViewModel { Deck = deck, Page = page, PreviousPage = previous };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteDeck(DeleteDeckViewModel model)
        {
            await _deckListService.DeleteDeckAsync(model.Deck.Id);

            return RedirectToAction("GetAllDecks", "Home", new { page = model.Page });
        }

        [HttpGet]
        public async Task<IActionResult> EditDeck(int id, int page = 1, string previous = "PrintDecks")
        {
            var deck = await _deckListService.GetDeckAsync(id);
            var model = new EditDeckViewModel { Id = deck.Id, Name = deck.Name, Page = page, PreviousPage = previous };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditDeck(EditDeckViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            await _deckListService.EditDeckAsync(model.Id, model.Name);

            return RedirectToAction(model.PreviousPage, "Home", new { id = model.Id, page = model.Page });
        }

        public async Task<IActionResult> ShuffleDeck(int id = 1, int page = 1, string previous = "GetAllDecks")
        {
            await _deckListService.ShuffleDeckAsync(id);

            return RedirectToAction(previous, "Home", new { id = id, page = page });
        }

        [HttpGet]
        public async Task<IActionResult> ManualShuffleDeck(int id = 1, int page = 1, string previous = "GetAllDecks")
        {
            await _deckListService.ManualShuffleDeckAsync(id);

            return RedirectToAction(previous, "Home", new { id = id, page = page });
        }
    }
}