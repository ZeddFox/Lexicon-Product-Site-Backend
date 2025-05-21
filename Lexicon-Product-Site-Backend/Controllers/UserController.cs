using Lexicon_Product_Site_Backend.Models;
using Lexicon_Product_Site_Backend.Requests;
using Microsoft.AspNetCore.Mvc;

namespace Lexicon_Product_Site_Backend.Controllers
{
    [ApiController]
    [Route("[user]")]
    public class UserController(UserDB userDB) : Controller
    {
        private readonly UserDB _userDB = userDB;

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
                    //User user = _userDB.Users.FirstOrDefault();

                    if (loginRequest.Password == user.password)
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
            user = _userDB.Find(user.Email == newUser.Email);

            // If user doesn't exist, continue registering
            if (user == null)
            {
                #region Get Highest ID
                // Get highest current ID from database
                var users = _userDB.Users.ToList();
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
                    _userDB.Users.Add(user);
                    _userDB.SaveChanges();

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

/*
using lexicon_message_thread_backend.HttpModels;
using Microsoft.AspNetCore.Mvc;

namespace lexicon_message_thread_backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MessageController(MessageDB messageDB) : Controller
    {
        private readonly MessageDB _messageDB = messageDB;

        [Route("read")]
        [HttpGet]
        public IResult Index()
        {
            return Results.Ok(new
            {
                messages = _messageDB.Messages.ToList()
            });
        }

        [Route("write")]
        [HttpPost]
        public IResult Write(Message message)
        {
            var messages = _messageDB.Messages.ToList();
            int currentID = 0;

            foreach (var item in messages)
            {
                if (currentID < item.MessageID)
                {
                    currentID = item.MessageID;
                }
            }

            currentID++;

            message.MessageID = currentID;
            message.SendDate = DateTime.Now;

            try
            {
                _messageDB.Messages.Add(message);
                _messageDB.SaveChanges();

                return Results.Ok(new
                {
                    status = "Message sent successfully."
                });
            }
            catch (Exception ex)
            {
                return Results.Conflict(ex.ToString());
            }
        }
    }
}
*/