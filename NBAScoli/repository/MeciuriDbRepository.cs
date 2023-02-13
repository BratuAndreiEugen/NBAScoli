using NBAScoli.model;
using NBAScoli.model.validators;
using NBAScoli.service;
using Npgsql;
using NpgsqlTypes;

namespace NBAScoli.repository;



public class MeciuriDbRepository : DbRepository<long, Meci>
{
    private EchipeDbRepository repoEchipe;

    public MeciuriDbRepository(EchipeDbRepository repo, IValidator<Meci> validator, string host, string username, string password,
        string database) : base(validator, host, username, password, database)
    {
        this.repoEchipe = repo;
    }

    public Meci findOne(long id)
    {
        string cs = "Host=localhost;Username=postgres;Password=Andreas14321;Database=MAPFacultativ";
        using var con = new NpgsqlConnection(cs);
        con.Open();

        string sql = "select echipa1, echipa2, data from meciuri where id = @idMeci";
        using var cmd = new NpgsqlCommand(sql, con);
        cmd.Parameters.AddWithValue("idMeci", id);
        cmd.Prepare();
        using NpgsqlDataReader rdr = cmd.ExecuteReader();
        int ok = 0;
        while (rdr.Read())
        {
            ok = 1;
            return new Meci(id, repoEchipe.findOne(rdr.GetInt32(0)), repoEchipe.findOne(rdr.GetInt32(1)),
                DateOnly.FromDateTime(rdr.GetDateTime(2)));
        }

        
        throw new ServiceException("Nu am gasit");
    }

    public List<Meci> findFromPeriod(DateOnly start, DateOnly end)
    {
        List<Meci> l = new List<Meci>();
        string cs = "Host=localhost;Username=postgres;Password=Andreas14321;Database=MAPFacultativ";
        using var con = new NpgsqlConnection(cs);
        con.Open();
        
        string sql = "select id, echipa1, echipa2, data from meciuri where data > @s and data < @e";
        using var cmd = new NpgsqlCommand(sql, con);
        DateTime st = new DateTime(start.Year, start.Month, start.Day);
        DateTime en = new DateTime(end.Year, end.Month, end.Day);
        cmd.Parameters.AddWithValue("s", st);
        cmd.Parameters.AddWithValue("e", en);
        cmd.Prepare();
        using NpgsqlDataReader rdr = cmd.ExecuteReader();
        while (rdr.Read())
        {
            l.Add(new Meci(rdr.GetInt32(0), repoEchipe.findOne(rdr.GetInt32(1)), repoEchipe.findOne(rdr.GetInt32(2)),
                DateOnly.FromDateTime(rdr.GetDateTime(3))));
        }

        return l;

    }
    
    public List<Meci> findOutOfPeriod(DateOnly start, DateOnly end)
    {
        List<Meci> l = new List<Meci>();
        string cs = "Host=localhost;Username=postgres;Password=Andreas14321;Database=MAPFacultativ";
        using var con = new NpgsqlConnection(cs);
        con.Open();
        
        string sql = "select id, echipa1, echipa2, data from meciuri where data < @s or data > @e";
        using var cmd = new NpgsqlCommand(sql, con);
        DateTime st = new DateTime(start.Year, start.Month, start.Day);
        DateTime en = new DateTime(end.Year, end.Month, end.Day);
        cmd.Parameters.AddWithValue("s", st);
        cmd.Parameters.AddWithValue("e", en);
        cmd.Prepare();
        using NpgsqlDataReader rdr = cmd.ExecuteReader();
        while (rdr.Read())
        {
            l.Add(new Meci(rdr.GetInt32(0), repoEchipe.findOne(rdr.GetInt32(1)), repoEchipe.findOne(rdr.GetInt32(2)),
                DateOnly.FromDateTime(rdr.GetDateTime(3))));
        }

        return l;

    }    

    public Tuple<Meci, int, int> getScore(Meci meci, Echipa e1, Echipa e2)
    {
        int scorE1 = 0, scorE2 = 0;
        
        string cs = "Host=localhost;Username=postgres;Password=Andreas14321;Database=MAPFacultativ";
        using var con = new NpgsqlConnection(cs);
        con.Open();
        
        string sql =
            "select e.nume, SUM(ja.puncte) from meciuri m inner join echipe e on m.echipa1 = e.id inner join jucatori j on e.id = j.echipaid inner join elevi el on el.id = j.elevid inner join jucatoriactivi ja on ja.jucatorid = el.id and ja.meciid = m.id where m.id = @idMeci group by e.nume";
        
        using var cmd = new NpgsqlCommand(sql, con);
        cmd.Parameters.AddWithValue("idMeci", meci.Id);
        cmd.Prepare();
        using NpgsqlDataReader rdr = cmd.ExecuteReader();
        while (rdr.Read())
        {
            scorE1 = rdr.GetInt32(1);
        }
        rdr.Close();
        
        string sql1 =
            "select e.nume, SUM(ja.puncte) from meciuri m inner join echipe e on m.echipa2 = e.id inner join jucatori j on e.id = j.echipaid inner join elevi el on el.id = j.elevid inner join jucatoriactivi ja on ja.jucatorid = el.id and ja.meciid = m.id where m.id = @idMeci group by e.nume";
        
        using var cmd1 = new NpgsqlCommand(sql1, con);
        cmd1.Parameters.AddWithValue("idMeci", meci.Id);
        cmd1.Prepare();
        using NpgsqlDataReader rdr1 = cmd1.ExecuteReader();
        while (rdr1.Read())
        {
            scorE2 = rdr1.GetInt32(1);
        }

        return new Tuple<Meci, int, int>(meci, scorE1, scorE2);
    }

}