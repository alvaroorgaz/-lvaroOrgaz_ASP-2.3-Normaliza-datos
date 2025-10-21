using System;
using System.Collections.Generic;
using System.Linq;

class Equipo
{
    public int Id { get; set; }
    public string? Nombre { get; set; }
}

class Competicion
{
    public int Id { get; set; }
    public string? Nombre { get; set; }
}

class Partido
{
    public int Id { get; set; }
    public int Equipo1Id { get; set; }
    public int Equipo2Id { get; set; }
    public int CompeticionId { get; set; }
    public string? Resultado { get; set; }
    public DateOnly Fecha { get; set; }
}

class Program
{
    static void Main()
    {
        List<string> datos = new List<string> {
            "Real Madrid;Barcelona;2-1;Liga;2025-10-12",
            "Atlético de Madrid;Sevilla;1-0;Liga;2025-10-13",
            "Barcelona;Valencia;3-2;Copa del Rey;2025-10-14",
            "Sevilla;Real Madrid;0-2;Liga;2025-10-15",
            "Valencia;Atlético de Madrid;1-1;Copa del Rey;2025-10-16",
            "Real Madrid;Atlético de Madrid;2-2;Liga;2025-10-17",
            "Barcelona;Sevilla;4-0;Liga;2025-10-18",
            "Valencia;Real Madrid;0-1;Copa del Rey;2025-10-19",
            "Atlético de Madrid;Barcelona;1-3;Liga;2025-10-20",
            "Sevilla;Valencia;2-2;Copa del Rey;2025-10-21"
        };

        // Transformamos los datos a objetos temporales
        var partidosTemp = datos.Select(l => {
            var p = l.Split(';');
            return new
            {
                Equipo1 = p[0],
                Equipo2 = p[1],
                Resultado = p[2],
                Competicion = p[3],
                Fecha = DateOnly.Parse(p[4])
            };
        }).ToList();

        // Equipos únicos
        var equipos = partidosTemp
            .SelectMany(p => new[] { p.Equipo1, p.Equipo2 })
            .Distinct()
            .Select((nombre, i) => new Equipo { Id = i + 1, Nombre = nombre })
            .ToList();

        // Competiciones únicas
        var competiciones = partidosTemp
            .Select(p => p.Competicion)
            .Distinct()
            .Select((nombre, i) => new Competicion { Id = i + 1, Nombre = nombre })
            .ToList();

        // Partidos con referencias por Id
        var partidos = partidosTemp.Select((p, i) => new Partido
        {
            Id = i + 1,
            Equipo1Id = equipos.First(e => e.Nombre == p.Equipo1).Id,
            Equipo2Id = equipos.First(e => e.Nombre == p.Equipo2).Id,
            CompeticionId = competiciones.First(c => c.Nombre == p.Competicion).Id,
            Resultado = p.Resultado,
            Fecha = p.Fecha
        }).ToList();

        // === Mostrar resultados ===
        Console.WriteLine("=== EQUIPOS ===");
        foreach (var e in equipos)
            Console.WriteLine($"{e.Id}: {e.Nombre}");

        Console.WriteLine("\n=== COMPETICIONES ===");
        foreach (var c in competiciones)
            Console.WriteLine($"{c.Id}: {c.Nombre}");

        Console.WriteLine("\n=== PARTIDOS ===");
        foreach (var p in partidos)
            Console.WriteLine($"{p.Id}: {p.Equipo1Id}-{p.Equipo2Id} ({p.Resultado}) {p.Fecha} [{p.CompeticionId}]");
    }
}

