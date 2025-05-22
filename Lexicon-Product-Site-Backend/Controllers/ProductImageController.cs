using System.Globalization;
using System.Threading.Tasks;
using Lexicon_Product_Site_Backend.Models;
using Lexicon_Product_Site_Backend.Requests;
using Microsoft.AspNetCore.Mvc;

namespace Lexicon_Product_Site_Backend.Controllers
{
    [ApiController]
    [Route("i")]
    public class ProductImageController(PSiteDB pSiteDB) : Controller
    {
        private readonly PSiteDB _pSiteDB = pSiteDB;
        private readonly IWebHostEnvironment _env;

        [HttpPost]
        [Route("/upload")]
        public async Task<IResult> UploadImage(NewProductImage newProductImage, IFormFile file)
        {
            // Check if image file exists
            if (file == null || file.Length == 0)
            {
                return Results.BadRequest("No file was uploaded.");
            }

            // Check if connected product exists
            Product? product = _pSiteDB.Products.Find(newProductImage.ProductID);
            if (product == null)
            {
                return Results.BadRequest("Product to upload images to was not found");
            }

            // Check if upload directory exists, if not create it.
            var uploadFolder = Path.Combine(_env.WebRootPath, "Images", newProductImage.ProductID.ToString());
            if (!Directory.Exists(uploadFolder))
            {
                Directory.CreateDirectory(uploadFolder);
            }

            #region Get Highest ID
            // Get highest current ID from database
            var images = product.Images;
            int currentID = 0;

            foreach (var item in images)
            {
                if (currentID < item.ImageID)
                {
                    currentID = item.ImageID;
                }
            }

            currentID++;
            #endregion

            string stringID = currentID.ToString();

            if (currentID < 10)
            {
                stringID = "0" + stringID;
            }

            string extension = Path.GetExtension(file.FileName);

            string fileName;

            if (newProductImage.IsThumbnail)
            {
                fileName = product.Name + "_thumbnail" + extension;
            }
            else
            {
                fileName = product.Name + "_img-" + stringID + extension;
            }

            string filePath = Path.Combine(uploadFolder, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            #region Set image variables
            // Create new image object
            ProductImage newImage = new ProductImage();

            newImage.ImageID = currentID;
            newImage.ProductID = product.ProductID;
            newImage.Product = product;
            newImage.Path = filePath;
            newImage.Name = fileName;
            newImage.Extension = extension;
            newImage.AltDescription = newProductImage.AltDescription;
            newImage.IsThumbnail = newProductImage.IsThumbnail;
            #endregion

            product.Images.Add(newImage);
            product.ImageIDs.Add(newImage.ImageID);

                try
                {
                    _pSiteDB.ProductImages.Add(newImage);
                    _pSiteDB.SaveChanges();

                    return Results.Ok(new
                    {
                        status = "Image added successfully."
                    });
                }
                catch (Exception ex)
                {
                    return Results.Conflict(ex.ToString());
                }
        }
    }
}