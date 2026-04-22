using UnityEngine;

public class LeaveConfirmUI : MonoBehaviour
{
    public GameObject confirmPanel; // assign ConfirmLeavePanel here
    public LeaveLobbyHandler leaveLobbyHandler;

    void Start()
    {
        confirmPanel.SetActive(false); // hide at start
    }

    // Called by Back or Leave button
    public void ShowConfirm()
    {
        confirmPanel.SetActive(true);
    }

    // Called by Yes button
    public void ConfirmLeave()
    {
        confirmPanel.SetActive(false);
        leaveLobbyHandler.LeaveLobby();
    }

    // Called by No button
    public void CancelLeave()
    {
        confirmPanel.SetActive(false);
    }
}
