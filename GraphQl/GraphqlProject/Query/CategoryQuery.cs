﻿using GraphQL;
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



            /*
            Field<ListGraphType<CategoryType>>("Categories").Resolve(context =>
            {
                return categoryResopistory.GetAllCategories();
            });
            */
        }
    }
}
