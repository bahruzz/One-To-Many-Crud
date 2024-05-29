using Asp.NetIntro_MVC.Models;
using NuGet.Protocol.Plugins;
using System.ComponentModel.DataAnnotations;

namespace Asp.NetIntro_MVC.ViewModels.Products
{
    public class ProductCreateVM
    {
        [Required(ErrorMessage = "This input can't be empty")]
        public string Name { get; set; }
        [Required(ErrorMessage = "This input can't be empty")]
        public string  Description { get; set; }

        [Required(ErrorMessage = "This input can't be empty")]
        public string Price { get; set; }
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "This input can't be empty")]
        public List<IFormFile> Images { get; set; }
    }
}
