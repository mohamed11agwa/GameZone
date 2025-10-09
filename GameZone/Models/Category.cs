namespace GameZone.Models
{
    public class Category:BaseEntity
    {
       
        // Navigation property
        public virtual ICollection<Game> Games { get; set; } = new HashSet<Game>();
    }
}
