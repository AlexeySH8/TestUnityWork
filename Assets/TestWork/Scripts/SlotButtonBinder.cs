using AxGrid;
using AxGrid.Base;
using UnityEngine;
using UnityEngine.UI;
using AxGrid.Model;
using System.Collections;

public class SlotButtonBinder : MonoBehaviourExtBind
{
    [SerializeField] private string _signal;
    [SerializeField] private Button _button;

    private static readonly string IsSpinning = "IsSpinning";
    private static readonly string CanStop = "CanStop";

    [OnAwake]
    public void Init()
    {
        _button.onClick.AddListener(() => Settings.Invoke(_signal));
    }

    [Bind("OnIsSpinningChanged")]
    private void OnIsSpinningChanged()
    {
        bool isSpinnig = Model.GetBool(IsSpinning);
        if (_signal == "StartClicked")
            _button.interactable = !isSpinnig;
    }

    [Bind("OnCanStopChanged")]
    private void OnCanStopChanged()
    {
        bool canStop = Model.GetBool(CanStop);
        if (_signal == "StopClicked")
            _button.interactable = canStop;
    }

    [Bind("EnableStopButtonAfterDelay")]
    private void EnableStopButtonAfterDelay(float delay)
    {
        StartCoroutine(EnableStopRoutine(delay));
    }

    private IEnumerator EnableStopRoutine(float delay)
    {
        yield return new WaitForSeconds(delay);
        Model.Set("CanStop", true);
    }
}
