using System;

namespace ExampleWebApi.Core.Extensions
{
    public static class ObjectExtensions
    {
        /// <summary>
        /// Throw ArgumentNullException with the object name if the object is null
        /// </summary>
        /// <param name="notNullable">an object</param>
        public static void ThrowExceptionIfNull(this object notNullable)
        {
            if (notNullable == null)
            {
                throw new ArgumentNullException(nameof(notNullable));
            }
        }
    }
}
