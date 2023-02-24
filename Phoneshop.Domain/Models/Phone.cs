using Phoneshop.Domain.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace Phoneshop.Domain.Models
{
    public class Phone : IEntity
    {
        // [Key] - data annotation not needed
        public int Id { get; set; }
        // foreign key property BrandId
        public int BrandId { get; set; }
        // reference navigation property Brand
        public Brand Brand { get; set; }
        [Required]
        [MaxLength(255)]
        public string Type { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }

        /// <summary>
        /// Creates a "full name" in the form of Brand - Type.
        /// </summary>
        /// <returns>A string composed of the brand and type, 
        /// or of type alone if the brand property is null.</returns>
        public override string ToString()
        {
            return $"{((Brand == null) ? string.Empty : Brand.Name)} - {Type}";
        }
    }
}
