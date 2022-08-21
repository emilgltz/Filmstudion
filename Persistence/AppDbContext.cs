using Microsoft.EntityFrameworkCore;
using f1lmstudion.Domain.Models;
namespace Filmstudion.Persistence
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<FilmCopy> FilmCopies { get; set; }
        public DbSet<FilmStudio> FilmStudios { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<User>().HasData(new User
            {
                FilmStudioId = 1,
                Email = "johanpersson@sff.se",
                IsAdmin = false,
                Id = 1,
                Password = "!Test123"
            });

            modelBuilder.Entity<User>().HasData(new User
            {
                FilmStudioId = 2,
                Email = "sorenandersson@sff.se",
                IsAdmin = false,
                Id = 2,
                Password = "!Test123"
            });

            modelBuilder.Entity<User>().HasData(new User
            {
                FilmStudioId = 2,
                Email = "kallesvensson@sff.se",
                IsAdmin = false,
                Id = 3,
                Password = "!Test123"
            });



            modelBuilder.Entity<Movie>().HasData(new Movie
            {
                MovieId = 1,
                Title = "Gudfadern",
                Director = "Fracis Ford Coppola",
                Country = "USA/Italien",
                ReleaseYear = 1972,
                AmountOfCopies = 4
            });

            modelBuilder.Entity<Movie>().HasData(new Movie
            {
                MovieId = 2,
                Title = "Avatar",
                Director = "James Cameron",
                Country = "USA",
                ReleaseYear = 2010,
                AmountOfCopies = 5
            });

            modelBuilder.Entity<Movie>().HasData(new Movie
            {
                MovieId = 3,
                Title = "Den Gröna Milen",
                Director = "Frank Darabont",
                Country = "USA",
                ReleaseYear = 1999,
                AmountOfCopies = 3
            });

            modelBuilder.Entity<Movie>().HasData(new Movie
            {
                MovieId = 4,
                Title = "Pulp Fiction",
                Director = "Quentin Tarantion",
                Country = "USA",
                ReleaseYear = 1994,
                AmountOfCopies = 1
            });


            modelBuilder.Entity<FilmCopy>().HasData(new FilmCopy
            {
                FilmCopyId = 1.1,
                MovieId = 1,
                IsRented = false,
                FilmStudioId = 0
            });

            modelBuilder.Entity<FilmCopy>().HasData(new FilmCopy
            {
                FilmCopyId = 1.2,
                MovieId = 1,
                IsRented = false,
                FilmStudioId = 0
            });

            modelBuilder.Entity<FilmCopy>().HasData(new FilmCopy
            {
                FilmCopyId = 1.3,
                MovieId = 1,
                IsRented = false,
                FilmStudioId = 0
            });
            modelBuilder.Entity<FilmCopy>().HasData(new FilmCopy
            {
                FilmCopyId = 1.4,
                MovieId = 1,
                IsRented = false,
                FilmStudioId = 0
            });
            modelBuilder.Entity<FilmCopy>().HasData(new FilmCopy
            {
                FilmCopyId = 2.1,
                MovieId = 2,
                IsRented = false,
                FilmStudioId = 0
            });
            modelBuilder.Entity<FilmCopy>().HasData(new FilmCopy
            {
                FilmCopyId = 2.2,
                MovieId = 2,
                IsRented = false,
                FilmStudioId = 0
            });

            modelBuilder.Entity<FilmCopy>().HasData(new FilmCopy
            {
                FilmCopyId = 2.3,
                MovieId = 2,
                IsRented = false,
                FilmStudioId = 0
            });
            modelBuilder.Entity<FilmCopy>().HasData(new FilmCopy
            {
                FilmCopyId = 2.4,
                MovieId = 2,
                IsRented = false,
                FilmStudioId = 0
            });
            modelBuilder.Entity<FilmCopy>().HasData(new FilmCopy
            {
                FilmCopyId = 2.5,
                MovieId = 2,
                IsRented = false,
                FilmStudioId = 0
            });
            modelBuilder.Entity<FilmCopy>().HasData(new FilmCopy
            {
                FilmCopyId = 3.1,
                MovieId = 3,
                IsRented = false,
                FilmStudioId = 0
            });
            modelBuilder.Entity<FilmCopy>().HasData(new FilmCopy
            {
                FilmCopyId = 3.2,
                MovieId = 3,
                IsRented = false,
                FilmStudioId = 0
            });
            modelBuilder.Entity<FilmCopy>().HasData(new FilmCopy
            {
                FilmCopyId = 3.3,
                MovieId = 3,
                IsRented = true,
                FilmStudioId = 1
            });
            modelBuilder.Entity<FilmCopy>().HasData(new FilmCopy
            {
                FilmCopyId = 4.1,
                MovieId = 4,
                IsRented = false,
                FilmStudioId = 0
            });

            modelBuilder.Entity<FilmStudio>().HasData(new FilmStudio
            {
                FilmStudioId = 1,
                FilmStudioName = "Facklan",
                City = "Åkersberga",
                PresidentName = "Johan persson",
                Email = "johanpersson@sff.se"
            });

            modelBuilder.Entity<FilmStudio>().HasData(new FilmStudio
            {
                FilmStudioId = 2,
                FilmStudioName = "Biohuset",
                City = "Mariefred",
                PresidentName = "Sören Andersson",
                Email = "sorenandersson@sff.se"
            });

            modelBuilder.Entity<FilmStudio>().HasData(new FilmStudio
            {
                FilmStudioId = 3,
                FilmStudioName = "Royal",
                City = "Norrtälje",
                PresidentName = "Kalle Svensson",
                Email = "kallesvensson@sff.se"
            });








        }
    }
}
