using FluentValidation;
using UsersApi.Interfaces;
using UsersApi.Models.Request;

namespace UsersApi.FluentValidation
{
    public class UpdateUserRequestValidator : AbstractValidator<UpdateUserRequestModel>
    {
        private readonly IReqResApi _reqResApi;
        public UpdateUserRequestValidator(IReqResApi reqResApi)
        {
            _reqResApi = reqResApi;
        }
        public UpdateUserRequestValidator()
        {
            RuleFor(user => user.Name).NotEmpty();
            RuleFor(user => user.Job).NotEmpty();

        }
    }
}
