using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.ApplicationUser.Commands.Models;
using SchoolProject.Core.SharedResourcesHelper;
using SchoolProject.Domain.Entities.Identity;

namespace SchoolProject.Core.Features.ApplicationUser.Commands.Hanlders
{
    internal class UserCommandHandler : ResponseHandler,
        IRequestHandler<AddUserCommand, Response<string>>,
        IRequestHandler<UpdateApplicationUserCommand, Response<string>>,
        IRequestHandler<DeleteApplicationUserCommand, Response<string>>,
        IRequestHandler<ChangeApplicationUserPasswordCommand, Response<string>>
    {

        #region Fields
        private readonly IMapper _mapper;
        private readonly IStringLocalizer _stringLocalizer;
        private readonly UserManager<User> _userManager;
        #endregion

        #region Constructors
        public UserCommandHandler(IStringLocalizer<SharedResourcesLocalization> localizer,
            IMapper mapper, UserManager<User> userManager
            ) : base(localizer)
        {
            _stringLocalizer = localizer;
            _mapper = mapper;
            _userManager = userManager;
        }
        #endregion

        #region Handlers
        public async Task<Response<string>> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user is not null)
                return BadRequest<string>
                    (_stringLocalizer[LocalizationSharedResourcesKeys.EmailAlreadyUsed]);

            var userByUserName = await _userManager.FindByNameAsync(request.UserName);

            if (userByUserName is not null)
                return BadRequest<string>
                    (_stringLocalizer[LocalizationSharedResourcesKeys.UserNameAlreadyExist]);

            var identityUser = _mapper.Map<User>(request);

            var createdResult = await _userManager.CreateAsync(identityUser, request.Password);

            if (createdResult.Succeeded)
                return Created<string>(_stringLocalizer[LocalizationSharedResourcesKeys.Created]);

            if (createdResult.Errors.Any())
                return UnprocessableEntity<string>(createdResult.Errors.FirstOrDefault()!.Description);
            else
                return UnprocessableEntity<string>();

        }

        public async Task<Response<string>> Handle(UpdateApplicationUserCommand request, CancellationToken cancellationToken)
        {
            var oldUser = await _userManager.FindByIdAsync(request.Id.ToString());

            if (oldUser is null) return NotFound<string>();

            var newUser = _mapper.Map(request, oldUser);

            var userByUserName = await _userManager.Users.FirstOrDefaultAsync(
                x => x.UserName == newUser.UserName && x.Id != newUser.Id);

            if (userByUserName != null) return BadRequest<string>(_stringLocalizer[LocalizationSharedResourcesKeys.UserNameAlreadyExist]);

            var result = await _userManager.UpdateAsync(newUser);

            if (!result.Succeeded) return BadRequest<string>(_stringLocalizer[LocalizationSharedResourcesKeys.ExpectationFailed]);

            return Success((string)_stringLocalizer[LocalizationSharedResourcesKeys.Updated]);
        }

        public async Task<Response<string>> Handle(DeleteApplicationUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == request.Id);

            if (user is null) return NotFound<string>();

            // @TODO - Delete every thing is related to the user

            var deletionResult = await _userManager.DeleteAsync(user);

            if (deletionResult.Succeeded)
                return Deleted<string>();

            return Failed<string>();
        }

        public async Task<Response<string>> Handle(ChangeApplicationUserPasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == request.Id);

            if (user is null) return NotFound<string>();

            var changePasswordResult = await _userManager.ChangePasswordAsync(user, request.CurrentPassword, request.NewPassword);

            if (changePasswordResult.Succeeded)
                return Success("");

            return BadRequest<string>();
        }
        #endregion
    }
}
