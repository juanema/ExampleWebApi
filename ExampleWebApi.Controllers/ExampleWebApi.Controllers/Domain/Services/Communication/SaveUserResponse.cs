using ExampleWebApi.Core.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExampleWebApi.Core.Domain.Services.Communication
{
    public class SaveUserResponse : ApiBaseResponse
    {
        public User User { get; private set; }

        private SaveUserResponse(bool success, IList<string> message, User user) : base(success, message)
        {
            User = user;
        }

        /// <summary>
        /// Creates a success response.
        /// </summary>
        /// <param name="user">Saved user.</param>
        /// <returns>Response.</returns>
        public SaveUserResponse(User user) : this(true, new List<string>(), user)
        { }

        /// <summary>
        /// Creates an error response.
        /// </summary>
        /// <param name="message">Error message.</param>
        /// <returns>Response.</returns>
        public SaveUserResponse(IList<string> message) : this(false, message, null)
        { }
    }
}
