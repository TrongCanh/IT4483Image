using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IT4483Image.DTO
{
    public class ResponseDTO
    {
        public string message { get; set; }
        public int status { get; set; }
        public ResponseDTO(string messageDTO, int statusDTO)
        {
            message = messageDTO;
            status = statusDTO;
        }
    }
}
