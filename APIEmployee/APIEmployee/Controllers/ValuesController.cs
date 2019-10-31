using APIEmployee.DataBase;
using APIEmployee.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace APIEmployee.Controllers
{
    public class ValuesController : ApiController
    {
        // GET api/values
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public List<EmployeeDetails> Post([FromBody] SearchCriteria searchCrieteria)
        {
            var dbcontext = new FakeMainModuleContext();
            return dbcontext.employee.ToList().First(); ;
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
