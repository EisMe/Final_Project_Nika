public class SalesByYearMonth
{
    public int Year { get; set; }
    public int Month { get; set; }
    public decimal TotalSales { get; set; }
}

public class SalesByProduct
{
    public string ProductName { get; set; }
    public decimal TotalSales { get; set; }
}

public class SalesByCategory
{
    public string ProductName { get; set; }
    public string CategoryName { get; set; }
    public decimal TotalSales { get; set; }
}

public class SalesByCustomerYear
{
    public string CustomerName { get; set; }
    public int Year { get; set; }
    public decimal TotalSales { get; set; }
}

public class SalesByCity
{
    public string CategoryName { get; set; }
    public string ProductName { get; set; }
    public string City { get; set; }
    public decimal TotalSales { get; set; }
}

public class TopCustomers
{
    public string CustomerName { get; set; }
    public decimal TotalSales { get; set; }
}

public class TopCustomersByYear
{
    public string CustomerName { get; set; }
    public int Year { get; set; }
    public decimal TotalSales { get; set; }
}

public class TopProducts
{
    public string ProductName { get; set; }
    public decimal TotalSales { get; set; }
}

public class TopProductsByProfit
{
    public string ProductName { get; set; }
    public decimal TotalProfit { get; set; }
}

public class TopProductsByYear
{
    public string ProductName { get; set; }
    public int Year { get; set; }
    public decimal TotalSales { get; set; }
}