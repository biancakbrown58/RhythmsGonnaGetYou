using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace RhythmsGonnaGetYou
{
    class RhythmsGonnaGetYouContext : DbContext
    {
        public DbSet<Band> Bands { get; set; }
        public DbSet<Album> Albums { get; set; }
        public DbSet<Song> Songs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // var loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());
            // optionsBuilder.UseLoggerFactory(loggerFactory);

            optionsBuilder.UseNpgsql("server=localhost;database=RhythmsGonnaGetYou");
        }
    }
    class Band
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string CountryOfOrigin { get; set; }
        public int NumberOfMembers { get; set; }
        public string Website { get; set; }
        public string Style { get; set; }
        public bool IsSigned { get; set; }
        public string ContactPhoneNumber { get; set; }
        public List<Album> Albums { get; set; }
    }

    class Album
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public bool IsExplicit { get; set; }
        public DateTime ReleaseDate { get; set; }
        public List<Song> Songs { get; set; }
        public int BandId { get; set; }
        public Band Band { get; set; }
    }

    class Song
    {
        public int Id { get; set; }
        public int TrackNumber { get; set; }
        public string Title { get; set; }
        public decimal Duration { get; set; }
        public int AlbumId { get; set; }
        public Album Album { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var context = new RhythmsGonnaGetYouContext();
            var bandCollection = context.Albums.Include(album => album.Songs).Include(album => album.Band);

            var bandCount = context.Bands.Count();
            Console.WriteLine($"{bandCount}");

            foreach (var album in bandCollection)
            {
                Console.WriteLine($"{album.Title} - {album.Band.Name}");
            }
        }
    }
}
