using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DataLayer.Abstraction.Entityes
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string Login { get; set; }

        [JsonIgnore]
        public string Password { get; set; }
        [JsonIgnore]
        public List<RefreshToken> RefreshTokens { get; set; }
    }
}
