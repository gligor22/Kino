using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Cineplex.Domain.Domain
{
    public class Acthor : BaseEntity
    {
        [Required]
        public String first_name { get; set; }
        [Required]
        public String last_name { get; set; }
        public ICollection<ActhorInMovies> Movies { get; set; }

    }
}
