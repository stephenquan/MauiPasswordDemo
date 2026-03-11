// MainPage.xaml.cs

namespace MauiPasswordDemo;

public partial class MainPage : ContentPage
{
	public MainPage()
	{
		InitializeComponent();
	}

	async void OnResetPassword(object sender, EventArgs e)
	{
		await Shell.Current.GoToAsync(nameof(ResetPasswordPage));
	}
}
