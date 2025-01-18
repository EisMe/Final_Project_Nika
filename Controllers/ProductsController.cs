﻿using Final_Project_Nika.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class ProductsController : Controller
{
    private readonly AdventureWorksLTDbContext _context;

    public ProductsController(AdventureWorksLTDbContext context)
    {
        _context = context;
    }

    // Helper method to set ModifiedDate
    private void SetModifiedDate(Product product)
    {
        product.ModifiedDate = DateTime.UtcNow;
    }

    // GET: Products
    public async Task<IActionResult> Index()
    {
        var adventureWorksLTDbContext = _context.Products
            .Take(20)
            .Include(p => p.ProductCategory)
            .Include(p => p.ProductModel);
        return View(await adventureWorksLTDbContext.ToListAsync());
    }

    // GET: Products/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var product = await _context.Products
            .Include(p => p.ProductCategory)
            .Include(p => p.ProductModel)
            .FirstOrDefaultAsync(m => m.ProductId == id);
        if (product == null)
        {
            return NotFound();
        }

        return View(product);
    }

    // GET: Products/Create
    public IActionResult Create()
    {
        ViewData["ProductCategoryId"] = new SelectList(_context.ProductCategories, "ProductCategoryId", "Name");
        ViewData["ProductModelId"] = new SelectList(_context.ProductModels, "ProductModelId", "Name");
        return View();
    }

    // POST: Products/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("ProductId,Name,ProductNumber,Color,StandardCost,ListPrice,Size,Weight,ProductCategoryId,ProductModelId,SellStartDate,SellEndDate,DiscontinuedDate,ThumbNailPhoto,ThumbnailPhotoFileName,Rowguid,ModifiedDate")] Product product)
    {
        if (ModelState.IsValid)
        {
            SetModifiedDate(product); // Set ModifiedDate here before saving

            _context.Add(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        ViewData["ProductCategoryId"] = new SelectList(_context.ProductCategories, "ProductCategoryId", "Name", product.ProductCategoryId);
        ViewData["ProductModelId"] = new SelectList(_context.ProductModels, "ProductModelId", "Name", product.ProductModelId);
        return View(product);
    }

    // GET: Products/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var product = await _context.Products.FindAsync(id);
        if (product == null)
        {
            return NotFound();
        }
        ViewData["ProductCategoryId"] = new SelectList(_context.ProductCategories, "ProductCategoryId", "Name", product.ProductCategoryId);
        ViewData["ProductModelId"] = new SelectList(_context.ProductModels, "ProductModelId", "Name", product.ProductModelId);
        return View(product);
    }

    // POST: Products/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("ProductId,Name,ProductNumber,Color,StandardCost,ListPrice,Size,Weight,ProductCategoryId,ProductModelId,SellStartDate,SellEndDate,DiscontinuedDate,ThumbNailPhoto,ThumbnailPhotoFileName,Rowguid,ModifiedDate")] Product product)
    {
        if (id != product.ProductId)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                SetModifiedDate(product); // Set ModifiedDate here before saving

                _context.Update(product);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(product.ProductId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }
        ViewData["ProductCategoryId"] = new SelectList(_context.ProductCategories, "ProductCategoryId", "Name", product.ProductCategoryId);
        ViewData["ProductModelId"] = new SelectList(_context.ProductModels, "ProductModelId", "Name", product.ProductModelId);
        return View(product);
    }

    // GET: Products/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var product = await _context.Products
            .Include(p => p.ProductCategory)
            .Include(p => p.ProductModel)
            .FirstOrDefaultAsync(m => m.ProductId == id);

        bool hasOrders = _context.SalesOrderDetails.Any(p => p.ProductId == id);
        ViewBag.HasOrders = hasOrders;

        if (product == null)
        {
            return NotFound();
        }

        return View(product);
    }

    // POST: Products/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var product = await _context.Products.FindAsync(id);
        if (product != null)
        {
            _context.Products.Remove(product);
        }
        if (_context.SalesOrderDetails.Any(p => p.ProductId == id))
        {
            TempData["Error"] = "Cannot delete product with existing orders.";
            return RedirectToAction(nameof(Index));
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool ProductExists(int id)
    {
        return _context.Products.Any(e => e.ProductId == id);
    }
}
