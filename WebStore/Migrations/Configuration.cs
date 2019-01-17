namespace WebStore.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<WebStore.Models.WebStoreModel>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "WebStore.Models.WebStoreModel";
        }

        protected override void Seed(WebStore.Models.WebStoreModel context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
            context.ProductCategories.AddOrUpdate(pc => pc.CategoryId,
                new Models.ProductCategory { CategoryId = 1, Name = "PC" },
                new Models.ProductCategory { CategoryId = 2, Name = "PS4" },
                new Models.ProductCategory { CategoryId = 3, Name = "Xbox One" },
                new Models.ProductCategory { CategoryId = 4, Name = "Switch" });


            context.Products.AddOrUpdate(p => p.ProductId,
                new Models.Product { ProductId = 1, Name = "Fallout 76", Description = "Play solo or join together as you explore, quest, build, and triumph against the wasteland’s greatest threats.", Price = 549, CategoryId = 1 },
                new Models.Product { ProductId = 2, Name = "God of War", Description = "Living as a man outside the shadow of the gods, Kratos must adapt to unfamiliar lands, unexpected threats, and a second chance at being a father. Together with his son Atreus, the pair will venture into the brutal Norse wilds and fight to fulfill a deeply personal quest.", Price = 579, CategoryId = 2 },
                new Models.Product { ProductId = 3, Name = "Horizon: Zero Dawn", Description = "Take on the role of skilled hunter Aloy as you explore a lush world inhabited by mysterious mechanized creatures in an exhilarating new Open World Action/ RPG.", Price = 459, CategoryId = 2 },
                new Models.Product { ProductId = 4, Name = "Red Dead Redemption 2", Description = "America, 1899. The end of the wild west era has begun as lawmen hunt down the last remaining outlaw gangs. Those who will not surrender or succumb are killed.", Price = 649, CategoryId = 3 },
                new Models.Product { ProductId = 5, Name = "The Legend of Zelda - Breath of the Wild", Description = "No kingdom. No memories. After a 100-year slumber, Link wakes up alone in a world he no longer remembers. Now the legendary hero must explore a vast and dangerous land and regain his memories before Hyrule is lost forever. Armed only with what he can scavenge, Link sets out to find answers and the resources needed to survive.", Price = 579, CategoryId = 4 },
                new Models.Product { ProductId = 6, Name = "Super Mario Party", Description = "The original 4-player Mario Party series board game mode that fans love is back, and your friends and family are invited to the party! Freely walk the board: choose where to move, which Dice Block to roll, and how to win the most Stars in skill-based minigames.", Price = 599, CategoryId = 4 },
                new Models.Product { ProductId = 7, Name = "Fifa 19", Description = "Led by the prestigious UEFA Champions League, FIFA 19 offers enhanced gameplay features that allow you to control the pitch in every moment, and provides new and unrivaled ways to play.", Price = 649, CategoryId = 2 },
                new Models.Product { ProductId = 8, Name = "Darksiders 3", Description = "Return to an apocalyptic Earth in Darksiders III, a hack-n-slash Action Adventure where players assume the role of FURY in her quest to hunt down and dispose of the Seven Deadly Sins.", Price = 599, CategoryId = 3 },
                new Models.Product { ProductId = 9, Name = "Slime Rancher", Description = "Slime Rancher is the tale of Beatrix LeBeau, a plucky, young rancher who sets out for a life a thousand light years away from Earth on the 'Far, Far Range' where she tries her hand at making a living wrangling slimes.", Price = 299, CategoryId = 3 },
                new Models.Product { ProductId = 10, Name = "The Sims 4", Description = "The Sims 4 is the life simulation gamein which you can create and control people. Experience the freedom to play with life in The Sims 4.", Price = 329, CategoryId = 1 });
        }
    }
}
