using GraphQL.Types;
using GraphQL;
using GraphqlProject.Interfaces;
using GraphqlProject.Models;
using GraphqlProject.Type;
using System.Reflection.Metadata.Ecma335;

namespace GraphqlProject.Mutation
{
    public class ReservationMutation : ObjectGraphType
    {
        public ReservationMutation(IReservationRepository reservationRepository)
        {
            Field<ReservationType>("CreateReservation")
                .Description("Mutation used to create Reservation record")
                .Arguments(new QueryArguments(new QueryArgument<ReservationInputType>() { Name = "reservation" })).Resolve(context =>
                {
                    var reservation = context.GetArgument<Reservation>("reservation");
                    return reservationRepository.AddReservation(reservation);
                });
            Field<ReservationType>("UpdateReservation")
                .Description("Mutation used for Update Reservation record")
                .Arguments(new QueryArguments(new QueryArgument<IntGraphType>() { Name = "reservationId" }, new QueryArgument<ReservationInputType>() { Name = "reservation" })).Resolve(context =>
                {
                    var reservationId = context.GetArgument<int>("reservationId");
                    var reservation = context.GetArgument<Reservation>("reservation");
                    return reservationRepository.UpdateReservation(reservationId, reservation);
                });
            Field<StringGraphType>("DeleteReservation")
                .Description("Mutation used for Delete Reservation record")
                .Arguments(new QueryArguments(new QueryArgument<IntGraphType>() { Name = "reservationId" })).Resolve(context =>
                {
                    var reservationId = context.GetArgument<int>("reservationId");
                    reservationRepository.DeleteReservation(reservationId);
                    return $"The reservation against this Id {reservationId} has been deleted.";
                });

            Field<StringGraphType>("DeleteReservationByMenuId")
                .Description("Mutation used to delete all Reservations for a specific MenuId")
                .Arguments(new QueryArguments(new QueryArgument<IntGraphType> { Name = "menuId" })).Resolve(context =>
                {
                    var menuId = context.GetArgument<int>("menuId");
                    reservationRepository.DeleteReservationsByMenuId(menuId);
                    return $"All reservations against MenuId {menuId} have been deleted.";
                });

            Field<ListGraphType<ReservationType>>("AddReservationWithMenuId")
                .Description("Mutaiton used to create a reservation with a spcific menu id")
                .Arguments(
                    new QueryArguments(
                        new QueryArgument<IntGraphType> { Name = "menuId" },
                        new QueryArgument<ListGraphType<ReservationInputType>> { Name = "reservation" }
                    )
                ).Resolve(context =>
                {
                    var menuId = context.GetArgument<int>("menuId");
                    var reservation = context.GetArgument<List<Reservation>>("reservation");
                    var addedReservations = reservationRepository.AddMultiReservationsWithMenuId(menuId, reservation);
                    return addedReservations;
                });

            Field<ListGraphType<ReservationType>>("AddMultiReservationWithMenuId")
            .Description("Mutation used to create multiple reservations with a specific menu id")
            .Arguments(
                new QueryArguments(
                    new QueryArgument<IntGraphType> { Name = "menuId" },
                    new QueryArgument<ListGraphType<ReservationInputType>> { Name = "reservations" }
                )
            )
            .Resolve(context =>
            {
                var menuId = context.GetArgument<int>("menuId");
                try
                {
                    var reservations = context.GetArgument<List<Reservation>>("reservations");
                    var addedReservations = reservationRepository.AddMultiReservationsWithMenuId(menuId, reservations);
                    return addedReservations;
                }
                catch (Exception ex)
                {
                    context.Errors.Add(new ExecutionError($"An error occurred while adding reservations or MenuId = {menuId} doen't exist.", ex));
                    return null;
                }
            });
        }
    }
}
