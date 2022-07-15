using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using Cineplex.Domain.Domain;

namespace Cineplex.Domain.Identity
{
    public class CineplexApplicationUser : IdentityUser
    {
        [Required]
        public String first_name { get; set; }
        [Required]
        public String last_name { get; set; }
        [Required]
        public String address { get; set; }

        public virtual ShoppingCart ShoppingCart { get; set; }
        public virtual Order Order { get; set; }   
    }
}
