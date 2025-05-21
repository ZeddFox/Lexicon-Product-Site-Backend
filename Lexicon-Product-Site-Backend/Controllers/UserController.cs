using Lexicon_Product_Site_Backend.Models;
using Lexicon_Product_Site_Backend.Requests;
using Microsoft.AspNetCore.Mvc;

namespace Lexicon_Product_Site_Backend.Controllers
{
    [ApiController]
    [Route("[user]")]
    public class UserController(PSiteDB pSiteDB) : Controller
    {
        private readonly PSiteDB _pSiteDB = pSiteDB;

        [Route("login")]
        [HttpGet]
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
                    User user = _pSiteDB.Users.FirstOrDefault();

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

        [Route("register")]
        [HttpPost]
        public IResult Register(RegisterRequest newUser)
        {
            User user = new User();
            // Check if user with that email already exists
            user = _pSiteDB.Users.FirstOrDefault(newUser.Email);

            // If user doesn't exist, continue registering
            if (user == null)
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
            // If user with same email already exist, return a HTTP Confict with message.
            else
            {
                return Results.Conflict(new
                {
                    message = "User with that email already exists."
                });
            }
        }
    }
}