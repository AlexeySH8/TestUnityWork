using AxGrid.FSM;
using AxGrid.Model;
using AxGrid;
using AxGrid.Base;

[State("Stopping")]
public class StoppingState : FSMState
{
    private static readonly string StopReel = "StopReel";
    private static readonly string CanStop = "CanStop";
    private static readonly string StopParticles = "StopParticles";
    private static readonly string SnapReelToSymbol = "SnapReelToSymbol";
    private static readonly string Idle = "Idle";
    private const float _snapDuration = 2.6f;

    [Enter]
    public void Enter()
    {
        Model.Set(CanStop, false);
        Settings.Invoke(StopReel);
        Settings.Invoke(StopParticles);
    }

    [One(_snapDuration)]
    public void SnapAndReturnToIdle()
    {
        Settings.Invoke(SnapReelToSymbol);
        Parent.Change(Idle);
    }
}
