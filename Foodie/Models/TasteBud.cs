using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Foodie
{


    public partial class TasteBudMetaData
    {
        public int FoodId { get; set; }
        [Required]
        public string FoodName { get; set; }
        [Required]
        public string Country { get; set; }
        [Required]
        public string Info { get; set; }
        [Required]
        public string Image { get; set; }
    }
    [MetadataType(typeof(TasteBudMetaData))]
    public partial class TasteBud
    {
    }
}