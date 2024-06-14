using GraphQL.Types;
using GraphqlProject.Models;

namespace GraphqlProject.Type
{
    public class ReservationType : ObjectGraphType<Reservation>
    {
        public ReservationType()
        {
            Field(r => r.Id, type: typeof(IdGraphType)).Description("Id property of Reservation Object");
            Field(r => r.CustomerName, type: typeof(StringGraphType)).Description("CustomerName property of Reservation Object");
            Field(r => r.Email, type: typeof(StringGraphType)).Description("Email property of Reservation Object");
            Field(r => r.PhoneNumber, type: typeof(StringGraphType)).Description("PhoneNumber property of Reservation Object");
            Field(r => r.SpecialRequest, type: typeof(StringGraphType)).Description("SpecialRequest property of Reservation Object");
            Field(r => r.PartySize, type: typeof(IdGraphType)).Description("PartySize property of Reservation Object");
            Field(r => r.ReservationDate, type: typeof(DateGraphType)).Description("ReservationDate property of Reservation Object");

            // Many-to-one menu relation
            Field(r => r.Menu, type: typeof(MenuType)).Description("Menu property for the reservation object");
        }

    }
    
    
}
