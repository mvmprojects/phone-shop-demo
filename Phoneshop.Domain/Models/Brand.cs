using Phoneshop.Domain.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace Phoneshop.Domain.Models
{
    public class Brand : IEntity
    {
        // [Key] - data annotation not needed
        public int Id { get; set; }
        [Required]
        [MaxLength(255)]
        public string Name { get; set; }
        // collection navigation property - not actually needed by either EF or the app
        //public List<Phone> Phones { get; set; }
    }
}
