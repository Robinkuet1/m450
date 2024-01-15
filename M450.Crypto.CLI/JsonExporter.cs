using M450.Crypto.DLL;
using Newtonsoft.Json;

namespace M450.Crypto.CLI;

public class JsonExporter
{
    public static string GetJsonString(CryptoCurrency currency, CryptoArguments args, decimal value, string existingJson)
    {
        try
        {
            var deserialized =
                JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, Dictionary<string, decimal>>>>(
                    existingJson);

            var operation = args.Price ? "price" : args.Volume ? "volume" : "fees";
            var date = (args.Date ?? DateOnly.FromDateTime(DateTime.Today)).ToString("dd-MM-yyyy");

            deserialized ??= new Dictionary<string, Dictionary<string, Dictionary<string, decimal>>>();
            if (!deserialized.ContainsKey(currency.ToString()))
            {
                deserialized[currency.ToString()] = new Dictionary<string, Dictionary<string, decimal>>();
            }

            if (!deserialized[currency.ToString()].ContainsKey(operation))
            {
                deserialized[currency.ToString()][operation] = new Dictionary<string, decimal>();
            }

            deserialized[currency.ToString()][operation][date] = value;

            return JsonConvert.SerializeObject(deserialized);
        }
        catch (Exception e)
        {
            Console.WriteLine($"Internal Error: {e}");
        }

        return "Invalid JSON";
    }
}