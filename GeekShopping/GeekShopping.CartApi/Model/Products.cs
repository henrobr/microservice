using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GeekShopping.CartApi.Model
{
    public class Products
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long Id { get; set; }
        [Required]
        [StringLength(150)]
        public string Name { get; set; }
        [Required]
        [Range(1, (double)decimal.MaxValue)]
        public decimal Price { get; set; }
        [StringLength(500)]
        public string Description { get; set; }
        [StringLength(50)]
        public string CategoryName { get; set; }
        [StringLength(300)]
        public string ImageUrl { get; set; }

    }
}
