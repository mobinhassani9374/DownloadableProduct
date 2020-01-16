using DownloadableProduct.Identity.DataModel;
using Microsoft.AspNetCore.Identity;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DownloadableProduct.Identity
{
    public class AppUserClaimsPrincipalFactory : UserClaimsPrincipalFactory<User>
    {
        private readonly UserManager<User> _userManager;
        public AppUserClaimsPrincipalFactory(UserManager<User> userManager,
            Microsoft.Extensions.Options.IOptions<IdentityOptions> options) : base(userManager, options)
        {
            _userManager = userManager;
        }
        public async override Task<ClaimsPrincipal> CreateAsync(User user)
        {
            var roles = await _userManager.GetRolesAsync(user);

            var principal = await base.CreateAsync(user);

            if (!string.IsNullOrEmpty(user.FullName))
            {
                ((ClaimsIdentity)principal.Identity).AddClaims(new[] {
                new Claim(ClaimTypes.GivenName, user.FullName),
                new Claim(ClaimTypes.Surname,user.FullName)
            });

                foreach (var role in roles)
                {
                    ((ClaimsIdentity)principal.Identity).AddClaim(new Claim(ClaimTypes.Role, role));
                }
            }

            return principal;
        }
    }
}
