namespace kolosNauka.Entities;

public class PC
{
    public int Id { get; set; }
    public string Name { get; set; }
    public float Weight { get; set; }
    public int Warranty { get; set; }
    public DateTime CreatedAt { get; set; }
    public int Stock { get; set; }
    
    public virtual ICollection<PCComponents> PCComponents { get; set; }
}