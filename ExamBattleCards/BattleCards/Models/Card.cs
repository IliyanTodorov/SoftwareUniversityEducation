﻿namespace BattleCards.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Card
    {
        public Card()
        {
            this.Users = new List<UserCard>();
        }

        public int Id { get; set; }

        [Required]
        [MinLength(5)]
        [MaxLength(15)]
        public string Name { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        [Required]
        public string Keyword { get; set; }

        public int Attack 
        {
            get => this.Attack;
            set
            {
                if (value < 0)
                {
                    throw new Exception("Attack cannot be less than 0");
                }
            } 
        }

        public int Health 
        {
            get => this.Health;
            set
            {
                if (value < 0)
                {
                    throw new Exception("Health cannot be less than 0");
                }
            }
        }

        [Required]
        [MaxLength(200)]
        public string Description { get; set; }

        public IList<UserCard> Users { get; set; } // TODO: Check for not consistent type
    }
}