using System.Diagnostics;
using GraphQL;
using GraphQL.Types;
using GraphqlProject.Interfaces;
using GraphqlProject.Models;
using GraphqlProject.Type;

namespace GraphqlProject.Mutation
{
    public class CategoryMutation : ObjectGraphType
    {
        public CategoryMutation(ICategoryRepository categoryRepository)
        {
            Field<CategoryType>("CreateCategory")
                .Description("Mutation used to create Category")
                .Arguments(new QueryArguments(
                    new QueryArgument<CategoryInputType>() { Name = "category" }))
                .Resolve(context =>
                {
                    var category = context.GetArgument<Category>("category");
                    return categoryRepository.AddCategory(category);
                });


            Field<CategoryType>("UpdateCategory")
                .Description("Mutation used to Update Category")
                .Arguments(new QueryArguments(new QueryArgument<IntGraphType>() { Name = "categoryId" }, 
                 new QueryArgument<CategoryInputType>() { Name = "category" }))
                .Resolve(context =>
                {
                    var categoryId = context.GetArgument<int>("categoryId");
                    var category = context.GetArgument<Category>("category");
                    return categoryRepository.UpdateCategory(categoryId, category);
                });
            Field<StringGraphType>("DeleteCategory")
                .Description("Mutation used to Delete Category")
                .Arguments(new QueryArguments(new QueryArgument<IntGraphType>() { Name = "categoryId" })).Resolve(context =>
                {
                    var categoryId = context.GetArgument<int>("categoryId");
                    categoryRepository.DeleteCategory(categoryId);
                    return $"The category against this Id {categoryId} has been deleted.";
                });
        }
    }
}
