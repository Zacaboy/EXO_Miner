using UnityEngine.InputSystem;
using UnityEngine;
using System.Collections.Generic;

#if UNITY_EDITOR
using UnityEditor;
#endif

#if UNITY_EDITOR
[InitializeOnLoad]
#endif
public static class InitCustomDeviceUsages
{
    static InitCustomDeviceUsages()
    {
        Initialize();
    }

    [RuntimeInitializeOnLoadMethod]
    private static void Initialize()
    {
        InputSystem.RegisterLayoutOverride(@"
          {
              ""name"" : ""JoystickConfigurationUsageTags"",
              ""extend"" : ""Joystick"",
              ""commonUsages"" : [
                  ""LeftHand"", ""RightHand""
              ]
          }
      ");
    }
}


public class CustomDeviceUsages : MonoBehaviour
{



    protected void OnEnable()
    {
        List<InputDevice> joysticks = new List<InputDevice>();

        foreach (InputDevice device in InputSystem.devices)
        {
            if (device.description.product == "Extreme 3D Pro") joysticks.Add(device);
        }

        InputSystem.SetDeviceUsage(joysticks[0], CommonUsages.LeftHand);
        InputSystem.SetDeviceUsage(joysticks[1], CommonUsages.RightHand);
    }


}
