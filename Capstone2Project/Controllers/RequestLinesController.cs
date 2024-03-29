﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Capstone2Project.Models;

namespace Capstone2Project.Controllers
{
    [Route("api/RequestLine")]
    [ApiController]
    public class RequestLinesController : ControllerBase
    {
        private readonly MyDb _context;

        public RequestLinesController(MyDb context)
        {
            _context = context;
        }

        // GET: api/RequestLines
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RequestLine>>> GetRequestLines()
        {
            return await _context.RequestLines.ToListAsync();
        }

        // GET: api/RequestLines/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RequestLine>> GetRequestLine(int id)
        {
            var requestLine = await _context.RequestLines.FindAsync(id);

            if (requestLine == null)
            {
                return NotFound();
            }

            return requestLine;
        }

        // PUT: api/RequestLines/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRequestLine(int id, RequestLine requestLine)
        {
            if (id != requestLine.Id)
            {
                return BadRequest();
            }

            _context.Entry(requestLine).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                //call recalculate method
                var success = RecalculateRequestTotal(id);
                if (!success) { return this.StatusCode(500); }
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RequestLineExists(id))
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

        // POST: api/RequestLines
        [HttpPost]
        public async Task<ActionResult<RequestLine>> PostRequestLine(RequestLine requestLine)
        {
            _context.RequestLines.Add(requestLine);
            await _context.SaveChangesAsync();
            ////call recalculate
            var success = RecalculateRequestTotal(requestLine.Id);
            if (!success) { return this.StatusCode(500); }

            return CreatedAtAction("GetRequestLine", new { id = requestLine.Id }, requestLine);
        }

        // DELETE: api/RequestLines/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<RequestLine>> DeleteRequestLine(int id)
        {
            var requestLine = await _context.RequestLines.FindAsync(id);
            if (requestLine == null)
            {
                return NotFound();
            }

            _context.RequestLines.Remove(requestLine);
            await _context.SaveChangesAsync();
            //call recalculate
            var success = RecalculateRequestTotal(id);
            if (!success) { return this.StatusCode(500); }

            return requestLine;
        }

        private bool RequestLineExists(int id)
        {
            return _context.RequestLines.Any(e => e.Id == id);
        }
        //Recalculate the total in the Request
        private bool RecalculateRequestTotal(int requestId) {
            var request = _context.Requests.SingleOrDefault(r => r.Id == requestId);
            if (request == null) {
                return false;
            }
            request.Total = _context.RequestLines
                .Include(l => l.Product)
                .Where(l => l.RequestId == requestId)
                .Sum(l => l.Quantity * l.Product.Price);

            if (request.Status == "Review")
                request.Status = "Revised";

            _context.SaveChanges();
            return true;
        }
    }
}
