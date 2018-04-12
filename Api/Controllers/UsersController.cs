using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using DAL.Model;
using DAL.Service;
using RssCrawleraApi.EmailService;
using AutoMapper;
using System.Security.Cryptography;
using System.Text;
using RssCrawleraApi.Email;
using RssCrawleraApi.Models;

namespace RssCrawleraApi.Controllers
{
    [Produces("application/json")]
    [Route("api/Users")]
    public class UsersController : Controller
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly IEmailSender _emailSender;


        public UsersController(IUserService userService, IMapper mapper, IEmailSender emailSender)
        {
            _userService = userService;
            _emailSender = emailSender;
            _mapper = mapper;
        }

        #region Registration
        // POST: api/Users        
        [HttpPost]
        [Route("registration")]
        public IActionResult Registration([FromBody] User user)
        {
            if (!IsEmailExist(user.Email))
            {
                var userDbo = _mapper.Map<User, UserDbo>(user);

                userDbo.Password = Hash(user.Password);

                userDbo.ActivationCode = Guid.NewGuid();

                userDbo.EmailIsValidate = false;

                _userService.AddUser(userDbo);

                SendVerificationLinkEmail(userDbo.FirstName, userDbo.Email, userDbo.ActivationCode.ToString());

                return Ok(userDbo);
            }
            else
            {
                return BadRequest("Email already exist");
            }
        }
        #endregion

        #region VerifyEmail
        [HttpGet("{id}")]
        public void VerifyAccount(string id)
        {
            var v = _userService.GetAll().Where(a => a.ActivationCode == new Guid(id)).FirstOrDefault();

            if (v != null && !v.EmailIsValidate)
            {
                v.EmailIsValidate = true;
                _userService.Save();
                Response.Redirect("http://localhost:4200/room");
            }
        }
        #endregion

        #region Login        
        [HttpPost]
        [Route("login")]
        public UserDbo Login([FromBody] Login login)
        {
            if (this.ModelState.IsValid)
            {
                var user = _userService.GetAll().Where(i => i.Email == login.Email).FirstOrDefault();

                if (user != null && user.Password == Hash(login.Password))
                {
                    return user;
                }

                throw new Exception("not found");
            }
            else
            {
                throw new Exception();
            }
            
        }
        #endregion

        [NonAction]
        private bool IsEmailExist(string email)
        {
            var user = _userService.GetAll().Where(i => i.Email == email).FirstOrDefault();

            return user != null;
        }

        [NonAction]
        private string Hash(string password)
        {
            return Convert.ToBase64String(SHA256.Create().ComputeHash(Encoding.UTF8.GetBytes(password)));
        }

        [NonAction]
        private void SendVerificationLinkEmail(string name, string email, string activationCode)
        {
            var link = new Uri($"{Request.Scheme}://{Request.Host}{"/api/Users"}/{activationCode}").AbsoluteUri;

            var subject = "News Prtal Verification";

            var body = "<br/><br/>We are excited to tell you that your News Portal account is" +
                          " successfully created. Please click on the below link to verify your account" +
                          " <br/><br/><a href='" + link + "'>" + link + "</a> ";

            _emailSender.SendEmailAsync(name, email, subject, body);
        }
    }
}