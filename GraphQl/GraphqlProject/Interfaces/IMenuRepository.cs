using GraphqlProject.Models;

namespace GraphqlProject.Interfaces
{
    public interface IMenuRepository
    {
        List<Menu> GetAllMenus();
        Menu GetMenuById(int id);
        List<Menu> GetFilteredMenu(int? minId, int? maxId);
        Menu AddMenuItem(Menu menu);
        Menu UpdateMenu(int menuId, Menu menu);
        void DeleteMenu(int menuId);
    }
}
