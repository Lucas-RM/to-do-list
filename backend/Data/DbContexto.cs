using backend.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace backend.Data
{
    public class DbContexto : DbContext
    {
        public DbContexto(DbContextOptions<DbContexto> options) : base(options)
        {
        }

        public DbSet<Tarefa> Tarefas { get; set; }

    }
}
