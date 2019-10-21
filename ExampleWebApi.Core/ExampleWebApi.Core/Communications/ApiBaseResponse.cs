using System.Collections.Generic;

namespace ExampleWebApi.Core.Communications
{
    /// <summary>
    /// Standard response for webApi methods
    /// </summary>
    public class ApiBaseResponse
    {
        private readonly IList<string> _messages;
        public bool Success { get; protected set; }
        public IList<string> Messages { get { return _messages; }  }

        public ApiBaseResponse(bool success, IList<string> messages)
        {
            Success = success;
            _messages = messages;
        }
    }
}
