using System;
using System.Collections;
using System.Collections.Generic;
using TinyMessenger;
using UnityEngine;
using UnityEngine.UI;

public class UpdateUIMsg : ITinyMessage {
    public object Sender { get; private set; }
    public string str;
    public UpdateUIMsg(string s) {
        str = s;
    }
}

public class KcpTest : MonoBehaviour {
    public Text hostText;
    public Text portText;
    public Button connectBtn;
    public Button closeBtn;
    public Text contentText;
    public InputField inputField;
    public Button sendBtn;

    private string host = "127.0.0.1";
    private int port = 40001;
    private KcpManager kcpManager = new KcpManager();
    private TinyMessageSubscriptionToken token;
    private string recStr;
    private bool isRec = false;

    private void Start() {
        hostText.text = host;
        portText.text = port.ToString();

        connectBtn.onClick.AddListener(() => {
            kcpManager.ConnectKcp(host, port);
        });

        closeBtn.onClick.AddListener(() => {
            kcpManager.Stop();
        });

        sendBtn.onClick.AddListener(() => {
            kcpManager.Send(inputField.text);
        });

        token = GameController.manager.tinyMsgHub.Subscribe<UpdateUIMsg>((m) => {
            Debug.LogError("++++++Receive  = " + m.str);
            recStr = m.str;
            isRec = true;
        });
    }

    private void Update() {
        if (isRec) {
            contentText.text = contentText.text + "\n" + recStr;
            isRec = false;
        }
    }

    private void OnDestroy() {
        GameController.manager.tinyMsgHub.Unsubscribe(token);
    }
}
