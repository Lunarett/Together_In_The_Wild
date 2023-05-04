using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class ButtonEnabler : MonoBehaviourPunCallbacks
{
    private Button btn;

    private void Awake() 
    {
        btn = GetComponent<Button>();
        btn.interactable = false;
    }

    public override void OnConnectedToMaster()
    {
        btn.interactable = true;
    }
}
