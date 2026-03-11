// ResetPasswordPage.xaml.cs
using CommunityToolkit.Maui;
using CommunityToolkit.Maui.Converters;
using CommunityToolkit.Maui.Markup;
namespace MauiPasswordDemo;

public partial class ResetPasswordPage : ContentPage
{
	[BindableProperty(DefaultBindingMode = BindingMode.TwoWay)] public partial string Code { get; set; } = string.Empty;
	[BindableProperty(DefaultBindingMode = BindingMode.TwoWay)] public partial string NewPassword { get; set; } = string.Empty;
	[BindableProperty(DefaultBindingMode = BindingMode.TwoWay)] public partial string ConfirmPassword { get; set; } = string.Empty;
	[BindableProperty] public partial bool CodeValid { get; set; } = false;
	[BindableProperty] public partial bool NewPasswordValid { get; set; } = true;
	[BindableProperty] public partial bool ConfirmPasswordValid { get; set; } = true;
	public ResetPasswordPage()
	{
		BindingContext = this;
		InitializeComponent();
		this.SetBinding(CodeValidProperty, static (ResetPasswordPage p) => p.Code, BindingMode.OneWay, new IsStringNotNullOrEmptyConverter());
		this.SetBinding(NewPasswordValidProperty, static (ResetPasswordPage p) => p.NewPassword, BindingMode.OneWay, new IsStringNotNullOrEmptyConverter());
		this.SetBinding(ConfirmPasswordValidProperty,
			new MultiBinding
			{
				Bindings =
				{
					BindingBase.Create(static (ResetPasswordPage p) => p.ConfirmPassword, BindingMode.OneWay),
					BindingBase.Create(static (ResetPasswordPage p) => p.NewPassword, BindingMode.OneWay)
				},
				Converter = new FuncMultiConverter<string?, string?, bool>(((string? confirmPassword, string? newPassword) v) =>
				{
					return !string.IsNullOrEmpty(v.confirmPassword)
						&& !string.IsNullOrEmpty(v.newPassword)
						&& v.newPassword.Equals(v.confirmPassword);
				})
			});
	}
}
