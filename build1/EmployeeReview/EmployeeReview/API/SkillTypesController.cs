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
    public class SkillTypesController : ApiController
    {
        private EmployeeReviewModel db = new EmployeeReviewModel();

        // GET: api/SkillTypes
        public IQueryable<SkillType> GetSkillTypes()
        {
            return db.SkillTypes;
        }

        // GET: api/SkillTypes/5
        [ResponseType(typeof(SkillType))]
        public IHttpActionResult GetSkillType(int id)
        {
            SkillType skillType = db.SkillTypes.Find(id);
            if (skillType == null)
            {
                return NotFound();
            }

            return Ok(skillType);
        }

        // PUT: api/SkillTypes/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutSkillType(int id, SkillType skillType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != skillType.SkillTypeID)
            {
                return BadRequest();
            }

            db.Entry(skillType).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SkillTypeExists(id))
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

        // POST: api/SkillTypes
        [ResponseType(typeof(SkillType))]
        public IHttpActionResult PostSkillType(SkillType skillType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.SkillTypes.Add(skillType);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = skillType.SkillTypeID }, skillType);
        }

        // DELETE: api/SkillTypes/5
        [ResponseType(typeof(SkillType))]
        public IHttpActionResult DeleteSkillType(int id)
        {
            SkillType skillType = db.SkillTypes.Find(id);
            if (skillType == null)
            {
                return NotFound();
            }

            db.SkillTypes.Remove(skillType);
            db.SaveChanges();

            return Ok(skillType);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SkillTypeExists(int id)
        {
            return db.SkillTypes.Count(e => e.SkillTypeID == id) > 0;
        }
    }
}