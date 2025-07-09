namespace MauiExpenseTrackerApp.Pages;

public partial class GraphPage : ContentPage
{
	public GraphPage()
	{
		InitializeComponent();
	}
    private void OnPaintSurface(object sender, SkiaSharp.Views.Maui.SKPaintSurfaceEventArgs e)
    {
        var canvas = e.Surface.Canvas;
        canvas.Clear(SkiaSharp.SKColors.LightBlue);
    }
}