using System.Collections.Generic;
using System.Text.RegularExpressions;

public static class CSVParser
{
    public static List<string[]> Parse(string csvContent)
    {
        List<string[]> parsedData = new List<string[]>();

        string[] lines = csvContent.Split('\n');
        string pattern = @"(?:^|,)(\""(?:[^\\""]+|\""\"")*\""|[^,]*)";

        foreach (string line in lines)
        {
            if (!string.IsNullOrEmpty(line.Trim()))
            {
                MatchCollection matches = Regex.Matches(line, pattern);
                List<string> fields = new List<string>();

                foreach (Match match in matches)
                {
                    fields.Add(match.Value.TrimStart(',').Trim(' ', '\"').Replace("\"\"", "\""));
                }

                parsedData.Add(fields.ToArray());
            }
        }

        return parsedData;
    }
}
