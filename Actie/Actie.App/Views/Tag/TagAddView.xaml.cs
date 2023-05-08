using Actie.App.ViewModels;

namespace Actie.App.Views.Tag;

public partial class TagAddView
{
	public TagAddView(TagAddViewModel viewModel)
        : base(viewModel)
	{
		InitializeComponent();
        SaveButton.IsEnabled = false;
    }
}
