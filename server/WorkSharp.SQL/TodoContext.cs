using Microsoft.EntityFrameworkCore;
using WorkSharp.SQL.Models;

namespace WorkSharp.SQL;

public class TodoContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlServer("Server=localhost,1433; Database=SeeSharp;User=sa; Password=theRealSlimShady1; TrustServerCertificate=True;");

    public DbSet<Todo> Todos { get; set; }
    public DbSet<User> Users { get; set; }
}



