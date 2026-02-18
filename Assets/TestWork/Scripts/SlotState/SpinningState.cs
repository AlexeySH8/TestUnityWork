using AxGrid.FSM;
using AxGrid.Model;
using AxGrid;

[State("Spinning")]
public class SpinningState : FSMState
{
    private static readonly string IsSpinning = "IsSpinning";
    private static readonly string CanStop = "CanStop";
    private static readonly string Stopping = "Stopping";
    private static readonly string CurrentSymbol = "CurrentSymbol";
    private static readonly string StartReel = "StartReel";
    private static readonly string StartParticles = "StartParticles";
    private static readonly string EnableStopButtonAfterDelay = "EnableStopButtonAfterDelay";
    private float _delay = 3f;

    [Enter]
    private void Enter()
    {
        Model.Set(IsSpinning, true);
        Model.Set(CanStop, false);
        Model.Set(CurrentSymbol, 0);

        Settings.Invoke(StartReel);
        Settings.Invoke(StartParticles);
        Settings.Invoke(EnableStopButtonAfterDelay, _delay);
    }

    [Bind("StopClicked")]
    public void OnStopClicked()
    {
        if (Model.GetBool(CanStop))
        {
            Parent.Change(Stopping);
        }
    }
}
