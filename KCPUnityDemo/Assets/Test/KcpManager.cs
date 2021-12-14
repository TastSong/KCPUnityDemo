using KcpCSharp;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KcpManager : KcpClient {

    private new KcpClient client;

    protected override void HandleReceive(ByteBuf bb) {
        string str = System.Text.Encoding.UTF8.GetString(bb.GetRaw());
        GameController.manager.tinyMsgHub.Publish(new UpdateUIMsg(str));
    }

    protected override void HandleException(Exception ex) {
        Debug.LogError("+++++ HandleException");
        base.HandleException(ex);
    }

    protected override void HandleTimeout() {
        Debug.LogError("+++++ HandleTimeout");
        base.HandleTimeout();
    }

    public void Connect(string host, int port) {
        client = new KcpManager();
        client.NoDelay(1, 10, 2, 1);
        client.WndSize(64, 64);
        client.Timeout(10 * 1000);
        client.SetMtu(512);
        client.SetMinRto(10);
        client.SetConv(port);
        client.Connect(host, port);
        client.Start();
    }

    public void Send(string content) {
        if (client != null && client.IsRunning()) {
            ByteBuf bb = new ByteBuf(System.Text.Encoding.UTF8.GetBytes(content));
            Debug.Log(bb.ReadableBytes());
            client.Send(bb);
        }     
    }

    public void Close() {       
        if (client != null) {
            client.Stop();
            Debug.LogError("++++++ client.IsRunning() = " + client.IsRunning());
        }       
    }
}
