using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Cineplex.Domain.Domain
{
    public class Movie : BaseEntity
    {
        public ICollection<ActhorInMovies> Acthors { get; set; }
        [Required]
        public int Duration { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        [EnumDataType(typeof(MovieType))]
        public MovieType movieType { get; set; }

        public Ticket Ticket { get; set; }
    }
}
