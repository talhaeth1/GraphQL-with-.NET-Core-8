using System.ComponentModel.DataAnnotations.Schema;

namespace GraphqlProject.Models
{
    [Table("Menus")]
    public class Menu
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }
        public int CategoryId { get; set; }

        // One-to-one relation with Category
        public Category Category { get; set; }

        // One-to-many relation with Reservation
        //public ICollection<Reservation> Reservations { get; set; }
        public List<Reservation> Reservations { get; set; } = [];

    }
}
