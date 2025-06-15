using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

public class Program
{
    public static void Main()
    {
        CultureInfo.CurrentCulture = new CultureInfo("uk-UA");
        Console.OutputEncoding = Encoding.UTF8;

        Console.WriteLine("--- Завдання 1: Заміна '0' на '1' і навпаки ---");
        string initialString = "1110001010101000111";
        int startPosition = 5;
        Console.WriteLine($"Початковий рядок: {initialString}");
        Console.WriteLine($"Позиція, з якої починається заміна: {startPosition}");
        string resultString = ReplaceZerosAndOnes(initialString, startPosition);
        Console.WriteLine($"Результат:         {resultString}\n");

        Console.WriteLine("--- Завдання 2: Кількість днів до вказаної дати ---");
        CountDaysUntilDate("31.12.2025");
        CountDaysUntilDate("01.01.2024");
        Console.WriteLine();

        Console.WriteLine("--- Завдання 3: Аналіз рядка з трьома датами ---");
        string datesString = "25.08.2023, 15.04.2021, 01.03.2024";
        AnalyzeMultipleDates(datesString);
    }

    public static string ReplaceZerosAndOnes(string input, int startIndex)
    {
        if (startIndex < 0 || startIndex >= input.Length)
        {
            return "Помилка: початкова позиція за межами рядка.";
        }

        StringBuilder sb = new StringBuilder(input);
        for (int i = startIndex; i < sb.Length; i++)
        {
            if (sb[i] == '0')
            {
                sb[i] = '1';
            }
            else if (sb[i] == '1')
            {
                sb[i] = '0';
            }
        }
        return sb.ToString();
    }

    public static void CountDaysUntilDate(string dateStr)
    {
        Console.Write($"Аналіз дати '{dateStr}': ");
        if (DateTime.TryParse(dateStr, out DateTime targetDate))
        {
            DateTime today = DateTime.Today;
            if (targetDate > today)
            {
                TimeSpan difference = targetDate - today;
                Console.WriteLine($"До цієї дати залишилося {difference.Days} днів.");
            }
            else if (targetDate < today)
            {
                Console.WriteLine("Цей день вже минув.");
            }
            else
            {
                Console.WriteLine("Цей день сьогодні!");
            }
        }
        else
        {
            Console.WriteLine("Некоректний формат дати.");
        }
    }

    public static void AnalyzeMultipleDates(string input)
    {
        Console.WriteLine($"Вхідний рядок: \"{input}\"");

        List<DateTime> dates = input.Split(',')
            .Select(s => s.Trim())
            .Select(s => DateTime.TryParse(s, out var dt) ? dt : (DateTime?)null)
            .Where(d => d.HasValue)
            .Select(d => d.Value)
            .ToList();

        if (dates.Count == 0)
        {
            Console.WriteLine("Не вдалося знайти жодної коректної дати в рядку.");
            return;
        }

        int earliestYear = dates.Min(d => d.Year);
        Console.WriteLine($"a) Рік з найменшим номером: {earliestYear}");

        var springDates = dates.Where(d => d.Month >= 3 && d.Month <= 5);
        if (springDates.Any())
        {
            Console.WriteLine($"b) Весняні дати: {string.Join(", ", springDates.Select(d => d.ToShortDateString()))}");
        }
        else
        {
            Console.WriteLine("b) Весняних дат не знайдено.");
        }

        DateTime latestDate = dates.Max();
        Console.WriteLine($"c) Сама пізня дата: {latestDate.ToShortDateString()}");
    }
}
