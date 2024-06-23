using System.Diagnostics;
using GraphQL;
using GraphQL.Types;
using GraphQL.Validation.Rules;
using GraphqlProject.Interfaces;
using GraphqlProject.Models;
using GraphqlProject.Type;

namespace GraphqlProject.Query
{
    public class MenuQuery: ObjectGraphType
    {
        public MenuQuery(IMenuRepository menuRepository)
        {
            
            Field<ListGraphType<MenuType>>("Menus")
                .Description("Return List of Menu")
                .Resolve(context =>
            {
                return menuRepository.GetAllMenus();
            });


            Field<MenuType>("Menu")
                .Description("Return Menu details for a specific id")
                .Arguments(new QueryArguments(new QueryArgument<IntGraphType> { Name = "menuId"})).Resolve(context =>
            {
                return menuRepository.GetMenuById(context.GetArgument<int>("menuId"));
            });

            Field<ListGraphType<MenuType>>("filteredMenu")
              .Description("Returns all the menus or filtered menus based on provided criteria")
              .Arguments(new QueryArguments(
                  new QueryArgument<IntGraphType> { Name = "minId", Description = "Minimum Menu ID" },
                  new QueryArgument<IntGraphType> { Name = "maxId", Description = "Maximum Menu ID" }
              ))
              .Resolve(context =>
              {
                  var minId = context.GetArgument<int?>("minId");
                  var maxId = context.GetArgument<int?>("maxId");
                  return menuRepository.GetFilteredMenu(minId, maxId);
              });

            /*
             Define the "menus" field
            Field<ListGraphType<MenuType>>("menus")
                .Description("Return all the menus")
                .Resolve(context => menuRepository.GetAllMenus());

            // Define the "menu" field
            Field<MenuType>("menu")
                .Description("Return a single menu by ID")
                .Arguments(new QueryArguments(new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "menuId", Description = "Menu ID" }))
                .Resolve(context => menuRepository.GetMenuById(context.GetArgument<int>("menuId")));

             */


        }
    }
}
