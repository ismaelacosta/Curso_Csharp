using ApiVideo2.Models;
using Microsoft.AspNetCore.Mvc;

namespace ApiVideo2.Controllers
{

    [ApiController]
    [Route("client")]
    public class ClientsController : ControllerBase
    {

        [HttpGet]
        [Route("/listall")]
        public dynamic ListClients()
        {

             List<Client> clients = new List<Client>
             {
                 new Client { 
                     Id = 1,
                     Name = "Ismael",
                     Age = 22,
                     Email = "ismael@yopmail"
                 },
                 new Client {
                     Id = 2,
                     Name = "Rafa",
                     Age = 17,
                     Email = "rafa@yopmail"
                 },
             };


            return clients;
        }

        [HttpGet]
        [Route("/id")]
        public dynamic ViewClient(int id)
        {
          Client client =   new Client
            {
                Id = 1,
                Name = "Ismael",
                Age = 22,
                Email = "ismael@yopmail"
            };

            return client;
        }


        [HttpPost]
        [Route("/save")]
        public dynamic SaveClient( Client client)
        {
            client.Id = 3;
            return new
            {
                success = true,
                message = " cliente registrado",
                result = client
            };
        }

        [HttpDelete]
        [Route("/delete")]
        public dynamic DeleteClient(int id)
        {
            string token = Request.Headers.Where(x => x.Key == "Authorization").FirstOrDefault().Value;   // todo viene en esta variable

            if(token == "ismael")
            {
                Console.WriteLine("Acceso Correcto");
            }

            return new
            {
                success = true,
                message = " cliente eliminado",
                result = id
            };
        }
    }
}
