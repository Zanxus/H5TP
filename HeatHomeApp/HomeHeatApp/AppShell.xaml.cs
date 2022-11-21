using HeatHomeApp.Veiw;

namespace HeatHomeApp;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

		Routing.RegisterRoute(nameof(HeatingUnitPage),typeof(HeatingUnitPage));
	}
}
