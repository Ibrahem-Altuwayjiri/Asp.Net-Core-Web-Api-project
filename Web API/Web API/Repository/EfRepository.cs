using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using Web_API.Models;
using static System.Net.Mime.MediaTypeNames;

namespace Web_API.Repository
{
    public class EfRepository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<T> _entityDbSet; // Determine which the Table use

        public EfRepository(ApplicationDbContext context)
        {
            _context = context;
            _entityDbSet = context.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAll(int id = 0)
        {

            if (typeof(T) == typeof(Movie))
            {
                var data = await _entityDbSet
                    .Where(m => EF.Property<int>(m, "GenreId") == id || id == 0)
                    .Include("Genre").ToListAsync();

                return data;
            }

            //.AsNoTracking()
            return await _entityDbSet.ToListAsync();
        }
        public async Task<T> GetById(int id)
        {
            if (typeof(T) == typeof(Movie))
            {
                return await _entityDbSet.Include("Genre").SingleOrDefaultAsync(m => EF.Property<int>(m, "Id") == id);
            }
                return await _entityDbSet.FindAsync(id);
        }
        public async Task<T> Add(T entity)
        {
            await _entityDbSet.AddAsync(entity);
            _context.SaveChanges();
            return entity;
        }
        public T Update(T entity)
        {
            _entityDbSet.Update(entity);
            _context.SaveChanges();
            return entity;
        }
        public T Delete(T entity)
        {

            _entityDbSet.Remove(entity);
            _context.SaveChanges();
            return entity;
        }
        public Task<bool> IsvalidGenre(int id)
        {
            return _context.Genres.AnyAsync(g => g.Id == id);
        }


        public void seed()
        {
            if (!_context.Genres.Any())
            {
                _context.Genres.AddRange(
                new Genre
                {
                    Name = "Drama"
                },
                new Genre
                {
                    Name = "Fantasy"
                },
                new Genre
                {
                    Name = "Documentary"
                },
                new Genre
                {
                    Name = "Action"
                },
                new Genre
                {
                    Name = "Comedy"
                });

                _context.Movies.AddRange(
               new Movie
               {
                   Title = "Hole in the Head, A",
                   Year = 1992,
                   Rate = 9.5,
                   GenreId = 2
               },
               new Movie
               {
                   Title = "Can I Do It 'Till I Need Glasses?",
                   Year = 2009,
                   Rate = 6.2,
                   GenreId = 4
               },
               new Movie
               {
                   Title = "King of Hearts",
                   Year = 2002,
                   Rate = 7.9,
                   GenreId = 5
               },
               new Movie
               {
                   Title = "King of Hearts2",
                   Year = 1094,
                   Rate = 5.9,
                   GenreId = 1
               },
               new Movie
               {
                   Title = "King of Hearts3",
                   Year = 1099,
                   Rate = 9.1,
                   GenreId = 2
               },
               new Movie
               {
                   Title = "King of Hearts4",
                   Year = 2008,
                   Rate = 7.4,
                   GenreId = 5
               },
               new Movie
               {
                   Title = "King of Hearts5",
                   Year = 2009,
                   Rate = 6.5,
                   GenreId = 1
               },
               new Movie
               {
                   Title = "King of Hearts6",
                   Year = 2013,
                   Rate = 6.4,
                   GenreId = 2
               },
               new Movie
               {
                   Title = "King of Hearts7",
                   Year = 2016,
                   Rate = 3.9,
                   GenreId = 5
               },
               new Movie
               {
                   Title = "King of Hearts8",
                   Year = 2007,
                   Rate = 7.7,
                   GenreId = 2
               });

                _context.SaveChanges();
            }


        }
    }
}
