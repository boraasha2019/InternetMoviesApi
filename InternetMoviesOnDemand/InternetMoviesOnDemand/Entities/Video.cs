using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InternetMoviesOnDemand.Entities
{
    public class Video
    {
        public int Id { get; set; }

        public int CategoryId { get; set; }

        public string VideoName { get; set; }

        public string VideoDescription { get; set; }

        public int VideoSize { get; set; }

        public string VideoPath { get; set; }

    }
}
