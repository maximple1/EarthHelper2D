using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseApplicationScr : MonoBehaviour
{
    public void CloseApplication()
    {
        Application.Quit();
    }
    public void GoToChannel()
    {
        Application.OpenURL("https://www.youtube.com/channel/UCqp3fQ7u4dgFYsZ7hdwLqYA");
    }
}
