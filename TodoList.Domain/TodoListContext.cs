using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TodoList.Domain.Entities;

namespace TodoList.Domain
{
    public class TodoListContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<TaskApp> Tasks { get; set; }

        public TodoListContext(DbContextOptions options) : base(options)
        {
        }
    }
}
