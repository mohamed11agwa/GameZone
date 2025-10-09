namespace GameZone.Models
{
    public class Device:BaseEntity
    {
        [MaxLength(50)]
        public string Icon { get; set; } = string.Empty;

        // Navigation property
        public virtual ICollection<Game> Games { get; set; } = new HashSet<Game>();
    }
}
