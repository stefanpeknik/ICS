using Actie.App.View;
using Actie.App.ViewModels;

namespace Actie.App.Views;

public partial class LogInView : ContentPageBase
{
	public LogInView(LogInViewModel logInViewModel) : base(logInViewModel)
	{
		InitializeComponent();
	}
}
