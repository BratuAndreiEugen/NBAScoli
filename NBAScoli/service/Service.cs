using NBAScoli.model;
using NBAScoli.repository;

namespace NBAScoli.service;

delegate List<Meci> FilterDate(DateOnly start, DateOnly end);

public class Service
{
    protected JucatoriDbRepository repoJucatori;
    protected MeciuriDbRepository repoMeciuri;
    protected EchipeDbRepository repoEchipe;
    private FilterDate f1;
    private FilterDate f2;

    public Service(JucatoriDbRepository repoJucatori, MeciuriDbRepository repoMeciuri, EchipeDbRepository repoEchipe)
    {
        this.repoJucatori = repoJucatori;
        this.repoMeciuri = repoMeciuri;
        this.repoEchipe = repoEchipe;
        f1 = new FilterDate(repoMeciuri.findFromPeriod);
        f2 = new FilterDate(repoMeciuri.findOutOfPeriod);

    }

    public List<Jucator> getPlayersForTeam(string numeEchipa)
    {
        
        Echipa e = repoEchipe.findOneByName(numeEchipa);
        return repoJucatori.findPlayersForTeam(e);
    }

    public List<JucatorActiv> getPlayersFromMatch(string numeEchipa, long idMeci)
    {
        Echipa e = repoEchipe.findOneByName(numeEchipa);
        Meci m = repoMeciuri.findOne(idMeci);

        return repoJucatori.findPlayersInMatch(e, m);
    }

    public List<Meci> getMatchesFromPeriod(string start, string end)
    {
        DateOnly s = DateOnly.ParseExact(start, "yyyy-MM-dd");
        DateOnly e = DateOnly.ParseExact(end, "yyyy-MM-dd");

        return f1(s, e);
    }

    public List<Meci> getMatchesNotInPeriod(string start, string end)
    {
        DateOnly s = DateOnly.ParseExact(start, "yyyy-MM-dd");
        DateOnly e = DateOnly.ParseExact(end, "yyyy-MM-dd");

        return f2(s, e);
    }

    public Tuple<Meci, int, int> getScoreForMatch(long idMeci)
    {
        Meci m = repoMeciuri.findOne(idMeci);
        return repoMeciuri.getScore(m, m.Echipa1, m.Echipa2);
    }

}