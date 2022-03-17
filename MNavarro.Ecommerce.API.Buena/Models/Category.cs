using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MNavarro.Ecommerce.API.Buena.Models
{
    public class Category
    {
        //[Required]
        //[JsonPropertyName("Id")]
        public int Id { get; set; }

        public string Name { get; set; }

        [StringLength(1000)]
        public string Description { get; set; }

        [JsonIgnore]
        public IEnumerable<Product> Products { get; set; }
    }
}