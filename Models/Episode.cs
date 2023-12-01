using System.ComponentModel.DataAnnotations;

namespace RnM.Api.Models
{
    public class Episode
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime AirDate { get; set; }
        public string EpisodeCode { get; set; }
        public Character[] Characters { get; set; }
        public string Url { get; set; }
        public DateTime Created {  get; set; }
    }
}
