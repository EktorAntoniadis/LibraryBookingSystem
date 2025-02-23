﻿using LibraryBookingSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryBookingSystem.Repositories.Implementations
{
    public class UserRepository : IUserRepository
    {
        private LibraryManagementDbContext _context;
        public UserRepository(LibraryManagementDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public void Add(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var user = GetById(id);
            _context.Users.Remove(user);
            _context.SaveChanges();
        }

        public IEnumerable<User> GetAll()
        {
            var user = _context.Users.Include(x=>x.Role).ToList();
            return user;
        }

        public User? GetByEmailOrUsername(User user)
        {
            var existingUser = _context.Users.FirstOrDefault(x => x.Username == user.Username || x.Email == user.Email);
            return existingUser;
        }

        public User? GetById(int id)
        {
            var user = _context.Users
                .Include(x=>x.Role)
                .ThenInclude(x=>x.Permissions)
                .FirstOrDefault(x=>x.UserId == id);
            return user;
        }

        public User? GetByUserName(string userName)
        {
            var user = _context.Users
                .Include(x=>x.Role)
                .Where(x => x.Username == userName).FirstOrDefault();
            return user;
        }

        public void Update(User user)
        {
            _context.Users.Update(user);
            _context.SaveChanges();
        }
    }
}
