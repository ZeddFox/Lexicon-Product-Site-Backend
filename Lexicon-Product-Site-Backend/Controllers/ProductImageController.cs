using Lexicon_Product_Site_Backend.Models;
using Microsoft.AspNetCore.Mvc;

namespace Lexicon_Product_Site_Backend.Controllers
{
    public class ProductImageController
    {
        [ApiController]
        [Route("[i]")]
        public class ProductController(PSiteDB pSiteDB) : Controller
        {
            private readonly PSiteDB _pSiteDB = pSiteDB;

        }
    }
}