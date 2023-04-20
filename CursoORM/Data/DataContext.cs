using Microsoft.EntityFrameworkCore;
using CursoORM.Data.Entities;

namespace CursoORM;
public class DataContext : DbContext
{
    public string DbPath { get; }

    public DataContext()
    {
        var path = "C:/Users/ismael.acosta/Documents/Curso_Csharp/CursoORM/";
        DbPath = System.IO.Path.Join(path, "CursoORM.db");
    }

    public DbSet<Gente> Gente { get; set; }
    public DbSet<cSexo> Sexo { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
       => options.UseSqlite($"Data Source={DbPath}");
}
