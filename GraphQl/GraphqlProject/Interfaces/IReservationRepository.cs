using GraphqlProject.Models;
using Microsoft.EntityFrameworkCore;

namespace GraphqlProject.Interfaces
{
    public interface IReservationRepository
    {
        List<Reservation> GetReservations();
        Reservation GetReservationById(int id);
        //List<Reservation> GetFilteredReservation(int? minId, int? maxId);

        Reservation AddReservation(Reservation reservation);
        Reservation AddReservationWithMenuId(int menuId, Reservation reservation);
        List<Reservation> AddMultiReservationsWithMenuId(int menuId, List<Reservation> multiReservations);
        Reservation UpdateReservation(int reservationId, Reservation reservation);
        void DeleteReservation(int id);
        void DeleteReservationsByMenuId(int menuId);
        //void AddMultiReservationsWithMenuId(int menuId, List<Reservation> reservations);
    }
}
