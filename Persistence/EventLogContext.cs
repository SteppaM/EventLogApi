using Microsoft.EntityFrameworkCore;
using Persistence.Models;

namespace Persistence
{
    public class EventLogContext : DbContext
    {
        public EventLogContext(DbContextOptions options): base(options) { }
        public DbSet<LogEntry> LogEntries { get; set; }
    }
}