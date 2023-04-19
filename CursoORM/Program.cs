using CursoORM.Data.Entities;
using CursoORM;
using Microsoft.EntityFrameworkCore;

internal class Program
{
    private static void Main(string[] args)
    {
        using var db = new DataContext();


        /* // Create
        Console.WriteLine("Inserting a new blog");
        db.Add(new cSexo
        {
            Nombre = "Masculino"
        });

        db.Add(new cSexo
        {
            Nombre = "Femenino"
        }); */


        //db.SaveChanges();

        // Read
        Console.WriteLine("Querying for a Sex");

        var sexo = db.Sexo
        .OrderBy(n => n.Nombre)
        .First();

        Console.WriteLine(sexo.Nombre);


        /* foreach (var item in sexo)
        {
            Console.WriteLine(item);
        } */


        // Update
/*cSexo csexo = db.Sexo.Where(n => n.Nombre == "Femenino").First();
csexo.Nombre = "Mujer";
Console.WriteLine(csexo.Nombre);
db.Entry(csexo).State = EntityState.Modified;
db.SaveChanges();
     Console.WriteLine(csexo.Nombre);*/

// Delete
Console.WriteLine("Delete the Sexo");
db.Remove(sexo);
db.SaveChanges();


    }
}