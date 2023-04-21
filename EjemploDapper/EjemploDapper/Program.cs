using System;
using Dapper;
using MySql.Data.MySqlClient;

internal class Program
{
    private static void Main(string[] args)
    {
        string connectionString = @"Server=localhost; Database=efexample;Uid=root;";
        using (var db = new MySqlConnection(connectionString))
        {
            var sqlInsert = "insert into beers (Name) values(@name)";
            var resultInsert = db.Execute(sqlInsert, new
            {
                name = "Ismael"
            });

            var sql = "select Name from beers";
            var result = db.Query<Beer>(sql);

            foreach (var item in result) 
            {
                Console.WriteLine(item.Name);
            }

        }


        
    }
}

public class Beer
{
    public int Id { get; set; }
    public string Name { get; set; }
}