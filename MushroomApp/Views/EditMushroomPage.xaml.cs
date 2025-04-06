using MushroomApp.Models;
using MushroomApp.Services;

namespace MushroomApp.Views;

public partial class EditMushroomPage : ContentPage
{
    private MushroomService _service;
    private Mushroom _mushroom;

    public EditMushroomPage(MushroomService service, Mushroom mushroom)
    {
        InitializeComponent();
        _service = service;
        _mushroom = mushroom;

        NameEntry.Text = mushroom.Name;
        LatinEntry.Text = mushroom.LatinName;
        DescEditor.Text = mushroom.Description;
        ImagePathEntry.Text = mushroom.ImagePath;
    }

    private async void OnSaveClicked(object sender, EventArgs e)
    {
        var list = await _service.LoadAsync();
        var item = list.FirstOrDefault(m => m.Name == _mushroom.Name && m.LatinName == _mushroom.LatinName);
        if (item != null)
        {
            item.Name = NameEntry.Text;
            item.LatinName = LatinEntry.Text;
            item.Description = DescEditor.Text;
            item.ImagePath = ImagePathEntry.Text;
            await _service.SaveAsync(list);
        }
        await Navigation.PopAsync();
    }

    private async void OnDeleteClicked(object sender, EventArgs e)
    {
        var list = await _service.LoadAsync();
        list.RemoveAll(m => m.Name == _mushroom.Name && m.LatinName == _mushroom.LatinName);
        await _service.SaveAsync(list);
        await Navigation.PopAsync();
    }
}