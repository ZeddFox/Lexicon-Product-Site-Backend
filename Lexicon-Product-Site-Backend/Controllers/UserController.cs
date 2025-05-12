using Lexicon_Product_Site_Backend.Models;
using Microsoft.AspNetCore.Mvc;

namespace Lexicon_Product_Site_Backend.Controllers
{
    [ApiController]
    [Route("[user]")]
    public class UserController(UserDB userDB) : Controller
    {
        private readonly UserDB _userDB = userDB;

        [Route("all")]
        [HttpGet]
        public IResult Index()
        {
            return Results.Ok({
                users = _userDB.Users.ToList()
            });
        }
        [Route("read")]
        [HttpGet]
        public IResult Read(User user)
        {
            // Find user by ID
            // If null, try to find using Email
            if (user.UserID == null)
             {
                // Find user by Email
                // If null, return HTTP 404 Not Found
                if (user.Email == null)
                {
                    return Results.NotFound();
                }
                // If Email is not null, try to get user by Email
                else
                {
                    try
                    {
                        //user = _userDB.Users.FirstOrDefault();
                    }
                    // If not found, return HTTP 404 Not Found
                    catch
                    {
                        return Results.NotFound();
                    }
                }
            }
            // If ID is not null, try to get user by ID
            else
            {
                try
                {
                    //user = _userDB.Users.FirstOrDefault();
                }
                // If not found, return HTTP 404 Not Found
                catch
                {
                    return Results.NotFound();
                }
            }
        }

        [Route("create")]
        [HttpPost]
        public IResult Create(User user)
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

            user.UserID = currentID;
            #endregion

            try
            {
                _userDB.Users.Add(user);
                _userDB.SaveChanges();

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