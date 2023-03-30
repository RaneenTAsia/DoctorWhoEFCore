// See https://aka.ms/new-console-template for more information


using DoctorWho.Db;
using DoctorWho.Db.Repositories;
using DoctorWhoDomain;
using Microsoft.EntityFrameworkCore;
using System.Data;

DoctorWhoDbContext _context = new DoctorWhoDbContext();

//Execute_fnCompanions(2);
void Execute_fnCompanions(int EpisodeId)
{
    var result = _context.Episodes
                .Where(e => e.EpisodeId == EpisodeId)
                .Select
                (a => _context.CompanionsFunctionResult(EpisodeId)
                ).Single().ToString();
    Console.WriteLine(result);
}

//Execute_fnEnemies(2);
void Execute_fnEnemies(int EpisodeId)
{
    var result = _context.Episodes
                .Where(e => e.EpisodeId == EpisodeId)
                .Select
                (a => _context.EnemiesFunctionResult(EpisodeId)
                ).Single().ToString();
    Console.WriteLine(result);
}

//Select_viewEpisodes();

void Select_viewEpisodes()
{
    var result = _context.ViewEpisodes.ToList();

    foreach (var episode in result)
    {
        Console.WriteLine(episode.ToString());
    }
}

//Execute_spSummariseEpisodes();

void Execute_spSummariseEpisodes()
{
    List<Companion> companions= new List<Companion>();
    List<Enemy> enemies = new List<Enemy>();

    var cmd = _context.Database.GetDbConnection().CreateCommand();
    cmd.CommandText = "[dbo].spSummariseEpisodes";

    try
    {
        cmd.Connection.Open();

        var reader = cmd.ExecuteReader();

        while (reader.Read())
        {
            companions.Add(new Companion
            {
                CompanionName = reader.GetString("CompanionName")
            });
        }
        reader.NextResult(); 

        while (reader.Read())
        {
            enemies.Add(new Enemy
            {
                EnemyName = reader.GetString("EnemyName")
            });
        }

        //print results
        Console.WriteLine("Top 3 Companions");
        foreach(var companion in companions)
        {
            Console.WriteLine(companion.CompanionName);
        }

        Console.WriteLine();
        Console.WriteLine("Top 3 Enemies");
        foreach(Enemy enemy in enemies) 
        {
            Console.WriteLine(enemy.EnemyName);
        }

    }
    finally
    {
        cmd.Connection?.Close();
    }
}

