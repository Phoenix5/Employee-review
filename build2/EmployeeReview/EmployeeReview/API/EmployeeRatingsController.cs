using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using EmployeeReview;

namespace EmployeeReview.API
{
    public class EmployeeRatingsController : ApiController
    {
        private EmployeeReviewModel db = new EmployeeReviewModel();

        // GET: api/EmployeeRatings
        public IEnumerable<EmployeeRating> GetEmployeeRatings()
        {
            return db.EmployeeRatings.ToList();
        }

        // GET: api/EmployeeRatings/5
        [ResponseType(typeof(EmployeeRating))]
        public IEnumerable<EmployeeRating> GetEmployeeRating(int id)
        {
            IList<EmployeeRating> employeeRating = db.EmployeeRatings.ToList();
           // if (employeeRating == null)
           // {
           //     return 0;
           // }

            return employeeRating.Where(x=>x.EmployeeID==id);
        }

        // PUT: api/EmployeeRatings/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutEmployeeRating(int id, EmployeeRating employeeRating)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != employeeRating.EmployeeRatingsID)
            {
                return BadRequest();
            }

            db.Entry(employeeRating).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeRatingExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/EmployeeRatings
        [ResponseType(typeof(EmployeeRating))]
        public IHttpActionResult PostEmployeeRating(EmployeeRating employeeRating)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.EmployeeRatings.Add(employeeRating);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = employeeRating.EmployeeRatingsID }, employeeRating);
        }

        // DELETE: api/EmployeeRatings/5
        [ResponseType(typeof(EmployeeRating))]
        public IHttpActionResult DeleteEmployeeRating(int id)
        {
            EmployeeRating employeeRating = db.EmployeeRatings.Find(id);
            if (employeeRating == null)
            {
                return NotFound();
            }

            db.EmployeeRatings.Remove(employeeRating);
            db.SaveChanges();

            return Ok(employeeRating);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EmployeeRatingExists(int id)
        {
            return db.EmployeeRatings.Count(e => e.EmployeeRatingsID == id) > 0;
        }
    }
}