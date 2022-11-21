using HeatHomeApp.VeiwModel;
using HeatHomeApp.Services;
using Microsoft.AspNetCore.SignalR.Client;

namespace HeatHomeApp.Veiw;

public partial class HeatingUnitPage : ContentPage
{
	private readonly SingleHeatingUnitVeiwModel singleHeatingUnitVeiwModel;
	HubConnection HubConnection;
	public HeatingUnitPage(SingleHeatingUnitVeiwModel singleHeatingUnitVeiwModel)
	{
		InitializeComponent();
		BindingContext = singleHeatingUnitVeiwModel;
		this.singleHeatingUnitVeiwModel = singleHeatingUnitVeiwModel;
	}

	protected override void OnNavigatedTo(NavigatedToEventArgs e)
	{
		base.OnNavigatedTo(e);
	}

	private void Stepper_ValueChanged(object sender, ValueChangedEventArgs e)
	{
        double value = e.NewValue;
    }

	private void Switch_Toggled(object sender, ToggledEventArgs e)
	{
        singleHeatingUnitVeiwModel.ToggleIsOn();
    }
}