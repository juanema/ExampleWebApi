using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExampleWebApi.Core.Domain.Models
{
    [Table("t_users")]
    public class User: Entity<int>
    {        
        [Required]
        [MaxLength(200)]
        public string Name { get; set; }
        [Required]
        public DateTime Birthdate { get; set; }

    }
}
