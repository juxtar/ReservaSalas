using ReservaSalas.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http.Routing;

namespace WebApi.Models
{
    public class ModelFactory
    {
        private UrlHelper _UrlHelper;
        private AppUserManager _AppUserManager;

        public ModelFactory(HttpRequestMessage request, AppUserManager appUserManager)
        {
            _UrlHelper = new UrlHelper(request);
            _AppUserManager = appUserManager;
        }

        public UserReturnModel Create(UserModel appUser)
        {
            return new UserReturnModel
            {
                Url = _UrlHelper.Link("GetUserById", new { id = appUser.Id }),
                ID = appUser.Id,
                UserName = appUser.UserName,
                FullName = string.Format("{0} {1}", appUser.FirstName, appUser.LastName),
                Email = appUser.Email,
                Empleado_ID = appUser.Empleado_ID
            };
        }
    }

    public class UserReturnModel
    {
        public string Url { get; set; }
        public string ID { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public int? Empleado_ID { get; set; }
    }
}