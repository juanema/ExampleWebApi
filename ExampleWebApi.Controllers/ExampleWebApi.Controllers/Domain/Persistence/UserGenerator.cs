using ExampleWebApi.Core.Domain.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExampleWebApi.Core.Domain.Persistence
{
    public class UserGenerator
    {
        readonly AppDbContext _appDbContext;

        public UserGenerator(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public void GenerateBikiniBottom()
        {
            if (!_appDbContext.Users.Any())
            {
                _appDbContext.Users.Add(new Models.User() { Id = 1, Name = "SpongeBob Square Pants", Birthdate = new System.DateTime(1990, 5, 8) });
                _appDbContext.Users.Add(new Models.User() { Id = 2, Name = "Patrick Star", Birthdate = new System.DateTime(1990, 7, 10) });
                _appDbContext.Users.Add(new Models.User() { Id = 3, Name = "Squidward Q. Tentacles", Birthdate = new System.DateTime(1976, 3, 15) });
                _appDbContext.Users.Add(new Models.User() { Id = 4, Name = "Mr. Krabs", Birthdate = new System.DateTime(1966, 6, 11) });
                _appDbContext.Users.Add(new Models.User() { Id = 5, Name = "Mr. Codfish", Birthdate = new System.DateTime(1985, 12, 31) });
                _appDbContext.Users.Add(new Models.User() { Id = 6, Name = "Larry the Lobster", Birthdate = new System.DateTime(1993, 07, 01) });
                _appDbContext.Users.Add(new Models.User() { Id = 7, Name = "Sandy Cheeks", Birthdate = new System.DateTime(1996, 11, 11) });

                _appDbContext.SaveChanges();
            }
        }
    }
}
