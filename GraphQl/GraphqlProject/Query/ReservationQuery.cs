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


            Field<ListGraphType<ReservationType>>("filteredReservation")
             .Description("Returns all the Reservation or filtered menus based on provided criteria")
             .Arguments(new QueryArguments(
                 new QueryArgument<IntGraphType> { Name = "minId", Description = "Minimum Menu ID" },
                 new QueryArgument<IntGraphType> { Name = "maxId", Description = "Maximum Menu ID" }
             ))
             .Resolve(context =>
             {
                 var minId = context.GetArgument<int?>("minId");
                 var maxId = context.GetArgument<int?>("maxId");
                 return reservationRepository.GetFilteredReservation(minId, maxId);
             });
        }
    }
}
