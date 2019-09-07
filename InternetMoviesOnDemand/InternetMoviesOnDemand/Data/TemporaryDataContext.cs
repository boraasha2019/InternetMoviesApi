using InternetMoviesOnDemand.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InternetMoviesOnDemand.Data
{
    public static class TemporaryDataContext
    {
        public static IEnumerable<User> _users = new List<User>
        {
            new User { Id = 1, UserName = "fred", Password = "123", Email="Fred@gmail.com", Role = "Administrator"},
            new User { Id = 2, UserName = "alice", Password = "456", Email="Alice1@gmail.com", Role = "Viewer"},
        };


        public static IEnumerable<Category> _category = new List<Category>();
    }
}
