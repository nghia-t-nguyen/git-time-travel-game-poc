using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

public class EnterCommand : MonoBehaviour
{
    public string command;
    public InputField inputField;
    public GameObject textDisplay;
    private const int Q_SIZE = 62;
    private string[] commandQueue = new string[Q_SIZE];
    private int queueIndex = 0;

    private void Update() {
        if (Input.GetKeyUp(KeyCode.Return))
            storeCommand();
    }

    public void storeCommand() {
        command = inputField.text;
        addLine("$ " + command + "\n");
        textDisplay.GetComponent<Text>().text = queueToString();
        inputField.text = "";
        SetCaretVisible(0);
    }
    
    private void SetCaretVisible(int pos) {
        inputField.caretPosition = pos; // desired cursor position
 
        inputField.GetType().GetField("m_AllowInput", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(inputField, true);
        inputField.GetType().InvokeMember("SetCaretVisible", BindingFlags.NonPublic | BindingFlags.InvokeMethod | BindingFlags.Instance, null, inputField, null);
        }

    private void addLine(string line) {
        if (queueIndex < Q_SIZE) {
            commandQueue[queueIndex] = line;
            ++queueIndex;
        } else {
            for (int i = 0; i < Q_SIZE-1; ++i)
                commandQueue[i] = commandQueue[i+1];
            commandQueue[Q_SIZE-1] = line;
        }
    }

    private string queueToString() {
        string ret = "";
        foreach (string c in commandQueue) {
            ret += c;
        }
        return ret;
    }
}
