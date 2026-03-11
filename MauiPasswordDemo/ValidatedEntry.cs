// ValidatedEntry.cs
using CommunityToolkit.Maui;
using CommunityToolkit.Maui.Converters;
namespace MauiPasswordDemo;

public partial class ValidatedEntry : ContentView
{
	[BindableProperty(DefaultBindingMode = BindingMode.TwoWay)] public partial string Text { get; set; } = string.Empty;
	[BindableProperty] public partial string LabelValue { get; set; } = string.Empty;
	[BindableProperty] public partial bool IsValid { get; set; } = true;
	[BindableProperty] public partial string Error { get; set; } = string.Empty;
	public ValidatedEntry()
	{
		ArgumentNullException.ThrowIfNull(Application.Current, nameof(Application));
		Application.Current.Resources.TryGetValue("ErrorBackground", out var errorBackgroundColor);
		ArgumentNullException.ThrowIfNull(errorBackgroundColor, nameof(errorBackgroundColor));
		var entry = new Entry { BindingContext = this };
		entry.SetBinding(Entry.PlaceholderProperty, static (ValidatedEntry v) => v.LabelValue);
		entry.SetBinding(Entry.TextProperty, static (ValidatedEntry v) => v.Text, mode: BindingMode.TwoWay);
		var errorLabel = new Label { BindingContext = this };
		errorLabel.SetBinding(Label.TextProperty, static (ValidatedEntry v) => v.Error);
		errorLabel.SetBinding(Label.IsVisibleProperty, static (ValidatedEntry v) => v.IsValid, converter: new InvertedBoolConverter());
		errorLabel.TextColor = (Color)errorBackgroundColor;
		Content = new VerticalStackLayout { entry, errorLabel };
	}
}
