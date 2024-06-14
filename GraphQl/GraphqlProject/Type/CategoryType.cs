using System.Reflection.Metadata.Ecma335;
using GraphQL.Types;
using GraphqlProject.Interfaces;
using GraphqlProject.Models;
using GraphqlProject.Services;

namespace GraphqlProject.Type
{
    public class CategoryType : ObjectGraphType<Category>
    {
        public CategoryType(IMenuRepository menuRepository)
        {
            Field(c => c.Id, type: typeof(IdGraphType)).Description("Id property of Category Object");
            Field(c => c.Name, type: typeof(StringGraphType)).Description("Name property of Category Object");
            Field(c => c.ImageUrl, type: typeof(StringGraphType)).Description("ImageURL property of Category Object");

            /*Field<ListGraphType<MenuType>>("Menus").Resolve(context =>
            {
                //return new ListGraphType<MenuType>();
                return menuRepository.GetAllMenus();
            });*/
            Field<ListGraphType<MenuType>>("Menus").Resolve(context =>
            {
                return menuRepository.GetAllMenus().Where(m => m.CategoryId == context.Source.Id);
            });


            /*
            Field(c => c.Id);
            Field(c => c.Name);
            Field(c => c.ImageUrl);
            
            Field<ListGraphType<MenuType>>("Menus").Resolve(context =>
            {
                return menuRepository.GetAllMenus();
            });
            */
        }
    }
}
