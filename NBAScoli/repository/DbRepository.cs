using NBAScoli.model;
using NBAScoli.model.validators;

namespace NBAScoli.repository;

public class DbRepository<ID, E> : IRepository<ID, E> where E : Entity<ID>
{
    protected IValidator<E> validator;
    protected String Host { get; set; }
    protected String Username { get; set; }
    protected string Password { get; set; }
    protected string Database { get; set; }

    public DbRepository(IValidator<E> validator, string host, string username, string password, string database)
    {
        this.validator = validator;
        Host = host;
        Username = username;
        Password = password;
        Database = database;
    }

    public E FindOne(ID id)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<E> FindAll()
    {
        throw new NotImplementedException();
    }

    public E Save(E entity)
    {
        throw new NotImplementedException();
    }

    public E Delete(ID id)
    {
        throw new NotImplementedException();
    }

    public E Update(E entity)
    {
        throw new NotImplementedException();
    }
}