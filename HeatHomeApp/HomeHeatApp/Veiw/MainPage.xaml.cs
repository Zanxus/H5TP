using HeatHomeApp.Services;
using HeatHomeApp.VeiwModel;

namespace HeatHomeApp;

public partial class MainPage : ContentPage
{
	private readonly HeatingUnitVeiwModel heatingUnitVeiwModel;
	public MainPage(HeatingUnitVeiwModel heatingUnitVeiwModel)
	{
		InitializeComponent();
		BindingContext = heatingUnitVeiwModel;
	}
}

