using AxGrid.FSM;
using AxGrid.Model;

[State("Idle")]
public class IdleState : FSMState
{
    private static readonly string IsSpinning = "IsSpinning";
    private static readonly string CanStop = "CanStop";
    private static readonly string CurrentSymbol = "CurrentSymbol";
    private static readonly string Spinning = "Spinning";

    [Enter]
    private void Enter()
    {
        Model.Set(IsSpinning, false);
        Model.Set(CanStop, false);
        Model.Set(CurrentSymbol, 0);
    }

    [Bind("StartClicked")]
    private void OnStartClicked()
    {
        Parent.Change(Spinning);
    }
}



