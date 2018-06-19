using System;
using Microsoft.EntityFrameworkCore;

namespace LPChallenge.Models
{
    public class LPChallengeContext : DbContext
    {
        public  LPChallengeContext(DbContextOptions<LPChallengeContext> options) : base(options) { }
        public DbSet<Border> Borders {get; set;}
        public DbSet<Segment> Segments {get; set;}

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Border>().HasMany(b => b.segments);
        }
    }
}