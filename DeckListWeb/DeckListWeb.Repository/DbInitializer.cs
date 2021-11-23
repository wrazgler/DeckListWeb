using Microsoft.EntityFrameworkCore;
using System.Linq;

using DeckListWeb.Repository.Models;

namespace DeckListWeb.Repository
{
    public class DbInitializer
    {
        public static void Initialize(RepositoryContext db)
        {
            db.Database.EnsureCreated();
            db.Database.Migrate();

            if (db.Decks.Any()) 
                return;

            var newdeck = new Deck { Number = 1 };
            db.Decks.Add(newdeck);

            newdeck.CreateDeck(db, newdeck);

            db.SaveChanges();
        }
    }
}