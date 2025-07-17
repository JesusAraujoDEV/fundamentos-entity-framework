namespace fundamentos_entity_framework // Asegúrate de que coincida con el namespace de tu proyecto
{
    using System;
    using System.IO;

    public static class DotEnv
    {
        public static void Load(string filePath)
        {
            if (!File.Exists(filePath))
                return; // Si el archivo .env no existe, simplemente no hacemos nada.

            foreach (var line in File.ReadAllLines(filePath))
            {
                // Ignorar líneas vacías o comentarios
                if (string.IsNullOrWhiteSpace(line) || line.TrimStart().StartsWith("#"))
                    continue;

                var parts = line.Split('=', StringSplitOptions.RemoveEmptyEntries);

                if (parts.Length != 2)
                {
                    // Puedes añadir un log si una línea no tiene el formato KEY=VALUE
                    // Console.WriteLine($"Advertencia: Línea en .env con formato inválido: {line}");
                    continue;
                }

                Environment.SetEnvironmentVariable(parts[0].Trim(), parts[1].Trim());
            }
        }
    }
}