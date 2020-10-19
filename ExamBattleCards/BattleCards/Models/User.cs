namespace BattleCards.Models
{
    using SIS.MvcFramework;
    using System;
    using System.Collections.Generic;

    public class User : IdentityUser<string>
    {
        public User()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Role = IdentityRole.User;
            this.Cards = new List<UserCard>();
        }

        public virtual IList<UserCard> Cards { get; set; }
    }
}
