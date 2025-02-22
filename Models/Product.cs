﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Final_Project_Nika.Models;

/// <summary>
/// Products sold or used in the manfacturing of sold products.
/// </summary>
public partial class Product
{
    /// <summary>
    /// Primary key for Product records.
    /// </summary>
    [Key]
    public int ProductId { get; set; }

    /// <summary>
    /// Name of the product.
    /// </summary>
    [Required]
    [StringLength(50, ErrorMessage = "The Name cannot exceed 50 characters.")]
    public string Name { get; set; }

    /// <summary>
    /// Unique product identification number.
    /// </summary>
    [Required]
    [StringLength(25, ErrorMessage = "The Product Number cannot exceed 25 characters.")]
    public string ProductNumber { get; set; }

    /// <summary>
    /// Product color.
    /// </summary>
    [StringLength(15, ErrorMessage = "The Color cannot exceed 15 characters.")]

    public string Color { get; set; }

    /// <summary>
    /// Standard cost of the product.
    /// </summary>
    [Required(ErrorMessage = "Standard Cost is required.")]
    [Range(0, double.MaxValue, ErrorMessage = "Standard Cost must be a positive number.")]
    public decimal StandardCost { get; set; }

    /// <summary>
    /// Selling price.
    /// </summary>
    [Required(ErrorMessage = "List Price is required.")]
    [Range(0, double.MaxValue, ErrorMessage = "List Price must be a positive number.")]
    public decimal ListPrice { get; set; }

    /// <summary>
    /// Product size.
    /// </summary>
    [StringLength(5, ErrorMessage = "The Size cannot exceed 5 characters.")]
    public string Size { get; set; }

    /// <summary>
    /// Product weight.
    /// </summary>
    [Range(0.0, 9999.99, ErrorMessage = "The Weight must be a positive value with up to 2 decimal places.")]

    public decimal? Weight { get; set; }

    /// <summary>
    /// Product is a member of this product category. Foreign key to ProductCategory.ProductCategoryID. 
    /// </summary>
    public int? ProductCategoryId { get; set; }

    /// <summary>
    /// Product is a member of this product model. Foreign key to ProductModel.ProductModelID.
    /// </summary>
    public int? ProductModelId { get; set; }

    /// <summary>
    /// Date the product was available for sale.
    /// </summary>
    [Required]
    [DataType(DataType.DateTime)]
    public DateTime SellStartDate { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Date the product was no longer available for sale.
    /// </summary>
    [DataType(DataType.DateTime)]
    public DateTime? SellEndDate { get; set; }

    /// <summary>
    /// Date the product was discontinued.
    /// </summary>
    [DataType(DataType.DateTime)]
    public DateTime? DiscontinuedDate { get; set; }

    /// <summary>
    /// Small image of the product.
    /// </summary>
    public byte[] ThumbNailPhoto { get; set; }

    /// <summary>
    /// Small image file name.
    /// </summary>
    [StringLength(50, ErrorMessage = "The Thumbnail Photo File Name cannot exceed 50 characters.")]
    public string ThumbnailPhotoFileName { get; set; }

    /// <summary>
    /// ROWGUIDCOL number uniquely identifying the record. Used to support a merge replication sample.
    /// </summary>
    [Required]
    public Guid Rowguid { get; set; }

    /// <summary>
    /// Date and time the record was last updated.
    /// </summary>
    [Required]
    [DataType(DataType.DateTime)]
    public DateTime ModifiedDate { get; set; }

    [NotMapped]
    public int OrderCount { get; set; } = 0;

    public virtual ProductCategory ProductCategory { get; set; }

    public virtual ProductModel ProductModel { get; set; }

    public virtual ICollection<SalesOrderDetail> SalesOrderDetails { get; set; } = new List<SalesOrderDetail>();
}