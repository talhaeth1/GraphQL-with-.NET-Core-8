using System.ComponentModel.DataAnnotations.Schema;

namespace GraphqlProject.Models
{
    [Table("Reservations")]
    public class Reservation
    {
        public int Id { get; set; }
        public string CustomerName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public int PartySize { get; set; }
        public string SpecialRequest { get; set; }
        public DateTime ReservationDate { get; set; }

        // Foreign key and navigation property for Menu
        public int MenuId { get; set; }
        public Menu Menu { get; set; }
    }
}
