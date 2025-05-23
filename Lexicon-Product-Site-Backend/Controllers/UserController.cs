using Lexicon_Product_Site_Backend.Models;
using Lexicon_Product_Site_Backend.Requests;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;

namespace Lexicon_Product_Site_Backend.Controllers
{
    [ApiController]
    [Route("user")]
    public class UserController(PSiteDB pSiteDB) : Controller
    {
        private readonly PSiteDB _pSiteDB = pSiteDB;

        [Route("/login")]
        [HttpPost]
        public IResult Login(LoginRequest loginRequest)
        {
            // Find user by Email
            // If null, return HTTP 404 Not Found
            if (loginRequest.Email == null)
            {
                return Results.NotFound();
            }
            // If Email is not null, try to get user by Email
            else
            {
                try
                {
                    // Find user in database
                    User user = _pSiteDB.Users.Where(item => item.Email == loginRequest.Email).Single();

                    if (loginRequest.Password == user.Password)
                    {
                        return Results.Ok(user);
                    }
                    else
                    {
                        return Results.Conflict(new
                        {
                            message = "Password was incorrect"
                        });
                    }
                }
                // If not found, return HTTP 404 Not Found
                catch
                {
                    return Results.NotFound();
                }
            }
        }

        [Route("/register")]
        [HttpPost]
        public IResult Register(RegisterRequest newUser)
        {
            User foundUser = null;
            try
            {
                // Check if user with that email already exists
                foundUser = _pSiteDB.Users.Where(item => item.Email == newUser.Email).Single();

                // If user with same email already exist, return a HTTP Confict with message.
                return Results.Conflict(new
                {
                    message = "User with that email already exists."
                });
            }
            catch
            {
                #region Get Highest ID
                // Get highest current ID from database
                var users = _pSiteDB.Users.ToList();
                int currentID = 0;

                foreach (var item in users)
                {
                    if (currentID < item.UserID)
                    {
                        currentID = item.UserID;
                    }
                }
                currentID++;
                #endregion

                #region Set user variables
                User user = new User();

                user.UserID = currentID;
                user.Email = newUser.Email;
                user.FirstName = newUser.FirstName;
                user.LastName = newUser.LastName;
                user.Password = newUser.Password;
                #endregion

                try
                {
                    _pSiteDB.Users.Add(user);
                    _pSiteDB.SaveChanges();

                    return Results.Ok(new
                    {
                        status = "User added successfully."
                    });
                }
                catch (Exception ex)
                {
                    return Results.Conflict(ex.ToString());
                }
            }
        }

        [Route("/giveadmin")]
        [HttpPost]
        public IResult GiveAdmin(AdminRequest adminRequest)
        {
            if (adminRequest.Password == "SuperSecretAdminPassword")
            {
                User toAdmin = _pSiteDB.Users.Find(adminRequest.UserID);
                toAdmin.IsAdmin = "true";
                _pSiteDB.SaveChanges();
                return Results.Ok();
            }
            else
            {
                return Results.Forbid();
            }
        }
        [Route("/removeadmin")]
        [HttpPost]
        public IResult RemoveAdmin(AdminRequest unadminRequest)
        {
            if (unadminRequest.Password == "SuperSecretAdminPassword")
            {
                User toNormal = _pSiteDB.Users.Find(unadminRequest.UserID);
                toNormal.IsAdmin = "false";
                _pSiteDB.SaveChanges();
                return Results.Ok();
            }
            else
            {
                return Results.Forbid();
            }
        }

        [Route("/getallusers")]
        [HttpGet]
        public IResult GetAllUsers(string password)
        {
            if (password == "SuperSecretAdminPassword")
            {
                return Results.Ok(new
                {
                    users = _pSiteDB.Users.ToList()
                });
            }
            else
            {
                return Results.Forbid();
            }
        }
    }
}