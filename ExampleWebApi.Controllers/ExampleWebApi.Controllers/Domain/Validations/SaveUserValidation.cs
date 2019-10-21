using System;
using System.ComponentModel.DataAnnotations;

namespace ExampleWebApi.Core.Domain.Models.Validations
{
    public class SaveUserValidation
    {
        [Required]
        [MaxLength(200)]
        public string Name { get; set; }

        [Required]
        public DateTime? BirthDate { get; set; }
    }
}
