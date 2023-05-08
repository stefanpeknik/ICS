using Actie.App.ViewModels;

namespace Actie.App.Views.User;

public partial class AddUserView
{
	public AddUserView(AddUserViewModel viewModel)
    : base(viewModel)
	{
		InitializeComponent();
        SaveButton.IsEnabled = false;
	}
}
