using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace File_Wizard.Infrastructure
{
    internal static class DotEnv
    {
        private const string FileName = ".env";

        private static readonly Lazy<IReadOnlyDictionary<string, string>> Values = new(Load, true);

        public static string GetString(string key, string fallback)
        {
            if (Values.Value.TryGetValue(key, out var value) && !string.IsNullOrWhiteSpace(value))
            {
                return value;
            }

            return fallback;
        }

        public static int GetInt(string key, int fallback)
        {
            string rawValue = GetString(key, fallback.ToString(CultureInfo.InvariantCulture));

            return int.TryParse(rawValue, NumberStyles.Integer, CultureInfo.InvariantCulture, out int parsed)
                ? parsed
                : fallback;
        }

        private static IReadOnlyDictionary<string, string> Load()
        {
            string? envFilePath = FindEnvFile();
            var values = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

            if (envFilePath is null)
            {
                return values;
            }

            foreach (string line in File.ReadAllLines(envFilePath))
            {
                string trimmed = line.Trim();

                if (trimmed.Length == 0 || trimmed.StartsWith("#", StringComparison.Ordinal))
                {
                    continue;
                }

                if (trimmed.StartsWith("export ", StringComparison.OrdinalIgnoreCase))
                {
                    trimmed = trimmed[7..].TrimStart();
                }

                int equalsIndex = trimmed.IndexOf('=');

                if (equalsIndex <= 0)
                {
                    continue;
                }

                string key = trimmed[..equalsIndex].Trim();
                string value = trimmed[(equalsIndex + 1)..].Trim();

                if (value.Length >= 2)
                {
                    bool doubleQuoted = value[0] == '"' && value[^1] == '"';
                    bool singleQuoted = value[0] == '\'' && value[^1] == '\'';

                    if (doubleQuoted || singleQuoted)
                    {
                        value = value[1..^1];
                    }
                }

                if (key.Length > 0)
                {
                    values[key] = value;
                }
            }

            return values;
        }

        private static string? FindEnvFile()
        {
            var directory = new DirectoryInfo(AppContext.BaseDirectory);

            while (directory is not null)
            {
                string candidatePath = Path.Combine(directory.FullName, FileName);

                if (File.Exists(candidatePath))
                {
                    return candidatePath;
                }

                directory = directory.Parent;
            }

            return null;
        }
    }
}