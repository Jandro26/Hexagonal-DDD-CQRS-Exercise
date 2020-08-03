using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Hexagonal_Exercise.entry_point.catalog.v1.model
{
    public class GetProductByIdResultModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public int? Category { get; set; }

        public string Description { get; set; }
    }
}
