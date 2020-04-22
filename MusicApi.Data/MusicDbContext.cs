using Microsoft.EntityFrameworkCore;
using MusicApi.Core.Models;
using MusicApi.Data.Configurations;

namespace MusicApi.Data
{
    public class MusicDbContext : DbContext
    {
        public MusicDbContext(DbContextOptions<MusicDbContext> options)
             : base(options) { }


        public DbSet<Music> Musics { get; set; }
        public DbSet<Artist> Artists { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
                .ApplyConfiguration(new MusicConfiguration());

            builder
                .ApplyConfiguration(new ArtistConfiguration());
        }

    }
}
