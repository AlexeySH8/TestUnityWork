using AxGrid.Base;
using AxGrid.FSM;
using AxGrid.Model;
using AxGrid;
using UnityEngine;

public class SlotInit : MonoBehaviourExt
{
    private static readonly string Idle = "Idle";

    [OnAwake]
    public void InitFSM()
    {
        Settings.Fsm = new FSM();
        Settings.Fsm.Add(new IdleState());
        Settings.Fsm.Add(new SpinningState());
        Settings.Fsm.Add(new StoppingState());
    }

    [OnStart]
    public void SatrtFSM()
    {
        Settings.Fsm.Start(Idle);
    }

    [OnUpdate]
    public void UpdateFSM()
    {
        Settings.Fsm.Update(Time.deltaTime);
    }
}
