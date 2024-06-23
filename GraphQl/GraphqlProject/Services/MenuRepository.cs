using GraphqlProject.Data;
using GraphqlProject.Interfaces;
using GraphqlProject.Models;
using Microsoft.EntityFrameworkCore;

namespace GraphqlProject.Services
{
    public class MenuRepository(GraphQLDbContext dbContext) : IMenuRepository
    {
        private GraphQLDbContext dbContext = dbContext;

        public Menu AddMenuItem(Menu menu)
        {
            ArgumentNullException.ThrowIfNull(menu);
            if (!dbContext.Menus.Any(x => x.Id == menu.Id))
                dbContext.Menus.Add(menu);

            dbContext.SaveChanges();
            return menu;
        }

        public void DeleteMenu(int menuId)
        {
            /*var menu = dbContext.Menus.Find(menuId) 
                ?? 
                throw new InvalidOperationException($"Menu with id {menuId} doesn't exist.");
            dbContext.Menus.Remove(menu);
            dbContext.SaveChanges();*/
            var menu = dbContext.Menus
                .Include(m => m.Reservations)
                .Include(m => m.Category)
                .FirstOrDefault(m => m.Id == menuId) ?? throw new InvalidOperationException($"Menu with id {menuId} doesn't exist.");

            // Remove related reservations
            dbContext.Reservations.RemoveRange(menu.Reservations);
            // Optionally, remove the related category if needed
            dbContext.Categories.Remove(menu.Category);

            dbContext.Menus.Remove(menu);
            dbContext.SaveChanges();
        }

        public List<Menu> GetAllMenus()
        {
            return dbContext.Menus
                .Include(f => f.Category)
                .Include(m => m.Reservations)
                .ToList();
        }

        public Menu GetMenuById(int id)
        {
            //return dbContext.Menus.Find(id) ?? throw new InvalidOperationException($"Menu with Id {id} doesn't exist.");
            /*return dbContext.Menus.Find(id) 
                ??
                throw new InvalidOperationException($"Menu with Id {id} does not Exist");
            */
            return dbContext.Menus
                .Include(m => m.Category)
                .Include(m => m.Reservations)
                .FirstOrDefault(m => m.Id == id)
                ?? throw new InvalidOperationException($"Menu with Id {id} does not exist.");
        }

        public List<Menu> GetFilteredMenu(int? minId, int? maxId)
        {
            var query = dbContext.Menus
                 .Include(f => f.Category)
                 .Include(m => m.Reservations)
                 .AsQueryable();
            /*if (minId.HasValue)
            {
                query = query.Where(e => e.Id >= minId.Value);
            }
            if (maxId.HasValue) 
            {
                query = query.Where(e => e.Id <= maxId.Value);
            }*/
            query = minId.HasValue ? query.Where(e => e.Id >= minId.Value) : query;
            query = maxId.HasValue ? query.Where(e => e.Id <= maxId.Value) : query;
            return query.ToList();
        }

        public Menu UpdateMenu(int menuId, Menu menu)
        {
            ArgumentNullException.ThrowIfNull(menu);

            /*if (dbContext.Menus.Any(x => x.Id == menuId))
            {
                var menuResult = dbContext.Menus.Find(menuId);
                menuResult.Name = menu.Name;
                menuResult.Description = menu.Description;
                menuResult.Price = menu.Price;
            }
            dbContext.SaveChanges();
            return menu;*/
            Menu menuResult = null;

            if (dbContext.Menus.Any(x => x.Id == menuId)) 
            {
                 menuResult = dbContext.Menus
                .Include(m => m.Reservations)
                .Include(m => m.Category)
                .FirstOrDefault(m => m.Id == menuId);

                if(menuResult != null)
                {
                    menuResult.Name = menu.Name;
                    menuResult.Description = menu.Description;
                    menuResult.Price = menu.Price;
                    menuResult.ImageUrl = menu.ImageUrl;
                    menuResult.CategoryId = menu.CategoryId;

                    if (menu.Category != null)
                    {
                        menuResult.Category.Name = menu.Category.Name;
                        menuResult.Category.ImageUrl = menu.Category.ImageUrl;
                    }

                    if (menu.Reservations != null)
                    {
                        dbContext.Reservations.RemoveRange(menuResult.Reservations);
                        menuResult.Reservations = menu.Reservations;
                    }
                    dbContext.SaveChanges();
                }
                else
                {
                    throw new InvalidOperationException($"Menu with Id {menuId} does not exist");
                }
            }
            return menuResult;

        }
    }
}
