using Dapper;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using WebApiDapper.Models;

namespace WebApiDapper.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ThingsController : ControllerBase
    {

        private string connectionString = @"server=localhost; Database=cruddapper; Uid=root";

        [HttpGet]
        public IActionResult Get()
        {
            IEnumerable<Thing> lst = null;
            using(var db = new MySqlConnection(connectionString))
            {
                var sql = "select id, name, description from thing";
                
                lst = db.Query<Thing>(sql);

            }

            return Ok(lst);

            
        }

        [HttpPost]
        public IActionResult Insert(Thing model)
        {
            int result = 0;

            using (var db = new MySqlConnection(connectionString))
            {
                var sql = "insert into thing(name, description) values (@name, @description)";
                result = db.Execute(sql,model);

            }

            return Ok(result);

        }

        [HttpPut]
        public IActionResult Update(Thing model)
        {
            int result = 0;
            using (var db = new MySqlConnection(connectionString))
            {
                var sql = "UPDATE  thing set name = @name, description = @description where id = @id";
                result = db.Execute(sql, model);

            }

            return Ok(result);
        }

        [HttpDelete]
        public IActionResult Delete(Thing model)
        {
            int result = 0;
            using (var db = new MySqlConnection(connectionString))
            {
                var sql = "DELETE from thing where id = @id";
                result = db.Execute(sql, model);

            }

            return Ok(result);
        }


    }
}
