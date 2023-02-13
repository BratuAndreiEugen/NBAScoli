namespace NBAScoli.model;

public class Meci : Entity<long>
{
    public Echipa Echipa1 { get; set; }
    
    public Echipa Echipa2 { get; set; }
    
    public DateOnly Data { get; set; }

    public Meci(long id, Echipa echipa1, Echipa echipa2, DateOnly data)
    {
        Id = id;
        Echipa1 = echipa1;
        Echipa2 = echipa2;
        Data = data;
    }

    public override string ToString()
    {
        return Id + " / " + Echipa1.Nume + " / " + Echipa2.Nume + " / " + Data.ToString();
    }
}