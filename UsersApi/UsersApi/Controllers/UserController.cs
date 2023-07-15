using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Security.Claims;
using UsersApi.Configs;
using UsersApi.Dtos;
using UsersApi.Interfaces;
using UsersApi.Models.Request;

namespace UsersApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Policy = "TokenPolicy")]
    public class UserController : ControllerBase
    {
        private readonly IReqResApi _reqResApi;
        private readonly IMapper _mapper;
        private IValidator<CreateUserRequestModel> _createUservalidator;
        private IValidator<UpdateUserRequestModel> _updateUserRequestModel;

        public UserController(IReqResApi reqResApi,IMapper mapper, IValidator<CreateUserRequestModel> createUservalidator, IValidator<UpdateUserRequestModel> updateUserRequestModel)
        {
            _reqResApi = reqResApi;
            _mapper = mapper;
            _createUservalidator = createUservalidator;
            _updateUserRequestModel = updateUserRequestModel;
        }
        
       [HttpGet("getUsers/{page}")]
        public async Task<IActionResult> GetUsers(int page)
        {
            if(page >= 0)
            {
                var users = await _reqResApi.GetAllUsersByPage(page);
                var _mappedUser = _mapper.Map<List<UserDto>>(users);
                return Ok(_mappedUser);
            }
            return NotFound("The requested resource was not found.");
        }

        [HttpGet("getUser/{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            if (id >= 0)
            {
                var users = await _reqResApi.GetUserById(id);
                var _mappedUser = _mapper.Map<UserDto>(users);
                return Ok(_mappedUser);
            }
            return NotFound("The requested resource was not found.");
        }

       [HttpPost("createUser")]
        public async Task<IActionResult> CreateUser(CreateUserRequestModel user)
        {
            ValidationResult result = await _createUservalidator.ValidateAsync(user);

            if (result.IsValid)
            {
                var users = await _reqResApi.CreateUser(user);
                return Ok(users);
            }
            return NotFound("The requested resource was not found.");
        }

        [HttpPut("updateUser/{id}")]
        public async Task<IActionResult> UpdateUser(int id, UpdateUserRequestModel updateUserRequest)
        {
            ValidationResult result = await _updateUserRequestModel.ValidateAsync(updateUserRequest);

            if (result.IsValid && id >= 0)
            {
                var userExist= await _reqResApi.GetUserById(id);
                if(userExist.Id == id)
                {
                    var updateUserDto = UpdateUserDto.Create(id, updateUserRequest);
                    var user = await _reqResApi.UpdateUser(updateUserDto);
                    return Ok(user);
                }
            }
            return NotFound("The requested resource was not found.");
        }

        [HttpDelete("deleteUser/{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            if (id >= 0)
            {
                var userExist = await _reqResApi.GetUserById(id);
                if (userExist.Id == id)
                {
                    await _reqResApi.DeleteUserById(id);
                    return Ok();
                }
            }
            return NotFound("The requested resource was not found.");
        }
    }
}
