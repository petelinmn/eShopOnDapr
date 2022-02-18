using BlastAce.Model;
using Dapr.Client;
using Dapr.Actors;
using Dapr.Actors.Client;
using Microsoft.AspNetCore.Mvc;
using Dapr;
//using BlastAce.Actors.Contract;

namespace BlastAceApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PersonController : ControllerBase
    {
        private readonly ILogger<PersonController> _logger;

        public PersonController(ILogger<PersonController> logger)
        {
            _logger = logger;
        }

        /*[HttpPut]
        public async Task<Person> Save([FromBody] Person person) =>
            await ActorProxy.Create<IPersonAccessActor>(new ActorId("SavePerson"), "PersonAccessActor")
                .SavePerson(person);

        [Route("{personId}")]
        [HttpGet]
        public async Task<Person> Get([FromRoute] int personId) =>
            await ActorProxy.Create<IPersonAccessActor>(new ActorId("GetPerson"), "PersonAccessActor")
                .GetPerson(personId);*/

        [Route("all")]
        [HttpGet]
        public async Task<IEnumerable<Person>> Get() =>
            await Task.Run(() => new Person[] {
                new Person { Id = 1, FirstName = "John", LastName = "Doe" }
            });
            /*await ActorProxy.Create<IPersonAccessActor>(new ActorId("GetPersons"), "PersonAccessActor")
                .GetPersons();*/

        /*[Topic("pubsub", "deletePerson")]
        [HttpPost]
        public async ValueTask<bool> DeletePerson(Person person) =>
            await ActorProxy.Create<IPersonAccessActor>(new ActorId("DeletePerson"), "PersonAccessActor")
                .DeletePerson(person.Id);

        [Route("process/{personId}/{property2}")]
        [HttpPost]
        public async ValueTask<bool> Process2Person([FromRoute] int personId, [FromRoute] string property2)
        {
            var dbProxy = ActorProxy.Create<IPersonAccessActor>(new ActorId("GetPerson"), "PersonAccessActor");
            var person = await dbProxy.GetPerson(personId);

            var proxy = ActorProxy.Create<IPersonProcessActor>(new ActorId("ProcessPerson"), "PersonProcessActor");
            person = await proxy.Process2Person(person, property2);

            person = await proxy.Process1Person(person, property2.Length);

            await dbProxy.SavePerson(person);

            return true;
        }

        [Route("increaseProgress/{personId}")]
        [HttpPost]
        public async ValueTask<bool> IncreaseProgress([FromRoute] int personId)
        {
            var dbProxy = ActorProxy.Create<IPersonAccessActor>(new ActorId("GetPerson"), "PersonAccessActor");
            var person = await dbProxy.GetPerson(personId);

            var proxy = ActorProxy.Create<IPersonProcessActor>(new ActorId("ProcessPerson"), "PersonProcessActor");
            person = await proxy.IncreaseProgressForPerson(person);
            await dbProxy.SavePerson(person);

            await proxy.CheckProgress(person);

            return true;
        }*/
    }
}
