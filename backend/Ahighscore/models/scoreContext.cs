using Microsoft.EntityFrameworkCore;

namespace Ahighscore.Models{
    public class scoreContext : DbContext {
        public scoreContext(DbContextOptions<scoreContext> options): base(options){

        }
      public DbSet<scoreItem> scoreItems {get; set; }
      }
    }