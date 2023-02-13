using NBAScoli.model;
using NBAScoli.model.validators;
using Npgsql;

namespace NBAScoli.repository;

public class JucatoriDbRepository : DbRepository<long, Jucator>
{
    public JucatoriDbRepository(IValidator<Jucator> validator, string host, string username, string password, string database) : base(validator, host, username, password, database){}

    public List<Jucator> findPlayersForTeam(Echipa echipa)
    {
        List<Jucator> l = new List<Jucator>();

        string cs = "Host=localhost;Username=postgres;Password=Andreas14321;Database=MAPFacultativ";
        using var con = new NpgsqlConnection(cs);
        con.Open();

        string sql =
            "select e.id, e.nume, s.nume from elevi e inner join jucatori j on e.id = j.elevid inner join scoli s on s.id = e.scoalaid where j.echipaid = @eid";
        using var cmd = new NpgsqlCommand(sql, con);
        cmd.Parameters.AddWithValue("eid", echipa.Id);
        cmd.Prepare();
        using NpgsqlDataReader rdr = cmd.ExecuteReader();

        while (rdr.Read())
        {
            l.Add(new Jucator(rdr.GetInt32(0), rdr.GetString(1), rdr.GetString(2), echipa));
        }
        return l;
    }

    public List<JucatorActiv> findPlayersInMatch(Echipa echipa, Meci meci)
    {
        List <JucatorActiv> l = new List<JucatorActiv>();
        string cs = "Host=localhost;Username=postgres;Password=Andreas14321;Database=MAPFacultativ";
        using var con = new NpgsqlConnection(cs);
        con.Open();

        string sql =
            "select e.id, ja.puncte, ja.tip, e.nume, e.scoalaid from elevi e inner join jucatoriactivi ja on e.id = ja.jucatorid inner join jucatori j on e.id = j.elevid where ja.meciid = @idMeci and j.echipaid = @idEchipa";
        using var cmd = new NpgsqlCommand(sql, con);
        cmd.Parameters.AddWithValue("idMeci", meci.Id);
        cmd.Parameters.AddWithValue("idEchipa", echipa.Id);
        cmd.Prepare();
        using NpgsqlDataReader rdr = cmd.ExecuteReader();
        
        while(rdr.Read())
        {
            if(rdr.GetString(2) == "Participant")
                l.Add(new JucatorActiv(rdr.GetInt32(0), meci.Id, rdr.GetInt32(1), TipJucator.Participant));
            else
            {
                l.Add(new JucatorActiv(rdr.GetInt32(0), meci.Id, rdr.GetInt32(1), TipJucator.Rezerva));
            }
        }
        
        
        return l;
    }

}