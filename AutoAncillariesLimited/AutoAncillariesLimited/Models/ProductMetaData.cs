using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Services.Description;

namespace AutoAncillariesLimited.Models
{
  public class ProductMetaData
  {
    [Required(ErrorMessage = "This field is required!")]
    [MinLength(6, ErrorMessage = "Product name too short!")]
    [MaxLength(100, ErrorMessage = "Product name too long!")]
    public string Name { get; set; }
    
    public decimal Price { get; set; }
  }

  [MetadataType(typeof(ProductMetaData))]
  public partial class Product
  {

  }
}