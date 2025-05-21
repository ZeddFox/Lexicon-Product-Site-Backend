using Lexicon_Product_Site_Backend.Models;
using Lexicon_Product_Site_Backend.Requests;
using Microsoft.AspNetCore.Mvc;

namespace Lexicon_Product_Site_Backend.Controllers
{
    [ApiController]
    [Route("[product]")]
    public class ProductController(PSiteDB pSiteDB) : Controller
    {
        private readonly PSiteDB _pSiteDB = pSiteDB;

        [Route("all")]
        [HttpGet]
        public IResult Index()
        {
            return Results.Ok(new
            {
                products = _pSiteDB.Products.ToList()
            });
        }
        [Route("read")]
        [HttpGet]
        public IResult Read(GetProduct getProduct)
        {
            // Find product by ID
            // If null, try to find using Name
            if (getProduct.ProductID == null)
            {
                // Find product by Name
                // If null, return HTTP 404 Not Found
                if (getProduct.Name == null)
                {
                    return Results.NotFound();
                }
                // If Email is not null, try to get product by Name
                else
                {
                    try
                    {
                        Product product = _pSiteDB.Products.FirstOrDefault();
                        return Results.Ok(product);
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
                    Product product = _pSiteDB.Products.FirstOrDefault();
                    return Results.Ok(product);
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
            var products = _pSiteDB.Products.ToList();
            int currentID = 0;

            foreach (var item in products)
            {
                if (currentID < item.ProductID)
                {
                    currentID = item.ProductID;
                }
            }

            currentID++;
            #endregion

            #region Set product variables
            product.ProductID = currentID;
            product.Name = newProduct.Name;
            product.Price = newProduct.Price;
            product.Amount = newProduct.Amount;
            product.Category = newProduct.Category;
            product.Description = newProduct.Description;
            product.Enabled = newProduct.Enabled;
            #endregion

            try
            {
                _pSiteDB.Products.Add(newProduct);
                _pSiteDB.SaveChanges();

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