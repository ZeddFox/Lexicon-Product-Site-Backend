using Lexicon_Product_Site_Backend.Models;
using Lexicon_Product_Site_Backend.Requests;
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
            return Results.Ok(new
            {
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
        }

        [Route("create")]
        [HttpPost]
        public IResult Create(NewProduct newProduct)
        {
            Product product = new Product();
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

            newProduct.ProductID = currentID;
            #endregion

            #region Set product variables
            product.ProductID = currentID;
            product.Name = newProduct.Name;
            product.Price = newProduct.Price;
            product.Amount = newProduct.Amount;
            product.Category = newProduct.Category;
            product.Description = newProduct.Description;
            // product.Images = newProduct.Images;                              Find out how this works
            product.Enabled = newProduct.Enabled;
            #endregion

            try
            {
                _productDB.Products.Add(newProduct);
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