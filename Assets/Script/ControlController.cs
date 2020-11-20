using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ControlController : MonoBehaviour
{
    static ControlController s_Instance = null;
    public static ControlController Instance => s_Instance;

    private SnapTurnProvider snapTurn;

    void Awake()
    {
        s_Instance = this;
        snapTurn = GetComponent<SnapTurnProvider>();
    }

    public void SetScripts(bool isEnable)
    {
        MasterController.Instance.LeftTeleportInteractor.gameObject.SetActive(isEnable);
        MasterController.Instance.RightTeleportInteractor.gameObject.SetActive(isEnable);
        MasterController.Instance.LeftTractorBeam.enabled = isEnable;
        MasterController.Instance.RightTractorBeam.enabled = isEnable;

        snapTurn.enabled = isEnable;
    }
}
