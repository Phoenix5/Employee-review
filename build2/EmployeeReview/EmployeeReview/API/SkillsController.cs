﻿using System;
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
    public class SkillsController : ApiController
    {
        private EmployeeReviewModel db = new EmployeeReviewModel();

        // GET: api/Skills
        public IQueryable<Skill> GetSkills()
        {
            return db.Skills;
        }

        // GET: api/Skills/5
        [ResponseType(typeof(Skill))]
        public IEnumerable<Skill> GetSkill(int id)
        {
           IList<Skill> skill = db.Skills.ToList();
          //  if (skill == null)
            //{
              //  return NotFound();
           // }

            return skill.Where(x=>x.SkillTypeID==id);
        }

        // PUT: api/Skills/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutSkill(int id, Skill skill)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != skill.SkillsID)
            {
                return BadRequest();
            }

            db.Entry(skill).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SkillExists(id))
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

        // POST: api/Skills
        [ResponseType(typeof(Skill))]
        public IHttpActionResult PostSkill(Skill skill)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Skills.Add(skill);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = skill.SkillsID }, skill);
        }

        // DELETE: api/Skills/5
        [ResponseType(typeof(Skill))]
        public string DeleteSkill(int id)
        {
            Skill skill = db.Skills.Find(id);
            if (skill == null)
            {
              //  return NotFound();
            }

          //  db.Skills.Remove(skill);
          //  db.SaveChanges();

            return skill.SkillsName;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SkillExists(int id)
        {
            return db.Skills.Count(e => e.SkillsID == id) > 0;
        }
    }
}