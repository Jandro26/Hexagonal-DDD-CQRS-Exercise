using System.ComponentModel.DataAnnotations;

namespace Hexagonal_Exercise.entry_point.catalog.v1.model
{
    public class CreateProductModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

    }
}