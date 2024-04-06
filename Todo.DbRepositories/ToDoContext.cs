 using Microsoft.EntityFrameworkCore;
 using TodoList.Domain;

 namespace Todolist.Models
{
    public class ToDoContext : DbContext
    {
        public ToDoContext(DbContextOptions<ToDoContext> options) : base(options) { }

        public DbSet<ToDo> ToDoS { get; set; } = null!;
      

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
          
        }
    }
}