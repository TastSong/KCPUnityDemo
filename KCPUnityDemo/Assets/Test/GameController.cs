using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TinyMessenger;

public class GameController : MonoBehaviour {
    public static GameController manager = null;
    public TinyMessengerHub tinyMsgHub = new TinyMessengerHub();

    private void Awake() {
        manager = this;
    }
}
