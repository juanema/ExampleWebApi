using ExampleWebApi.Core.Domain.Models;
using ExampleWebApi.Core.Domain.Models.Validations;
using ExampleWebApi.Core.Domain.Persistence.Context;
using ExampleWebApi.Core.Domain.Services.Communication;
using ExampleWebApi.Core.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace ExampleWebApi.Core.Controllers.v2
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IStringLocalizer<UsersController> _localizer;
        private readonly AppDbContext _appDbContext;

        public UsersController(AppDbContext appDbContext, IStringLocalizer<UsersController> localizer)
        {
            _appDbContext = appDbContext;
            _localizer = localizer;
        }


        /// <summary>
        /// Get all Users
        /// </summary>        
        [HttpGet]
        public async Task<IEnumerable<User>> Get()
        {
            return await _appDbContext.Users.ToListAsync().ConfigureAwait(false);
        }


        /// <summary>
        /// Get a specific User
        /// </summary>    
        /// <param name="id">User Id</param>
        [HttpGet("{id}", Name = "Get")]
        public async Task<User> Get(int id)
        {
            Domain.Models.User result;

            result = await _appDbContext.Users.FindAsync(id).ConfigureAwait(false);
            if (result == null)
            {
                throw new InvalidOperationException(_localizer["Id not found"]);
            }

            return result;
        }

        /// <summary>
        /// Create or update a User
        /// </summary>            
        /// <param name="id">User Id</param>
        /// <param name="value">Validated values</param>
        [HttpPost]
        public async Task<SaveUserResponse> Post(int? id, [FromBody] SaveUserValidation value)
        {
            Domain.Models.User result;
            IList<string> messages = new List<string>();
            value.ThrowExceptionIfNull();
            value.BirthDate.ThrowExceptionIfNull();

            if (id.HasValue)
            {
                result = await _appDbContext.Users.FindAsync(id).ConfigureAwait(false);

                if (result != null)
                {
                    result.Birthdate = value.BirthDate.GetValueOrDefault();
                    result.Name = value.Name;
                    _appDbContext.Update(result);
                }
                else
                {
                    result = new Domain.Models.User() { Id = id.Value, Birthdate = value.BirthDate.GetValueOrDefault(), Name = value.Name };
                    _appDbContext.Add(result);
                }
            }
            else
            {
                result = new Domain.Models.User() { Birthdate = value.BirthDate.GetValueOrDefault(), Name = value.Name };
                _appDbContext.Add(result);
            }
            _appDbContext.SaveChanges();

            return result != null ? new SaveUserResponse(result) : new SaveUserResponse(messages);
        }

        /// <summary>
        /// Update a specific User
        /// </summary>    
        /// <param name="id">User Id</param>
        /// <param name="value">Validated values</param>
        [HttpPut("{id}")]
        public async Task<SaveUserResponse> Put(int id, [FromBody] SaveUserValidation value)
        {
            value.ThrowExceptionIfNull();
            value.BirthDate.ThrowExceptionIfNull();
            Domain.Models.User result;
            IList<string> messages = new List<string>();

            result = await _appDbContext.Users.FindAsync(id).ConfigureAwait(false);

            if (result != null)
            {
                result.Birthdate = value.BirthDate.GetValueOrDefault();
                result.Name = value.Name;
                _appDbContext.Update(result);
                _appDbContext.SaveChanges();
            }
            else
            {
                messages.Add(_localizer["Id not found"]);
            }

            return result != null ? new SaveUserResponse(result) : new SaveUserResponse(messages);
        }

        /// <summary>
        /// Delete a specific User
        /// </summary>    
        /// <param name="id">User Id</param>
        [HttpDelete("{id}")]
        public async Task<SaveUserResponse> Delete(int id)
        {
            Domain.Models.User result;
            IList<string> messages = new List<string>();

            result = await _appDbContext.Users.FindAsync(id).ConfigureAwait(false);

            if (result != null)
            {
                _appDbContext.Remove(result);
                _appDbContext.SaveChanges();
            }
            else
            {
                messages.Add(_localizer["Id not found"]);
            }

            return result != null ? new SaveUserResponse(result) : new SaveUserResponse(messages);
        }
    }
}
