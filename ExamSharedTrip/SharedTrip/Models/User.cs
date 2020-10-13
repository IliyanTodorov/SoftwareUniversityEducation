namespace SharedTrip.Models
{
    using SIS.MvcFramework;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class User
    {
        public string Id { get; set; }

        [Required]
        [MinLength(5)]
        [MaxLength(20)]
        public string Username { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        [MinLength(6)]
        [MaxLength(20)]
        public string Password { get; set; }

        public IEnumerable<UserTrip> UserTrips { get; set; }

        [Column(TypeName = "bigint")]
        public IdentityRole Role { get; set; }
    }
}
