using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using WebApi.Models;
using System.Web.Http.Cors;

namespace WebApi.Controllers
{
    //[RoutePrefix("api/Accounts")]
    public class AccountsController : BaseApiController
    {
        [Authorize]
        //[Route("Me")]
        [HttpGet]
        public IHttpActionResult Me()
        {
            return Ok(this.UserManager.Users.ToList()
                .Where(u => u.Id == User.Identity.GetUserId())
                .Select(u => this.TheModelFactory.Create(u))
                .First()
                );
        }

        [Authorize]
        //[Route("Users")]
        [HttpGet]
        public IHttpActionResult Users()
        {
            return Ok(this.UserManager.Users.ToList().Select(u => this.TheModelFactory.Create(u)));
        }

        [Authorize]
        [Route("api/Accounts/User/{id:guid}", Name = "GetUserById")]
        [HttpGet]
        public async Task<IHttpActionResult> GetUser(string Id)
        {
            var user = await this.UserManager.FindByIdAsync(Id);

            if (user != null)
            {
                return Ok(this.TheModelFactory.Create(user));
            }

            return NotFound();

        }

        [Authorize]
        [Route("api/Accounts/User/{username}")]
        [HttpGet]
        public async Task<IHttpActionResult> GetUserByName(string username)
        {
            var user = await this.UserManager.FindByNameAsync(username);

            if (user != null)
            {
                return Ok(this.TheModelFactory.Create(user));
            }

            return NotFound();

        }

        [AllowAnonymous]
        //[Route("api/Accounts/Create")]
        [HttpPost]
        public async Task<IHttpActionResult> Create([FromBody]CreateUserBindingModel createUserModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = new UserModel()
            {
                UserName = createUserModel.Username,
                Email = createUserModel.Email,
                FirstName = createUserModel.FirstName,
                LastName = createUserModel.LastName,
                Empleado = new ReservaSalas.Modelos.Empleado()
                {
                    Descripcion = createUserModel.FirstName + " " + createUserModel.LastName,
                    Legajo = (new Random()).Next(1, 2500)
                }
            };

            IdentityResult addUserResult = await this.UserManager.CreateAsync(user, createUserModel.Password);

            if (!addUserResult.Succeeded)
            {
                return GetErrorResult(addUserResult);
            }

            Uri locationHeader = new Uri(Url.Link("GetUserById", new { id = user.Id }));

            return Created(locationHeader, TheModelFactory.Create(user));
        }

        [Authorize]
        //[Route("api/Accounts/ChangePassword")]
        [HttpPost]
        public async Task<IHttpActionResult> ChangePassword([FromBody]ChangePasswordBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            IdentityResult result = await this.UserManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword, model.NewPassword);

            if (!result.Succeeded)
            {
                return GetErrorResult(result);
            }

            return Ok();
        }

        [Authorize]
        [Route("api/Accounts/User/{id:guid}")]
        [HttpDelete]
        public async Task<IHttpActionResult> DeleteUser(string id)
        {

            //Only SuperAdmin or Admin can delete users (Later when implement roles)

            var appUser = await this.UserManager.FindByIdAsync(id);

            if (appUser != null)
            {
                IdentityResult result = await this.UserManager.DeleteAsync(appUser);

                if (!result.Succeeded)
                {
                    return GetErrorResult(result);
                }

                return Ok();

            }

            return NotFound();

        }
    }
}
