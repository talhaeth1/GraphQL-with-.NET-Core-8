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
            Field<ListGraphType<ReservationType>>("Reservation").Resolve(context =>
            {
                return reservationRepository.GetReservations();
            });

            Field<ReservationType>("Reservations").Arguments(new QueryArguments(new QueryArgument<IntGraphType> { Name = "reservationId" })).Resolve(context =>
            {
                return reservationRepository.GetReservationById(context.GetArgument<int>("reservationId"));
            });
        }
    }
}
