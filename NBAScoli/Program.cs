// See https://aka.ms/new-console-template for more information

using System.Security.Cryptography;
using NBAScoli.model;
using NBAScoli.model.validators;
using NBAScoli.repository;
using NBAScoli.service;
using Npgsql;

class Program
{
    private static Service Service;

    static void printOptions()
    {
        Console.WriteLine("1.Afiseaza jucatorii unei echipe date");
        Console.WriteLine("2.Afiseaza jucatorii unei echipe date in cadrul unui meci dat");
        Console.WriteLine("3.Afiseaza toate meciurile dintr-o perioada calendaristica");
        Console.WriteLine("4.Afiseaza scorul unui meci dat");
        Console.WriteLine("5.Iesire din aplicatie");
    }

    static void task1()
    {
        Console.WriteLine("Introduceti numele echipei : ");
        string numeEchipa = Console.ReadLine();
        try
        {
            List<Jucator> l = Service.getPlayersForTeam(numeEchipa);
            l.ForEach(x => Console.WriteLine(x));
        }
        catch (ServiceException s)
        {
            Console.WriteLine(s.Message);
        }
        
        Console.WriteLine();

    }
    
    
    private static void task2()
    {
        Console.WriteLine("Introduceti numele echipei : ");
        string numeEchipa = Console.ReadLine();
        Console.WriteLine("Introuceti ID-ul meciului");
        int input = Int32.Parse(Console.ReadLine());
        
        try
        {
            List<JucatorActiv> l = Service.getPlayersFromMatch(numeEchipa, input);
            l.ForEach(x => Console.WriteLine(x));
        }
        catch (ServiceException s)
        {
            Console.WriteLine(s.Message);
        }
        
        Console.WriteLine();
                
    }
    
    private static void task3()
    {
        Console.WriteLine("Introduceti inceputul perioadei ( yyyy-MM-dd ) : ");
        string start = Console.ReadLine();
        Console.WriteLine("Introuceti sfarsitul perioadei ( yyyy-MM-dd ) : ");
        string end = Console.ReadLine();
        
        try
        {
            List<Meci> l = Service.getMatchesFromPeriod(start, end);
            l.ForEach(Console.WriteLine);
        }
        catch (ServiceException s)
        {
            Console.WriteLine(s.Message);
        }
        
        Console.WriteLine();
        
    }
    
    private static void task4()
    {
        Console.WriteLine("Introuceti ID-ul meciului");
        int input = Int32.Parse(Console.ReadLine());
        
        try
        {
            Tuple<Meci, int, int> t = Service.getScoreForMatch(input);
            Console.WriteLine(t.Item1.Echipa1.Nume + " : " + t.Item2);
            //Console.WriteLine();
            Console.WriteLine(t.Item1.Echipa2.Nume + " : " + t.Item3);
        }
        catch (ServiceException s)
        {
            Console.WriteLine(s.Message);
        }
        
        Console.WriteLine();
    }

    static void loop()
    {
        while (true)
        {
            printOptions();
            int input = Int32.Parse(Console.ReadLine());
            switch (input)
            {
                case 1:
                    task1();
                    break;
                case 2:
                    task2();
                    break;
                case 3:
                    task3();
                    break;
                case 4:
                    task4();
                    break;
                case 5:
                    return;
            }
        }
    }

    


    static void Main(string[] args)
    {
        // Elev e = new Elev(1, "Andrei Bratu", "C.N. Elena Cuza");
        // Console.WriteLine(e);
        // Echipa ec = new Echipa(1, "CB");
        // Console.WriteLine(ec);
        // Echipa ec1 = new Echipa(2, "CNFB");
        // Jucator j = new Jucator(e.Id, e.Nume, e.Scoala, ec);
        // Console.WriteLine(j);
        // Meci m = new Meci(1, ec, ec1, DateOnly.ParseExact("2020-05-03", "yyyy-MM-dd"));
        // Console.WriteLine(m.Data.Month);
        // Console.WriteLine(m);
        // JucatorActiv ja = new JucatorActiv(1, 1, 20, TipJucator.Participant);
        // Console.WriteLine(ja);
        // DateOnly d = DateOnly.ParseExact("2020-05-03", "yyyy-MM-dd");
        // Console.WriteLine(d.Year +"-"+d.Month+"-"+d.Day);

        // var cs = "Host=localhost;Username=postgres;Password=Andreas14321;Database=MAPFacultativ";
        // using var con = new NpgsqlConnection(cs);
        // con.Open();
        //
        // var sql = "SELECT version()";
        // using var cmd = new NpgsqlCommand(sql, con);
        // var version = cmd.ExecuteScalar().ToString();
        // Console.WriteLine($"PostgreSQL version: {version}");
        JucatoriDbRepository repoJucatori = new JucatoriDbRepository(new JucatorValidator(), "localhost", "postgres",
            "Andreas14321", "MAPFacultativ");
        // List<Jucator> l = repoJucatori.findPlayersForTeam(new Echipa(1, "Carol"));
        // l.ForEach(x => Console.WriteLine(x));
        EchipeDbRepository repoEchipe = new EchipeDbRepository(new EchipaValidator(), "localhost", "postgres",
            "Andreas14321", "MAPFacultativ");
        
        MeciuriDbRepository repoMeciuri = new MeciuriDbRepository(repoEchipe, new MeciValidator(), "localhost", "postgres",
            "Andreas14321", "MAPFacultativ");
        
        Service = new Service(repoJucatori, repoMeciuri, repoEchipe);
        loop();

    }
}
