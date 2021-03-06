using Microsoft.AspNetCore.Authorization;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Technical_Test.Models
{
    [BsonIgnoreExtraElements]
    public class Car: ICollection
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; }

        public string User_id { get; set; }

        [Required(ErrorMessage = "Number plate is required")]
        public string NumberPlate { get; set; }

        [Required(ErrorMessage ="Brand is required")]
        public string Brand_id { get; set; }

        [Required(ErrorMessage = "Model is required")]
        public string Model_id { get; set; }

        [Required(ErrorMessage = "Price is required")]
        [DataType(DataType.Currency, ErrorMessage = "The price should be a number")]
        public double? Price { get; set; }

        public string Brand_Descrip { get; set; }

        public String Model_Descrip { get; set; }

        public double? PriceIVA { get; set; }

    }
}
