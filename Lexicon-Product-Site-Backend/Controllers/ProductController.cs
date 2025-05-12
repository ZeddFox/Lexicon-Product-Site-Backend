using Lexicon_Product_Site_Backend.Models;
using Microsoft.AspNetCore.Mvc;

namespace Lexicon_Product_Site_Backend.Controllers
{
    [ApiController]
    [Route("[product]")]
    public class ProductController(ProductDB productDB) : Controller
    {
        private readonly ProductDB _productDB = productDB;

        [Route("all")]
        [HttpGet]
        public IResult Index()
        {
            return Results.Ok({
                products = _productDB.Products.ToList()
            });
        }
        [Route("read")]
        [HttpGet]
        public IResult Read(Product product)
        {
            // Find product by ID
            // If null, try to find using Name
            if (product.ProductID == null)
            {
                // Find product by Name
                // If null, return HTTP 404 Not Found
                if (product.Name == null)
                {
                    return Results.NotFound();
                }
                // If Email is not null, try to get product by Name
                else
                {
                    try
                    {
                        //product = _productDB.Products.FirstOrDefault();
                    }
                    // If not found, return HTTP 404 Not Found
                    catch
                    {
                        return Results.NotFound();
                    }
                }
            }
            // If ID is not null, try to get product by ID
            else
            {
                try
                {
                    //product = _productDB.Products.FirstOrDefault();
                }
                // If not found, return HTTP 404 Not Found
                catch
                {
                    return Results.NotFound();
                }
            }

            if (product != null)
            {
                return Results.Ok(product);
            }
            else
            {
                return Results.NotFound();
            }
        }

        [Route("create")]
        [HttpPost]
        public IResult Create(Product product)
        {
            #region Get Highest ID
            // Get highest current ID from database
            var products = _productDB.Products.ToList();
            int currentID = 0;

            foreach (var item in products)
            {
                if (currentID < item.ProductID)
                {
                    currentID = item.ProductID;
                }
            }

            currentID++;

            product.ProductID = currentID;
            #endregion

            try
            {
                _productDB.Products.Add(product);
                _productDB.SaveChanges();

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