namespace kolosNauka.Entities;

public class PCComponents
{
    public int PCId { get; set; }
    public string ComponentCode { get; set; }
    public int Amount { get; set; }

    public virtual PC PC { get; set; }
    public virtual Components Component { get; set; }
}