using NBAScoli.model;
using NBAScoli.model.validators;
using NBAScoli.service;
using Npgsql;

namespace NBAScoli.repository;

public class EchipeDbRepository : DbRepository<long, Echipa>
{
    public EchipeDbRepository(IValidator<Echipa> validator, string host, string username, string password, string database) : base(validator, host, username, password, database) {}

    
    public Echipa findOne(long id)
    {
        string cs = "Host=localhost;Username=postgres;Password=Andreas14321;Database=MAPFacultativ";
        using var con = new NpgsqlConnection(cs);
        con.Open();

        string sql = "select nume from echipe where id = @id";
        using var cmd = new NpgsqlCommand(sql, con);
        cmd.Parameters.AddWithValue("id", id);
        cmd.Prepare();
        using NpgsqlDataReader rdr = cmd.ExecuteReader();
        int ok = 0;
        while (rdr.Read())
        {
            ok = 1;
            return new Echipa(id, rdr.GetString(0));
        }

        throw new ServiceException("Nu am gasit");
    }
    
    public Echipa findOneByName(string nume)
    {
        string cs = "Host=localhost;Username=postgres;Password=Andreas14321;Database=MAPFacultativ";
        using var con = new NpgsqlConnection(cs);
        con.Open();

        string sql = "select id, nume from echipe where nume = @name";
        using var cmd = new NpgsqlCommand(sql, con);
        cmd.Parameters.AddWithValue("name", nume);
        cmd.Prepare();
        using NpgsqlDataReader rdr = cmd.ExecuteReader();
        int ok = 0;
        while (rdr.Read())
        {
            ok = 1;
            return new Echipa(rdr.GetInt32(0), nume);
        }

        throw new ServiceException("Nu am gasit");
    }
}