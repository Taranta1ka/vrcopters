using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mode : MonoBehaviour
{
    public void Gamepad()
    {
        PlayerPrefs.DeleteKey("ControlMode");
        PlayerPrefs.SetInt("ControlMode", 1);
        PlayerPrefs.Save();
    }
    public void Keyboard()
    {
        PlayerPrefs.DeleteKey("ControlMode");
        PlayerPrefs.SetInt("ControlMode", 0);
        PlayerPrefs.Save();
    }
}
