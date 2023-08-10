using Microsoft.EntityFrameworkCore;
using ToDo.Host.Persistence.Entities;

namespace ToDo.Host.Persistence
{
    public class ToDoDbContext : DbContext
    {
        public ToDoDbContext(DbContextOptions<ToDoDbContext> options) : base(options)
        {
        }

        public DbSet<TaskEntity> Tasks { get; set; }
        public DbSet<CategoryEntity> Categories { get; set; }
    }
}
