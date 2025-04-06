using System.Text.Json;
using MushroomApp.Models;

namespace MushroomApp.Services;

public class MushroomService
{
    private string filePath = Path.Combine(FileSystem.AppDataDirectory, "mushrooms.json");

    public async Task<List<Mushroom>> LoadAsync()
    {
        if (!File.Exists(filePath))
            return new List<Mushroom>();

        var json = await File.ReadAllTextAsync(filePath);
        return JsonSerializer.Deserialize<List<Mushroom>>(json) ?? new List<Mushroom>();
    }

    public async Task SaveAsync(List<Mushroom> mushrooms)
    {
        var json = JsonSerializer.Serialize(mushrooms);
        await File.WriteAllTextAsync(filePath, json);
    }
}
