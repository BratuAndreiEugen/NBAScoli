namespace NBAScoli.model;

public class Elev : Entity<long>
{
    public string Nume { get; set; }
    
    public string Scoala { get; set; }

    public Elev(long id, string nume, string scoala)
    {
        Id = id;
        Nume = nume;
        Scoala = scoala;
    }

    public override string ToString()
    {
        return Id + " / " + Nume + " / " + Scoala;
    }
}