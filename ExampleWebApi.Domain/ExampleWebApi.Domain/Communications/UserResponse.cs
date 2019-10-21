using ExampleWebApi.Core.Communications;
using ExampleWebApi.Domain.Models;
using System.Collections.Generic;

namespace ExampleWebApi.Domain.Communications
{
    public class UserResponse : ApiBaseResponse
    {
        public User User { get; private set; }

        private UserResponse(bool success, IList<string> message, User user) : base(success, message)
        {
            User = user;
        }

        /// <summary>
        /// Creates a success response.
        /// </summary>
        /// <param name="user">Saved user.</param>
        /// <returns>Response.</returns>
        public UserResponse(User user) : this(true, new List<string>(), user)
        { }

        /// <summary>
        /// Creates an error response.
        /// </summary>
        /// <param name="message">Error message.</param>
        /// <returns>Response.</returns>
        public UserResponse(IList<string> message) : this(false, message, null)
        { }
    }
}
