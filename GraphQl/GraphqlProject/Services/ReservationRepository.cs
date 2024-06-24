using GraphqlProject.Data;
using GraphqlProject.Interfaces;
using GraphqlProject.Models;
using Microsoft.EntityFrameworkCore;

namespace GraphqlProject.Services
{
    public class ReservationRepository(GraphQLDbContext dbContext) : IReservationRepository
    {
        private GraphQLDbContext dbContext = dbContext;

        public Reservation AddReservation(Reservation reservation)
        {
            ArgumentNullException.ThrowIfNull(reservation);
            if (!dbContext.Reservations.Any(x => x.Id == reservation.Id))
                dbContext.Reservations.Add(reservation);

            dbContext.SaveChanges();
            return reservation;
        }

        public void DeleteReservation(int id)
        {
            var reservation = dbContext.Reservations.Find(id) ?? throw new InvalidOperationException($"Reservation with id {id} doesn't exist.");
            dbContext.Reservations.Remove(reservation);
            dbContext.SaveChanges();
        }

        public List<Reservation> GetReservations()
        {
            return dbContext.Reservations
                 .Include(r => r.Menu)
                 .ToList();
            //return dbContext.Reservations.ToList<Reservation>();
        }

        public Reservation GetReservationById(int id)
        {
            /*if we are using primary key then use "Find" method
            return dbContext.Reservations.Find(id)
                ?? 
                throw new InvalidOperationException($"Reservation with Id {id} does not exist."); */
            return dbContext.Reservations.FirstOrDefault(x => x.Id == id)
                ?? 
                throw new InvalidOperationException($"Reservation with Id {id} does not exist."); 
        }


        /*public List<Reservation> GetFilteredReservation(int? minId, int? maxId)
        {
            var query = dbContext.Categories
                 .AsQueryable();
        
            query = minId.HasValue ? query.Where(e => e.Id >= minId.Value) : query;
            query = maxId.HasValue ? query.Where(e => e.Id <= maxId.Value) : query;
            return query.ToList();
        }*/


        public Reservation UpdateReservation(int reservationId, Reservation reservation)
        {
            ArgumentNullException.ThrowIfNull(reservation);

            if (dbContext.Reservations.Any(x => x.Id == reservationId))
            {
                var reservationResult = dbContext.Reservations.Find(reservationId);
                reservationResult.CustomerName = reservation.CustomerName;
                reservationResult.Email = reservation.Email;
                reservationResult.PhoneNumber = reservation.PhoneNumber;
                reservationResult.PartySize = reservation.PartySize;
                reservationResult.ReservationDate = reservation.ReservationDate;
                reservationResult.MenuId = reservation.MenuId;
            }

            dbContext.SaveChanges();
            return reservation;
        }
    }
}
