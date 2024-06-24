using GraphQL;
using GraphQL.Types;
using GraphqlProject.Interfaces;
using GraphqlProject.Services;
using GraphqlProject.Type;

namespace GraphqlProject.Query
{
    public class CategoryQuery : ObjectGraphType
    {
        public CategoryQuery(ICategoryRepository categoryResopistory)
        {
            /*
            Field<ListGraphType<CategoryType>>(
                "Categories",
                "Return all the Categories",
                resolve: context => categoryResopistory.GetAllCategories()
                );
            */


            Field<ListGraphType<CategoryType>>("categories")
                .Description("Return Category list")
                .Resolve(context =>
                {
                    return categoryResopistory.GetAllCategories();
                });

            Field<CategoryType>("category")
                .Description("Return specific Category list based on Id")
                .Arguments(new QueryArguments(new QueryArgument<IntGraphType> { Name = "categoryId" })).Resolve(context =>
                {
                    return categoryResopistory.GetCategoryById(context.GetArgument<int>("categoryId"));
                });

            /*Field<ListGraphType<CategoryType>>("filteredCategoty")
             .Description("Returns all the Category or filtered menus based on provided criteria")
             .Arguments(new QueryArguments(
                 new QueryArgument<IntGraphType> { Name = "minId", Description = "Minimum Menu ID" },
                 new QueryArgument<IntGraphType> { Name = "maxId", Description = "Maximum Menu ID" }
             ))
             .Resolve(context =>
             {
                 var minId = context.GetArgument<int?>("minId");
                 var maxId = context.GetArgument<int?>("maxId");
                 return categoryResopistory.GetFilteredCategory(minId, maxId);
             });*/
        }
    }
}
