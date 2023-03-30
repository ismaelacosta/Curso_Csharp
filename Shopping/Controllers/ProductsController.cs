﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Shopping.Data;
using Shopping.Data.Entities;
using Shopping.Helpers;
using Shopping.Models;

namespace Shopping.Controllers
{
    [Authorize(Roles ="Admin")]
    public class ProductsController : Controller
    {
        private readonly DataContext _context;
        private readonly IComboHelper _comboHelper;
        private readonly IBlobHelper _blobHelper;

        public ProductsController(DataContext context, IComboHelper comboHelper, IBlobHelper blobHelper)
        {
            _context = context;
            _comboHelper = comboHelper;
            _blobHelper = blobHelper;
        }

        public async Task<IActionResult> Index()
        {
            /*return _context.Categories != null ?
                        View(await _context.Categories.ToListAsync()) :
                        Problem("Entity set 'DataContext.Categories'  is null.");*/
            try
            {
                return View(await _context.Productcs
                    .Include( p => p.ProductImages)
                    .Include( p => p.ProductCategories)
                    .ThenInclude( pc => pc.Category)
                    .ToListAsync());
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IActionResult> Create()
        {
            CreateProductViewModel model = new()
            {
                Categories = await _comboHelper.GetComboCategoriasAsync(),
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateProductViewModel model)
        {
            if (ModelState.IsValid)
            {
                Guid imageId = Guid.Empty;
                if (model.ImageFile != null)
                {
                    imageId = await _blobHelper.UploadBlobAsync(model.ImageFile, "products");
                }

                Product product = new()
                {
                    Description = model.Description,
                    Name = model.Name,
                    Price = model.Price,
                    Stock = model.Stock,
                };

                product.ProductCategories = new List<ProductCategory>()
                {
                    new ProductCategory
                    {
                        Category = await _context.Categories.FindAsync(model.CategoryId)
                    }
                };

                if (imageId != Guid.Empty)
                {
                    product.ProductImages = new List<ProductImage>()
                    {
                        new ProductImage { ImageId = imageId }
                    };
                }

                try
                {
                    _context.Add(product);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index)); 
                }
                catch (DbUpdateException dbUpdateException)
                {
                    if (dbUpdateException.InnerException.Message.Contains("duplicate"))
                    {
                        ModelState.AddModelError(string.Empty, "Ya existe un producto con el mismo nombre.");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, dbUpdateException.InnerException.Message);
                    }
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, exception.Message);
                }
            }

            model.Categories = await _comboHelper.GetComboCategoriasAsync();
            return View(model);

        }

        public async Task<IActionResult> Edit(int id)
        {
            Product product = await _context.Productcs.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            EditProductViewModel model = new()
            {
                Description = product.Description,
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Stock = product.Stock,
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CreateProductViewModel model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            try
            {
                Product product = await _context.Productcs.FindAsync(model.Id);
                product.Description = model.Description;
                product.Name = model.Name;
                product.Price = model.Price;
                product.Stock = model.Stock;
                _context.Update(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
                
            }
            catch (DbUpdateException dbUpdateException)
            {
                if (dbUpdateException.InnerException.Message.Contains("duplicate"))
                {
                    ModelState.AddModelError(string.Empty, "Ya existe un producto con el mismo nombre.");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, dbUpdateException.InnerException.Message);
                }
            }
            catch (Exception exception)
            {
                ModelState.AddModelError(string.Empty, exception.Message);
            }

            return View(model);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Product product = await _context.Productcs
                .Include(p => p.ProductImages)
                .Include(p => p.ProductCategories)
                .ThenInclude(pc => pc.Category)
                .FirstOrDefaultAsync(p => p.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        public async Task<IActionResult> AddImage(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Product product = await _context.Productcs.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            AddProductImageViewModel model = new()
            {
                ProductId = product.Id,
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddImage(AddProductImageViewModel model)
        {
            if (ModelState.IsValid)
            {
                Guid imageId = await _blobHelper.UploadBlobAsync(model.ImageFile, "products");
                Product product = await _context.Productcs.FindAsync(model.ProductId);
                ProductImage productImage = new()
                {
                    Product = product,
                    ImageId = imageId,
                };

                try
                {
                    _context.Add(productImage);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, exception.Message);
                }
            }

            return View(model);
        }

        public async Task<IActionResult> DeleteImage(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ProductImage productImage = await _context.ProductImages
                .Include(pi => pi.Product) // donde nos regresamos
                .FirstOrDefaultAsync(pi => pi.Id == id);
            if (productImage == null)
            {
                return NotFound();
            }

            await _blobHelper.DeleteBlobAsync(productImage.ImageId, "products");
            _context.ProductImages.Remove(productImage);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Details), new { id = productImage.Product.Id });
        }

        public async Task<IActionResult> AddCategory(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Product product = await _context.Productcs
                .Include(p => p.ProductCategories)
                .ThenInclude(pc => pc.Category)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (product == null)
            {
                return NotFound();
            }


            List<Category> categories = product.ProductCategories.Select( pc => new Category
            {
                Id  = pc.Category.Id,
                Name = pc.Category.Name,
            }).ToList();


            AddCategoryProductViewModel model = new()
            {
                ProductId = product.Id,
                Categories = await _comboHelper.GetComboCategoriasAsync(categories),
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddCategory(AddCategoryProductViewModel model)
        {

            Product product = await _context.Productcs
                .Include(p => p.ProductCategories)
                .ThenInclude(pc => pc.Category)
                .FirstOrDefaultAsync(p => p.Id == model.ProductId);

            if (ModelState.IsValid)
            {
                

                ProductCategory productCategory = new()
                {
                    Category = await _context.Categories.FindAsync(model.CategoryId),
                    Product = product,
                };

                try
                {
                    _context.Add(productCategory);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Details), new { Id = product.Id });
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, exception.Message);
                }
            }


            List<Category> categories = product.ProductCategories.Select(pc => new Category
            {
                Id = pc.Category.Id,
                Name = pc.Category.Name,
            }).ToList();

            model.Categories = await _comboHelper.GetComboCategoriasAsync(categories);
            return View(model);
        }


        public async Task<IActionResult> DeleteCategory(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ProductCategory productCategory = await _context.ProductCategories
                .Include(pc => pc.Product)
                .FirstOrDefaultAsync(pc => pc.Id == id);
            if (productCategory == null)
            {
                return NotFound();
            }

            _context.ProductCategories.Remove(productCategory);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Details), new { Id = productCategory.Product.Id });
        }

        public async Task<IActionResult> Delete(int id)
        {
            if(id == null)
            {
                return NotFound();
            }

            Product product = await _context.Productcs
                .Include(p => p.ProductCategories)
                .Include(p => p.ProductImages)
                .FirstOrDefaultAsync(p => p.Id == id);


            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Product model)
        {
            Product product = await _context.Productcs
                .Include(p => p.ProductCategories)
                .Include(p => p.ProductImages)
                .FirstOrDefaultAsync(p => p.Id == model.Id);

            if (product == null)
            {
                return NotFound();
            }

            _context.Productcs.Remove(product);
            await _context.SaveChangesAsync();

            foreach (ProductImage productImage in product.ProductImages)
            {
                await _blobHelper.DeleteBlobAsync(productImage.ImageId, "products");
            }

         
            return RedirectToAction(nameof(Index));
        }

    }
}
