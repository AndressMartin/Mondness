using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputExt : MonoBehaviour
{
    public static float GetAxis(string axis)
    {
        if (!SceneManage.paused)
        {
            return Input.GetAxis(axis);
        }

        return 0;
    }
    public static float GetAxisRaw(string axis)
    {
        if (!SceneManage.paused)
        {
            return Input.GetAxisRaw(axis);
        }

        return 0;
    }
    public static bool GetKeyDown(KeyCode key)
    {
        if (!SceneManage.paused)
        {
            return Input.GetKeyDown(key);
        }

        return false;
    }
    public static bool GetButtonDown(string btn)
    {
        if (!SceneManage.paused)
        {
            return Input.GetButtonDown(btn);
        }

        return false;
    }
}
