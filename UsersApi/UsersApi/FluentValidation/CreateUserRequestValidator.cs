using FluentValidation;
using System;
using UsersApi.Models.Request;

namespace UsersApi.FluentValidation
{
    public class CreateUserRequestValidator : AbstractValidator<CreateUserRequestModel>
    {
        public CreateUserRequestValidator()
        {
            RuleFor(user => user.Name).NotEmpty();
            RuleFor(user => user.Job).NotEmpty();
        }
    }
}
