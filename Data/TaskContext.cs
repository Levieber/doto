using doto.Models;
using Microsoft.EntityFrameworkCore;

namespace doto.Data;

public class TaskContext : DbContext
{
    public TaskContext(DbContextOptions<TaskContext> options) : base(options) { }

    public DbSet<TaskModel> Tasks { get; set; }
}
