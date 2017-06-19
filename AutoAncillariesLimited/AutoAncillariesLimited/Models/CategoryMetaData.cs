using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AutoAncillariesLimited.Models
{
  public class CategoryMetaData
  {
    [Required]
    [MinLength(6)]
    [MaxLength(100)]
    public string Name { get; set; }
  }

  [MetadataType(typeof(CategoryMetaData))]
  public partial class Category
  {
    
  }
}