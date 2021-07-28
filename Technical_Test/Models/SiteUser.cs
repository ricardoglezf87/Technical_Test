using AspNetCore.Identity.Mongo.Model;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Technical_Test.Models
{
    [BsonIgnoreExtraElements]
    public class SiteUser : MongoUser
    {
        [Required(ErrorMessage = "Company is required")]
        [Display(Name = "Company")]
        public string Company { get; set; }
    }
}
