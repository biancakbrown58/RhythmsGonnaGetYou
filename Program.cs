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

    public class MenuOptions
    {
        // string name string countryOfOrigin, int numberOfMembers, string website, string style, bool isSigned, string contactName, string contactPhoneNumber)
        // public void AddBand(string name, string countryOfOrigin, int numberOfMembers, string website, string style, bool isSigned, string contactName, string contactPhoneNumber)
        // {
        // var context = new RhythmsGonnaGetYouContext();

        // {
        //     Name = name,
        //     CountryOfOrigin = countryOfOrigin,
        //     NumberOfMembers = numberOfMembers,
        //     Website = website,
        //     Style = style,
        //     IsSigned = isSigned,
        //     ContactName = contactName,
        //     ContactPhoneNumber = contactPhoneNumber

        // };
        //     context.Bands.Add(newBand);
        //     context.SaveChanges();
        // }

        // private List<Band> bands = new List<Band>();
        // public void AddBand(Band newBand)
        // {
        //   var context = new RhythmsGonnaGetYouContext();
        //     context.Bands.Add(newBand);

        // }
    }
    class Band
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string CountryOfOrigin { get; set; }
        public int NumberOfMembers { get; set; }
        public string Website { get; set; }
        public string Style { get; set; }
        public string IsSigned { get; set; }
        public string ContactName { get; set; }
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
        // public List<Band> Bands { get; set; }
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
            static string AskForString(string prompt)
            {
                Console.Write(prompt);
                var userInput = Console.ReadLine();
                return userInput;
            }
            static int AskForInteger(string prompt)
            {
                Console.Write(prompt);
                int userInput;
                var intUserInput = Int32.TryParse(Console.ReadLine(), out userInput);

                if (intUserInput)
                {
                    return userInput;
                }
                else
                {
                    Console.WriteLine("Invalid");
                    return 0;
                }
            }
            var menuOptions = new MenuOptions();
            var context = new RhythmsGonnaGetYouContext();
            var bandCollection = context.Albums.Include(album => album.Songs).Include(album => album.Band);

            var isRunning = true;
            // newBand = new Band();
            // var newBand = new Band();
            while (isRunning)
            {
                Console.WriteLine("");
                Console.WriteLine("-----------------------");
                Console.WriteLine("-----------------------");
                Console.WriteLine("Welcome to Band Manager");
                Console.WriteLine("-----------------------");
                Console.WriteLine("-----------------------");
                Console.WriteLine("");
                Console.Write("What would you like to do (A)dd - (V)iew - (U)pdate ");

                var choice = Console.ReadLine().ToUpper();
                if (choice == "A")
                {
                    var band = new Band();
                    Console.WriteLine("Would you like to add a:");
                    Console.Write("(B)and - (A)lbum - (S)ong: ");
                    var userInput = Console.ReadLine().ToUpper();
                    // var newBand = new Band();
                    if (userInput == "B")
                    {

                        Console.WriteLine("Band adding");


                        band.Name = AskForString("Band name: ");
                        band.CountryOfOrigin = AskForString("From: ");
                        band.NumberOfMembers = AskForInteger("How many members: ");
                        band.Website = AskForString("Website: ");
                        band.Style = AskForString("Music Genre: ");
                        // band.IsSigned = YesOrNo();
                        // Console.WriteLine("Signed: ");
                        band.IsSigned = AskForString("Signed: ");

                        band.ContactName = AskForString("Contact Name: ");
                        band.ContactPhoneNumber = AskForString("Contact Phone Number: ");

                        // context.AddBand(band);
                        context.Bands.Add(band);
                        context.SaveChanges();
                    }
                    else
                    {
                        Console.WriteLine("No");
                    }
                }


                else if (choice == "V")
                {
                    var viewBands = context.Bands.OrderBy(b => b.Name);
                    Console.WriteLine("View all Bands");

                    foreach (var band in viewBands)
                    {
                        Console.WriteLine($"{band.Name}");
                    }

                }

                else
                {
                    isRunning = false;
                }
            }
        }
    }
}



