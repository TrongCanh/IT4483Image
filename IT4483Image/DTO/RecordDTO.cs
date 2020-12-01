using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IT4483Image.DTO
{
    public class RecordDTO
    {
        public string Title { get; set; }
        public int? Type { get; set; }
        public float? Longitude { get; set; }
        public float? Latitude { get; set; }
        public DateTime? start { get; set; }
        public DateTime? end { get; set; }
        public int? Subjects { get; set; }
        public int? Location { get; set; }


    }
}
