using System;
using System.Collections.Generic;

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

        var equipos = new List<Equipo>();
        var competiciones = new List<Competicion>();
        var partidos = new List<Partido>();

        int idEquipo = 1, idCompeticion = 1, idPartido = 1;

        foreach (var linea in datos)
        {
            var partes = linea.Split(';');
            string e1 = partes[0];
            string e2 = partes[1];
            string resultado = partes[2];
            string competicion = partes[3];
            DateOnly fecha = DateOnly.Parse(partes[4]);

            // Equipo 1
            var eq1 = equipos.Find(x => x.Nombre == e1);
            if (eq1 == null)
            {
                eq1 = new Equipo { Id = idEquipo++, Nombre = e1 };
                equipos.Add(eq1);
            }

            // Equipo 2
            var eq2 = equipos.Find(x => x.Nombre == e2);
            if (eq2 == null)
            {
                eq2 = new Equipo { Id = idEquipo++, Nombre = e2 };
                equipos.Add(eq2);
            }

            // Competición
            var comp = competiciones.Find(x => x.Nombre == competicion);
            if (comp == null)
            {
                comp = new Competicion { Id = idCompeticion++, Nombre = competicion };
                competiciones.Add(comp);
            }

            // Partido
            partidos.Add(new Partido
            {
                Id = idPartido++,
                Equipo1Id = eq1.Id,
                Equipo2Id = eq2.Id,
                CompeticionId = comp.Id,
                Resultado = resultado,
                Fecha = fecha
            });
        }

        // Mostrar resultados
        Console.WriteLine("=== Equipos ===");
        foreach (var e in equipos)
            Console.WriteLine($"{e.Id}: {e.Nombre}");

        Console.WriteLine("\n=== Competiciones ===");
        foreach (var c in competiciones)
            Console.WriteLine($"{c.Id}: {c.Nombre}");

        Console.WriteLine("\n=== Partidos ===");
        foreach (var p in partidos)
            Console.WriteLine($"{p.Id}: {p.Equipo1Id}-{p.Equipo2Id} ({p.Resultado}) {p.Fecha} [{p.CompeticionId}]");
    }
}

