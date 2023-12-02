using System.ComponentModel.DataAnnotations;

namespace RnM.Api.Models
{
    public enum CharacterGender
    {
        Female,
        Male,
        Genderless,
        Unknown
    }

    public enum CharacterStatus
    {
        Alive,
        Dead,
        Unknown
    }

    public class Character
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
        public CharacterStatus Status { get; set; }
        public string? Species {  get; set; }
        public string? Type { get; set; }
        public CharacterGender Gender { get; set; }
        public string? Origin { get; set; }
        public Location Location { get; set; }
        public string? Image {  get; set; }
        public ICollection<Episode> Episodes { get; } = new List<Episode>();
        public string? Url { get; set; }
        public DateTime Created { get; set; }

    }
}
