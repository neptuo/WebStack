using Neptuo.WebStack.Routing;
using Neptuo.WebStack.Services;
using Neptuo.WebStack.Services.Behaviors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace TestWebApp.Services
{
    [Route("~/person/john")]
    public class PersonJohnDoeHandler : IGet, IWithOutput<PersonModel>
    {
        public PersonModel Output { get; private set; }

        public Task<bool> ExecuteAsync()
        {
            Output = new PersonModel()
            {
                FirstName = "John",
                LastName = "Doe"
            };
            return Task.FromResult(true);
        }
    }

    public class PersonModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}