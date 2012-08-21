using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace OverAchiever.Services.Controllers
{
    public class FakeGoalValue : ApiController
    {
        public IEnumerable<int> Get()
        {
            return new[] {20};
        }

        public int Get(int id)
        {
            return id;
        }

        public void Post(string value)
        {
        }

        public void Put(int id, string value)
        {
        }

        public void Delete(int id)
        {
        }
    }
}