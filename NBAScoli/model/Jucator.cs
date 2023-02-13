namespace NBAScoli.model;

public class Jucator : Elev
{
    public Echipa Echipa { get; set; }

    public Jucator(long id, string nume, string scoala, Echipa echipa) : base(id, nume, scoala)
    {
        Echipa = echipa;
    }

    public override string ToString()
    {
        return base.ToString() + " / " + Echipa.Nume;
    }
}