using Hexagonal_Exercise.catalog.core.domain.commandBus;
using System.ComponentModel.DataAnnotations;

namespace Hexagonal_Exercise.entry_point.catalog.v1.model
{
    public class RenameProductModel
    {
        [Required]
        public string Name { get; set; }
    }
}