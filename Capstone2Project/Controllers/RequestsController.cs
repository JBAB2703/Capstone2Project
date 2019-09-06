using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Capstone2Project.Models;

namespace Capstone2Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequestsController : ControllerBase
    {
        private readonly MyDb _context;

        public RequestsController(MyDb context)
        {
            _context = context;
        }

        // GET: api/Requests
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Request>>> GetRequests()
        {
            return await _context.Requests.ToListAsync();
        }

        // GET: api/GetRequestForReview
        [Route("/api/GetRequestForReview")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Request>>> GetRequestForReview() {
            return await _context  //the database
                           .Requests //Entity
                           .Where( r => r.Status=="Review") //filter for review
                           .ToListAsync(); //return a collection
        }

        // GET: api/Requests/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Request>> GetRequest(int id)
        {
            var request = await _context.Requests.FindAsync(id);

            if (request == null)
            {
                return NotFound();
            }

            return request;
        }

        // GET: api/SetStatusReview/5
        [Route("/api/SetStatusReview/{id}")]
        [HttpGet]
        public async Task<ActionResult<Request>> SetStatusReview(int id) {
            var request = await _context.Requests.FindAsync(id);

            if (request == null) {
                return NotFound();
            }

            request.Status = "Review";
            _context.SaveChanges();

            return Ok();
        }

        // GET: api/SetStatusApproved/5
        [Route("/api/SetStatusApproved/{id}")]
        [HttpGet]
        public async Task<ActionResult<Request>> SetStatusApproved(int id) {
            var request = await _context.Requests.FindAsync(id);

            if (request == null) {
                return NotFound();
            }

            request.Status = "Approved";
            _context.SaveChanges();

            return Ok();
        }

        // GET: api/SetStatusRejected/5
        [Route("/api/SetStatusRejected/{id}")]
        [HttpGet]
        public async Task<ActionResult<Request>> SetStatusRejected(int id) {
            var request = await _context.Requests.FindAsync(id);

            if (request == null) {
                return NotFound();
            }

            request.Status = "Rejected";
            _context.SaveChanges();

            return Ok();
        }

        // PUT: api/Requests/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRequest(int id, Request request)
        {
            if (id != request.Id)
            {
                return BadRequest();
            }

            _context.Entry(request).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RequestExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Requests
        [HttpPost]
        public async Task<ActionResult<Request>> PostRequest(Request request)
        {
            _context.Requests.Add(request);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRequest", new { id = request.Id }, request);
        }

        // DELETE: api/Requests/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Request>> DeleteRequest(int id)
        {
            var request = await _context.Requests.FindAsync(id);
            if (request == null)
            {
                return NotFound();
            }

            _context.Requests.Remove(request);
            await _context.SaveChangesAsync();

            return request;
        }

        private bool RequestExists(int id)
        {
            return _context.Requests.Any(e => e.Id == id);
        }
    }
}
