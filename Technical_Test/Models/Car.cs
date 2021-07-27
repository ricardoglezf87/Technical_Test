using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Technical_Test.Models
{
    public class Car
    {

        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; }

        [Required(ErrorMessage = "Number plate is required")]
        public string NumberPlate { get; set; }

        [Required(ErrorMessage ="Brand is required")]
        public string Brand_id { get; set; }

        [Required(ErrorMessage = "Model is required")]
        public string Model_id { get; set; }

        [Required(ErrorMessage = "Price is required")]
        [DataType(DataType.Currency, ErrorMessage = "The price should be a number"), DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = true)]
        public double? Price { get; set; }

    }
}
