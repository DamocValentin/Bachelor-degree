using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace Data.Core.Domain
{
    public class User : IdentityUser<Guid>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public ICollection<Rating> Ratings { get; set; }
        public ICollection<Review> Reviews { get; set; }
        public ICollection<UserActivity> UserActivities { get; set; }

        public static User Create(string firstName, string lastName, string username, string email, string password, string phoneNumber)
        {
            var instance = new User { Id = Guid.NewGuid() };
            instance.Update(firstName, lastName, username, email, password, phoneNumber);
            return instance;
        }

        public void Update(string firstName, string lastName, string username, string email, string passwordHash, string phoneNumber)
        {
            FirstName = firstName;
            LastName = lastName;
            UserName = username;
            Email = email;
            PhoneNumber = phoneNumber;
            PasswordHash = passwordHash;
        }
    }
}
