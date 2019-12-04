#define k26
#if k26
using ShopApp.Entities;
#endif
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopApp.WebUI.Models
{
    public class CategoryModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
#if k26
        public List<Product> Products { get; set; } 
#endif
    }
}
