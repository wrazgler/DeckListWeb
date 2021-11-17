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

            var deck1 = new Deck { Number = 1 };
            db.Decks.Add(deck1);

            deck1.CreateDeck(db, deck1);

            db.SaveChanges();
        }
    }
}