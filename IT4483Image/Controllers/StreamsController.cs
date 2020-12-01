using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using IT4483Image.Models;
using IT4483Image.DTO;

namespace IT4483Image.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StreamsController : ControllerBase
    {
        private readonly RecordContext _context;

        public StreamsController(RecordContext context)
        {
            _context = context;
        }

        // GET: api/Records/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Record>> GetRecord(long id)
        {
            var record = await _context.Records.FindAsync(id);

            if (record == null)
            {
                return NotFound();
            }

            return record;
        }

        [HttpPost]
        [Route("search-stream-video")]
        public async Task<ActionResult<ResponseDTO>> searchRecords(RecordDTO recordDTO, int skip, int take)
        {
            return new ResponseDTO("Thành công", 200, await _context.Records.Where(s => (s.Type == 2)
                                && (recordDTO.Title == null || s.Title.Contains(recordDTO.Title))
                                && (recordDTO.Location == null || s.Location == recordDTO.Location)
                                && (recordDTO.Subjects == null || s.Type == recordDTO.Subjects)
                                && (recordDTO.start == null || s.CreatedAt >= recordDTO.start)
                                && (recordDTO.end == null || s.CreatedAt <= recordDTO.end)
                                ).Skip(skip).Take(take).ToArrayAsync());
        }

        private bool RecordExists(long id)
        {
            return _context.Records.Any(e => e.Id == id);
        }
    }
}
