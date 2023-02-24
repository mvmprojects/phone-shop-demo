using Phoneshop.Domain.Interfaces;
using System;
using System.ComponentModel.DataAnnotations;

namespace Phoneshop.Domain.Models
{
    public class Log : IEntity
    {
        public int Id { get; set; }
        [MaxLength(255)]
        public string Level { get; set; }
        public string Message { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
