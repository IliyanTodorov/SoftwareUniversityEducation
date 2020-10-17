namespace SharedTrip.Models
{
    using SIS.MvcFramework;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class User : IdentityUser<string>
    {
        public User()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Trips = new HashSet<UserTrip>();
        }

        //public string Id { get; set; }

        //[Required]
        //[MinLength(5)]
        //[MaxLength(20)]
        //public string Username { get; set; }

        //[Required]
        //public string Email { get; set; }

        //[Required]
        //[MinLength(6)]
        //[MaxLength(20)]
        //public string Password { get; set; }

        public ICollection<UserTrip> Trips { get; set; }

        //public IdentityRole Role { get; set; }
    }
}
