namespace RnM.Api.Models
{
    public class Location
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Type { get; set; }
        public string? Dimension { get; set; }
        public ICollection<Character> Residents { get; } = new List<Character>();
        public string? Url {  get; set; }
        public DateTime Created { get; set; }
    }
}
