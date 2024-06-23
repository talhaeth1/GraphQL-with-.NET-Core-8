﻿using GraphQL.Types;
using GraphQL;
using GraphqlProject.Interfaces;
using GraphqlProject.Models;
using GraphqlProject.Type;

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
        }
    }
}
