using System.Diagnostics;
using GraphQL;
using GraphQL.Types;
using GraphqlProject.Interfaces;
using GraphqlProject.Models;
using GraphqlProject.Type;

namespace GraphqlProject.Query
{
    public class MenuQuery: ObjectGraphType
    {
        public MenuQuery(IMenuRepository menuRepository)
        {
            /*
            Field<ListGraphType<MenuType>>(
                "menus",
                "Return all the menus",
                resolve: context => menuRepository.GetAllMenus()
                );

                Field<MenuType>("Menu", "description",
                new QueryArguments(new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "id", Description = "desc" }),

                resolve: context => menuRepository.GetMenuById(context.GetArgument("id", int.MinValue))
                );
            */

            Field<ListGraphType<MenuType>>("Menus").Resolve(context =>
            {
                return menuRepository.GetAllMenus();
            });


            Field<MenuType>("Menu").Arguments(new QueryArguments(new QueryArgument<IntGraphType> { Name = "menuId"})).Resolve(context =>
            {
                return menuRepository.GetMenuById(context.GetArgument<int>("menuId"));
            });


            /*
            Field<ListGraphType<MenuType>>("Menus").Resolve(context =>
            {
                return menuRepository.GetAllMenus();
            });
            Field<MenuType>("Menu").Arguments(new QueryArguments(new QueryArgument<IntGraphType> { Name = "menuId" })).Resolve(context =>
            {
                return menuRepository.GetMenuById(context.GetArgument<int>("menuId"));
            });
            */
        }
    }
}
