using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IT4483Image.DTO
{
    public class RecordDTO
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int Type { get; set; }
        public string Link { get; set; }
        public float Longitude { get; set; }
        public float Latitude { get; set; }

    }
}
