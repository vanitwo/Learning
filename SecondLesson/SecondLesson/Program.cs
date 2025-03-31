using System.Text.Json;

class Program
{
    public static async Task Main()
    {
        var url = "https://raw.githubusercontent.com/navferty/dotnet-learning/master/data/JSON_sample_1.json";

                   
        var deals = await LoadJsonFromUrl<List<Deal>>(url);            
        

        var number = GetNumbersOfDeals(deals);
        Console.WriteLine(string.Join(", ", number));
        var sums = GetSumsByMonth(deals);
        foreach (var deal in sums) 
        {
            Console.WriteLine($"{deal.Month:MMMM yyyy} - Сумма: {deal.Sum}"); 
        }
    }

    private static IList<string> GetNumbersOfDeals(IEnumerable<Deal> deals)
    {
        return deals
            .Where(d => d.Sum > 100)
            .OrderBy(d => d.Date)
            .Take(5)
            .OrderByDescending(d => d.Sum)
            .Select(d => d.Id)
            .ToList();
    }

    private static IList<SumByMonth> GetSumsByMonth(IEnumerable<Deal> deals)
    {
        return deals        
        .GroupBy(d => new { Year = d.Date.Year, Month = d.Date.Month })
        .Select(g => new SumByMonth(            
            new DateTime(g.Key.Year, g.Key.Month, 1),
            g.Sum(d => d.Sum)
        ))
        .ToList();
    }

    public record SumByMonth(DateTime Month, int Sum);    

    public static async Task<T> LoadJsonFromUrl<T>(string url)
    {
        using var httpClient = new HttpClient();

        try
        {
            var json = await httpClient.GetStringAsync(url);
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true // Игнорировать регистр свойств
            };
            return JsonSerializer.Deserialize<T>(json, options);
        }
        catch (HttpRequestException ex)
        {
            throw new Exception($"Ошибка запроса: {ex.Message}");
        }
        catch (JsonException ex)
        {
            throw new Exception($"Ошибка JSON: {ex.Message}");
        }
    }
}

public class Deal
{
    public int Sum { get; set; }
    public string Id { get; set; }
    public DateTime Date { get; set; }
}