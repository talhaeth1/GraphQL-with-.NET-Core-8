using GraphQL;
using GraphQL.Types;
using GraphqlProject.Interfaces;
using GraphqlProject.Type;

namespace GraphqlProject.Query
{
    public class ReservationQuery : ObjectGraphType
    {
        public ReservationQuery(IReservationRepository reservationRepository)
        {
            Field<ListGraphType<ReservationType>>("Reservation")
                .Description("Return Reservation list")
                .Resolve(context =>
            {
                return reservationRepository.GetReservations();
            });

            Field<ReservationType>("Reservations")
                .Description("Return Reservation list based on Id")
                .Arguments(new QueryArguments(new QueryArgument<IntGraphType> { Name = "reservationId" })).Resolve(context =>
            {
                return reservationRepository.GetReservationById(context.GetArgument<int>("reservationId"));
            });
        }
    }
}
