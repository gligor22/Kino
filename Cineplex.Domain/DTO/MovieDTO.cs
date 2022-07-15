using Cineplex.Domain.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Cineplex.Domain.DTO
{
    public class MovieDTO
    {
        public Guid id { get; set; }
        public int Duration { get; set; }
        public string Title { get; set; }

        [EnumDataType(typeof(MovieType))]
        public MovieType movieType { get; set; }
        public List<Acthor> Acthors { get; set; }
    }
}
