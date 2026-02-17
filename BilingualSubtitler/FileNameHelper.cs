using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections.Generic;

public static class FileNameHelper
{
    private static readonly Dictionary<char, string> TransliterationMap = new()
    {
        ['а'] = "a",
        ['б'] = "b",
        ['в'] = "v",
        ['г'] = "g",
        ['д'] = "d",
        ['е'] = "e",
        ['ё'] = "e",
        ['ж'] = "zh",
        ['з'] = "z",
        ['и'] = "i",
        ['й'] = "j",
        ['к'] = "k",
        ['л'] = "l",
        ['м'] = "m",
        ['н'] = "n",
        ['о'] = "o",
        ['п'] = "p",
        ['р'] = "r",
        ['с'] = "s",
        ['т'] = "t",
        ['у'] = "u",
        ['ф'] = "f",
        ['х'] = "kh",
        ['ц'] = "c",
        ['ч'] = "ch",
        ['ш'] = "sh",
        ['щ'] = "shh",
        ['ъ'] = "",
        ['ы'] = "y",
        ['ь'] = "",
        ['э'] = "e",
        ['ю'] = "yu",
        ['я'] = "ya"
    };

    public static string BuildNewFileName(string originalPath, string description)
    {
        string directory = Path.GetDirectoryName(originalPath)!;
        string fileNameWithoutExt = Path.GetFileNameWithoutExtension(originalPath);

        string transliterated = Transliterate(description);

        // всё кроме букв, цифр и пробелов → пробел
        transliterated = Regex.Replace(transliterated, @"[^a-zA-Z0-9\s]", " ");

        // пробелы → _
        transliterated = Regex.Replace(transliterated.Trim(), @"\s+", "_");

        // убираем повторяющиеся _
        transliterated = Regex.Replace(transliterated, @"_+", "_");

        string newFileName = $"{fileNameWithoutExt}.{transliterated}.mka";

        return Path.Combine(directory, newFileName);
    }

    public static string Transliterate(string text)
    {
        var sb = new StringBuilder();

        foreach (char c in text)
        {
            char lower = char.ToLowerInvariant(c);

            if (TransliterationMap.TryGetValue(lower, out var latin))
            {
                // сохраняем регистр
                if (char.IsUpper(c) && latin.Length > 0)
                {
                    sb.Append(char.ToUpperInvariant(latin[0]));
                    if (latin.Length > 1)
                        sb.Append(latin.Substring(1));
                }
                else
                {
                    sb.Append(latin);
                }
            }
            else
            {
                sb.Append(c);
            }
        }

        return sb.ToString();
    }
}
