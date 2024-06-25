using GraphQL;
using GraphQL.Types;
using GraphqlProject.Interfaces;
using GraphqlProject.Models;
using GraphqlProject.Type;

namespace GraphqlProject.Mutation
{
    public class MenuMutation : ObjectGraphType
    {
        public MenuMutation(IMenuRepository menuRepository)
        {
            Field<MenuType>("CreateMenu")
                .Description("Mutation used to create Menu")
                .Arguments(new QueryArguments(new QueryArgument<MenuInputType>() { Name = "menu" })).Resolve(context =>
                {
                    var menu = context.GetArgument<Menu>("menu");
                    return menuRepository.AddMenuItem(menu);
                });
            Field<MenuType>("UpdateMenu")
                .Description("Mutation used to Update Menu")
                .Arguments(new QueryArguments(new QueryArgument<IntGraphType>() { Name = "menuId" }, new QueryArgument<MenuInputType>() { Name = "menu" })).Resolve(context =>
                {
                    var menuId = context.GetArgument<int>("menuId");
                    var menu = context.GetArgument<Menu>("menu");
                    return menuRepository.UpdateMenu(menuId, menu);
                });
            Field<StringGraphType>("DeleteMenu")
                .Description("Mutation used to Delete Menu")
                .Arguments(new QueryArguments(new QueryArgument<IntGraphType>() { Name = "menuId" })).Resolve(context =>
                {
                    var menuId = context.GetArgument<int>("menuId");
                    menuRepository.DeleteMenu(menuId);
                    return $"The menu against this Id {menuId} has been deleted.";
                });

            Field<MenuType>("AddMenuWithCategoryId")
                .Description("Mutation used to add a new menu item with a specific CategoryId")
                .Arguments(new QueryArguments(
                    new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "categoryId", Description = "CategoryId for the menu item" },
                    new QueryArgument<NonNullGraphType<MenuInputType>> { Name = "menu", Description = "Menu input data" }
                ))
                .Resolve(context =>
                {
                    var categoryId = context.GetArgument<int>("categoryId");
                    var menu = context.GetArgument<Menu>("menu");
                    return menuRepository.AddMenuWithCategoryId(categoryId, menu);
                });
        }
    }
}
