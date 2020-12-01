using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using IT4483Image.Models;
using IT4483Image.DTO;
using System.Reflection;

namespace IT4483Image.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecordsController : ControllerBase
    {
        private readonly RecordContext _context;

        public RecordsController(RecordContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("healthz")]
        public string Healthz()
        {
            return "Active success";
        }

        // GET: api/Records
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Record>>> GetRecords()
        {
            return await _context.Records.ToListAsync();
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
        [Route("search-image-video")]
        public async Task<ActionResult<ResponseDTO>> searchRecords(RecordDTO recordDTO, int skip, int take)
        {

            return new ResponseDTO("Thành công", 200, await _context.Records.Where(s => ( recordDTO.Type==null || s.Type==recordDTO.Type)
                                && (recordDTO.Title == null || s.Title.Contains(recordDTO.Title))
                                && (recordDTO.Location == null || s.Location == recordDTO.Location)
                                && (recordDTO.Subjects == null || s.Type == recordDTO.Subjects)
                                && (recordDTO.start == null || s.CreatedAt >= recordDTO.start)
                                && (recordDTO.end == null || s.CreatedAt <= recordDTO.end)
                                ).Skip(skip).Take(take).ToArrayAsync());
        }


        // PUT: api/Records/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<ActionResult<ResponseDTO>> PutRecord(long id, Record record)
        {

            record.Id = record.Id;

            _context.Entry(record).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RecordExists(id))
                {
                    return new ResponseDTO("Không tồn tại ", 404); 
                }
                else
                {
                    throw;
                }
            }

            return new ResponseDTO("Update thành công",200);
        }

        // POST: api/Records
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Record>> PostRecord(Record record)
        {
            _context.Records.Add(record);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRecord", new { id = record.Id }, record);
        }


        /// <summary>
        /// Sao chép giá trị đối tượng, nếu đối tượng cần sao chép không có trường đó thì bỏ qua
        /// </summary>
        /// <typeparam name="T">object</typeparam>
        /// <param name="sourceObject">Đầu vào bản cần sao chép</param>
        /// <param name="destObject">Đối tượng cần lấy sau khi sao chép</param>
        /// Created by: BVMINH (24/08/2020)
        public static void CopyObject<T>(object sourceObject, ref T destObject)
        {
            if (sourceObject == null || destObject == null)
            {
                return;
            }
            Type sourceType = sourceObject.GetType();
            Type targetType = destObject.GetType();
            foreach (PropertyInfo p in sourceType.GetProperties())
            {
                //  Get the matching property in the destination object
                PropertyInfo targetObj = targetType.GetProperty(p.Name);
                //  If there is none, skip
                if (targetObj == null)
                    continue;
                //set value
                targetObj.SetValue(destObject, p.GetValue(sourceObject, null), null);
            }
        }

        // DELETE: api/Records/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Record>> DeleteRecord(long id)
        {
            var record = await _context.Records.FindAsync(id);
            if (record == null)
            {
                return NotFound();
            }

            _context.Records.Remove(record);
            await _context.SaveChangesAsync();

            return record;
        }

        private bool RecordExists(long id)
        {
            return _context.Records.Any(e => e.Id == id);
        }
    }
}
