using System;
using System.Collections.Generic;
using System.Text;

namespace Cineplex.Domain.Domain
{
    public class ActhorInMovies : BaseEntity
    {
        public Guid ActhorID { get; set; }
        public Acthor Achtor { get; set; }
        public Guid MovieID { get; set; }
        public Movie Movie { get; set; }
    }
}
