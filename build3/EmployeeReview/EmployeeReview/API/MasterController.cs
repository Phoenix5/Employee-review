using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace EmployeeReview.API
{
    public class MasterController : ApiController
    {

        private EmployeeReviewModel db = new EmployeeReviewModel();
        // GET api/values
        public IEnumerable<Employee> Get()
        {
            var list = db.Employees.ToList();

            return list;
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
