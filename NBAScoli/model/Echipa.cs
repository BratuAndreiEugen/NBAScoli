namespace NBAScoli.model;

public class Echipa : Entity<long>
{
    public String Nume { get; set; }

    public Echipa(long id, string nume)
    {
        Id = id;
        Nume = nume;
    }

    public override string ToString()
    {
        return Id + " / " + Nume;
    }
}