namespace NBAScoli.model;

public enum TipJucator
{
    Rezerva, Participant
}

public class JucatorActiv
{
    public long IdJucator { get; set; }
    
    public long IdMeci { get; set; }
    
    public long Puncte { get; set; }
    
    public TipJucator Tip { get; set; }

    public JucatorActiv(long idJucator, long idMeci, long puncte, TipJucator tip)
    {
        IdJucator = idJucator;
        IdMeci = idMeci;
        Puncte = puncte;
        Tip = tip;
    }

    public override string ToString()
    {
        return IdJucator + " / " + IdMeci + " / " + Puncte + " / " + Tip;
    }
}