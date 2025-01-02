using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Authorization.Queries.Models;
using SchoolProject.Core.SharedResourcesHelper;
using SchoolProject.Domain.Entities.Identity;
using SchoolProject.Domain.Results;
using SchoolProject.Service.Abstracts;

namespace SchoolProject.Core.Features.Authorization.Queries.Handlers
{
    public class ClaimsQueryHandler : ResponseHandler,
        IRequestHandler<ManageUserClaimsQuery, Response<ManageUserClaimsResults>>
    {

        #region  Fields
        private readonly IAuthorizationService _authorizationService;
        private readonly UserManager<User> _userManager;
        #endregion

        #region  Constructors
        public ClaimsQueryHandler(IStringLocalizer<SharedResourcesLocalization> localizer, IAuthorizationService authorizationService, UserManager<User> userManager) : base(localizer)
        {
            _authorizationService = authorizationService;
            _userManager = userManager;
        }
        #endregion

        #region  Handle Functions
        public async Task<Response<ManageUserClaimsResults>> Handle(ManageUserClaimsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                User user = await _userManager.FindByIdAsync(request.UserId.ToString());
                ManageUserClaimsResults response = await _authorizationService
                                                         .ManageUserClaimsDataAsync(user);
                if (response is null)
                    return Failed<ManageUserClaimsResults>();
                return Success(response);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return Failed<ManageUserClaimsResults>();
            }
        }
        #endregion

    }
}