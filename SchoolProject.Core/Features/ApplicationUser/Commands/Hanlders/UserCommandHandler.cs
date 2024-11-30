using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.ApplicationUser.Commands.Models;
using SchoolProject.Core.SharedResourcesHelper;
using SchoolProject.Domain.Entities.Identity;

namespace SchoolProject.Core.Features.ApplicationUser.Commands.Hanlders
{
    internal class UserCommandHandler : ResponseHandler,
        IRequestHandler<AddUserCommand, Response<string>>
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
        #endregion 
    }
}
