using ExampleWebApi.Core;
using ExampleWebApi.Domain.Communications;
using ExampleWebApi.Domain.Models;
using ExampleWebApi.Domain.Models.Validations;
using ExampleWebApi.Domain.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace ExampleWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]    
    public class UserController : ControllerBase
    {
        private readonly IStringLocalizer<UserController> _localizer;
        private readonly IUserService _service;

        public UserController(IUserService service, IStringLocalizer<UserController> localizer)
        {
            Guard.Against<ArgumentNullException>(service == null, $"Parameter {nameof(service)} is null");
            Guard.Against<ArgumentNullException>(localizer == null, $"Parameter {nameof(localizer)} is null");
            _service = service;
            _localizer = localizer;
        }


        /// <summary>
        /// Get all Users
        /// </summary>        
        [HttpGet]
        public async Task<IEnumerable<User>> Get()
        {
            return await _service.ListAsync().ConfigureAwait(false);
        }


        /// <summary>
        /// Get a specific User
        /// </summary>    
        /// <param name="id">User Id</param>
        [HttpGet("{id}", Name = "Get")]
        public async Task<User> Get(int id)
        {
            Domain.Models.User user;

            user = await _service.GetAsync(id).ConfigureAwait(false);
            if (user == null)
            {
                throw new InvalidOperationException(_localizer["Id not found"]);
            }

            return user;
        }

        /// <summary>
        /// Create or update a User
        /// </summary>            
        /// <param name="id">User Id</param>
        /// <param name="value">Validated values</param>
        [HttpPost]
        public async Task<UserResponse> Post(int? id, [FromBody] SaveUserValidation value)
        {
            Guard.Against<ArgumentNullException>(value == null, $"Parameter {nameof(value)} is null");
            Domain.Models.User user;
            UserResponse response;
            

            if (id.HasValue)
            {
                user = await _service.GetAsync(id.Value).ConfigureAwait(false);

                if (user != null)
                {
                    user.Birthdate = value.BirthDate.GetValueOrDefault();
                    user.Name = value.Name;
                    response = await _service.UpdateAsync(user).ConfigureAwait(false);
                }
                else
                {
                    user = new Domain.Models.User() { Id = id.Value, Birthdate = value.BirthDate.GetValueOrDefault(), Name = value.Name };
                    response = await _service.SaveAsync(user).ConfigureAwait(false);
                }
            }
            else
            {
                user = new Domain.Models.User() { Birthdate = value.BirthDate.GetValueOrDefault() , Name = value.Name };
                response = await _service.SaveAsync(user).ConfigureAwait(false);
            }

            return response;
        }

        /// <summary>
        /// Update a specific User
        /// </summary>    
        /// <param name="id">User Id</param>
        /// <param name="value">Validated values</param>
        [HttpPut("{id}")]
        public async Task<UserResponse> Put(int id, [FromBody] SaveUserValidation value)
        {
            Guard.Against<ArgumentNullException>(value == null, $"Parameter {nameof(value)} is null");
            Domain.Models.User user;
            UserResponse response;

            user = await _service.GetAsync(id).ConfigureAwait(false);

            if (user != null)
            {
                user.Birthdate = value.BirthDate.GetValueOrDefault();
                user.Name = value.Name;
                response = await _service.UpdateAsync(user).ConfigureAwait(false);
            }
            else
            {
                response = new UserResponse(new List<string>() { _localizer["Id not found"] });
            }

            return response;
        }

        /// <summary>
        /// Delete a specific User
        /// </summary>    
        /// <param name="id">User Id</param>
        [HttpDelete("{id}")]
        public async Task<UserResponse> Delete(int id)
        {
            Domain.Models.User user;
            UserResponse response;

            user = await _service.GetAsync(id).ConfigureAwait(false);

            if (user != null)
            {
                response = await _service.DeleteAsync(user).ConfigureAwait(false);
            }
            else
            {
                response = new UserResponse(new List<string>() { _localizer["Id not found"] });
            }

            return response;
        }
    }
}
