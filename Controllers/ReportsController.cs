using Final_Project_Nika.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Final_Project_Nika;
using Final_Project_Nika.Controllers;

public class ReportsController : Controller
{
    private readonly AdventureWorksLTDbContext _context;

    public ReportsController(AdventureWorksLTDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index(string reportType = "YearMonth")
    {
        ViewBag.ReportType = reportType;

        switch (reportType)
        {
            case "YearMonth":
                var yearMonthReport = await _context.SalesOrderHeaders
                    .GroupBy(s => new {
                        Year = s.OrderDate.Year,
                        Month = s.OrderDate.Month
                    })
                    .Select(g => new SalesByYearMonth
                    {
                        Year = g.Key.Year,
                        Month = g.Key.Month,
                        TotalSales = g.Sum(s => s.TotalDue)
                    })
                    .OrderByDescending(r => r.Year)
                    .ThenByDescending(r => r.Month)
                    .ToListAsync();
                return View(yearMonthReport);

            case "Products":
                var productsReport = await _context.SalesOrderDetails
                    .Include(s => s.Product)
                    .GroupBy(s => s.Product.Name)
                    .Select(g => new SalesByProduct
                    {
                        ProductName = g.Key,
                        TotalSales = g.Sum(s => s.LineTotal)
                    })
                    .OrderByDescending(r => r.TotalSales)
                    .ToListAsync();
                return View(productsReport);

            case "Categories":
                var categoriesReport = await _context.SalesOrderDetails
                    .Include(s => s.Product)
                    .Include(s => s.Product.ProductCategory)
                    .GroupBy(s => s.Product.ProductCategory.Name)
                    .Select(g => new SalesByCategory
                    {
                        CategoryName = g.Key,
                        TotalSales = g.Sum(s => s.LineTotal)
                    })
                    .OrderByDescending(r => r.TotalSales)
                    .ToListAsync();
                return View(categoriesReport);

            case "CustomerYear":
                var customerYearReport = await _context.SalesOrderHeaders
                    .Include(s => s.Customer)
                    .GroupBy(s => new {
                        Customer = s.Customer.FirstName + " " + s.Customer.LastName,
                        Year = s.OrderDate.Year
                    })
                    .Select(g => new SalesByCustomerYear
                    {
                        CustomerName = g.Key.Customer,
                        Year = g.Key.Year,
                        TotalSales = g.Sum(s => s.TotalDue)
                    })
                    .OrderByDescending(r => r.Year)
                    .ThenByDescending(r => r.TotalSales)
                    .ToListAsync();
                return View(customerYearReport);

            case "City":
                var cityReport = await _context.SalesOrderHeaders
                    .Include(s => s.Customer)
                    .GroupBy(s => s.ShipToAddress.City)
                    .Select(g => new SalesByCity
                    {
                        City = g.Key,
                        TotalSales = g.Sum(s => s.TotalDue)
                    })
                    .OrderByDescending(r => r.TotalSales)
                    .ToListAsync();
                return View(cityReport);

            case "Top10Customers":
                var top10Customers = await _context.SalesOrderHeaders
                    .Include(s => s.Customer)
                    .GroupBy(s => s.Customer.FirstName + " " + s.Customer.LastName)
                    .Select(g => new TopCustomers
                    {
                        CustomerName = g.Key,
                        TotalSales = g.Sum(s => s.TotalDue)
                    })
                    .OrderByDescending(r => r.TotalSales)
                    .Take(10)
                    .ToListAsync();
                return View(top10Customers);

            case "Top10CustomersByYear":
                var top10CustomersByYear = await _context.SalesOrderHeaders
                    .Include(s => s.Customer)
                    .GroupBy(s => new {
                        Customer = s.Customer.FirstName + " " + s.Customer.LastName,
                        Year = s.OrderDate.Year
                    })
                    .Select(g => new TopCustomersByYear
                    {
                        CustomerName = g.Key.Customer,
                        Year = g.Key.Year,
                        TotalSales = g.Sum(s => s.TotalDue)
                    })
                    .OrderByDescending(r => r.Year)
                    .ThenByDescending(r => r.TotalSales)
                    .ToListAsync();
                return View(top10CustomersByYear);

            case "Top10Products":
                var top10Products = await _context.SalesOrderDetails
                    .Include(s => s.Product)
                    .GroupBy(s => s.Product.Name)
                    .Select(g => new TopProducts
                    {
                        ProductName = g.Key,
                        TotalSales = g.Sum(s => s.LineTotal)
                    })
                    .OrderByDescending(r => r.TotalSales)
                    .Take(10)
                    .ToListAsync();
                return View(top10Products);

            case "Top10ProductsByProfit":
                var top10ProductsByProfit = await _context.SalesOrderDetails
                    .Include(s => s.Product)
                    .GroupBy(s => s.Product.Name)
                    .Select(g => new TopProductsByProfit
                    {
                        ProductName = g.Key,
                        TotalProfit = g.Sum(s => s.LineTotal - (s.OrderQty * s.Product.StandardCost))
                    })
                    .OrderByDescending(r => r.TotalProfit)
                    .Take(10)
                    .ToListAsync();
                return View(top10ProductsByProfit);

            default:
                return View(new List<object>());
        }
    }
}