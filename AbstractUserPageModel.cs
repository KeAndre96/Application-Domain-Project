using AppDomainProject.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppDomainProject
{
    
    public abstract class AbstractUserPageModel : PageModel
    {
        protected AppDomainProjectContext _context { get; }

        private PersonalInfoData _personalInfo = null;
        protected PersonalInfoData PersonalInfo
        {
            get {
                if (_personalInfo == null)
                    _personalInfo = GetPersonalInfo();
                return _personalInfo;
            }
        }

        private UserInfoData _userInfo = null;
        protected UserInfoData UserInfo
        {
            get
            {
                if (_userInfo == null)
                    _userInfo = GetUserInfo();
                return _userInfo;
            }
        }

        public AbstractUserPageModel(AppDomainProjectContext context)
        {
            _context = context;
        }

        private PersonalInfoData GetPersonalInfo()
        {
            var query = from u in _context.PersonalInfoData select u;
            string id = HttpContext.User.FindFirst("ID")?.Value;
            query = query.Where(m => m.ID.Equals(id));

            return query.FirstOrDefault();
        }

        private UserInfoData GetUserInfo()
        {
            var query = from u in _context.UserInfoData select u;
            string id = HttpContext.User.FindFirst("ID")?.Value;
            query = query.Where(m => m.ID.Equals(id));

            return query.FirstOrDefault();
        }
    }

    /// <summary>
    /// Extend the class 'AuthenticatedPageModel' for a page that should be accessible to any logged in user
    /// </summary>
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme, Policy = "registered")]
    public abstract class AuthenticatedPageModel : AbstractUserPageModel
    {
        public AuthenticatedPageModel(AppDomainProjectContext context) : base(context) { }
    }

    /// <summary>
    /// Extend the class 'UserPageModel' for a page that should be accessible only to a 'User' account
    /// </summary>
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme, Policy = "user")]
    public abstract class UserPageModel : AbstractUserPageModel
    {
        public UserPageModel(AppDomainProjectContext context) : base(context) { }
    }


    /// <summary>
    /// Extend the class 'ManagerPageModel' for a page that should be accessible only to a 'Manager' account
    /// </summary>
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme, Policy = "manager")]
    public abstract class ManagerPageModel : AbstractUserPageModel
    {
        public ManagerPageModel(AppDomainProjectContext context) : base(context) { }
    }

    /// <summary>
    /// Extend the class 'AdminPageModel' for a page that should be accessible only to a 'Admin' account
    /// </summary>
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme, Policy = "admin")]
    public abstract class AdminPageModel : AbstractUserPageModel
    {
        public AdminPageModel(AppDomainProjectContext context) : base(context) { }
    }
}
