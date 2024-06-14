using System.ComponentModel.DataAnnotations.Schema;

namespace GraphqlProject.Models
{
    [Table("Categories")]
    public class Category
    {    
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public ICollection<Menu> Menus { get; set; }
       /* public int MenuId { get; set; }
        [ForeignKey("MenuId")]
        public Menu Menu { get; set; }
       */
    }
}
