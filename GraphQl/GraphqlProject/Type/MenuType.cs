using GraphQL.Types;
using GraphqlProject.Models;
using GraphqlProject.Services;

namespace GraphqlProject.Type
{
    public class MenuType : ObjectGraphType<Menu>
    {
        public MenuType()
        {
            Field(m => m.Id, type: typeof(IdGraphType)).Description("Id property of manu Object");
            Field(m => m.Name, type: typeof(StringGraphType)).Description("Name property of manu Object");
            Field(m => m.Description, type: typeof(StringGraphType)).Description("Description property of manu Object");
            Field(m => m.Price, type: typeof(DecimalGraphType)).Description("Price property of manu Object");
            Field(m => m.ImageUrl, type: typeof(StringGraphType)).Description("ImageUrl property of manu Object");
            Field(m => m.CategoryId, type: typeof(IdGraphType)).Description("CategoryId property of manu Object");

            //one-to-one category relation
            Field(d => d.Category, type: typeof(CategoryType)).Description("Category property for the menu object");

            // One-to-many reservations relation
            Field(d => d.Reservations, type: typeof(ListGraphType<ReservationType>)).Description("List of Reservations for the menu object ");


        }
    }
}
