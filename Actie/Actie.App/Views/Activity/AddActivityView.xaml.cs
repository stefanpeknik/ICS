using Actie.App.ViewModels;

namespace Actie.App.Views.Activity;

public partial class AddActivityView
{
	public AddActivityView(AddActivityViewModel viewModel)
    : base(viewModel)
	{
		InitializeComponent();

        SaveButton.IsEnabled = false;
	}
}
