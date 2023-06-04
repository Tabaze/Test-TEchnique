using Microsoft.EntityFrameworkCore;
using testApp.Models;

namespace testApp.Context;

public class TestContext :DbContext
{
    public DbSet<User> users { get; set; }
    public DbSet<Formulaire> formulaires { get; set; }
    public TestContext(DbContextOptions options) : base(options)
    {
    }
    public TestContext()
    {
    }
}
