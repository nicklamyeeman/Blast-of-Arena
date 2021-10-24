using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsActions : MonoBehaviour {

    public Dropdown dropRes;
    private Resolution[] resolutions;

    void Start() {
        List<string> options = new List<string>();
        resolutions = Screen.resolutions;
        Resolution current = Screen.currentResolution;
        int resIndex = 0;

        for (int i = 0; i < resolutions.Length; i += 1) {
            Resolution elem = resolutions[i];
            options.Add(elem.width + " x " + elem.height);
            if (elem.width == current.width && elem.height == current.height)
                resIndex = i;
        }

        dropRes.ClearOptions();
        dropRes.AddOptions(options);
        dropRes.value = resIndex;
        dropRes.RefreshShownValue();
    }

    public void setResolution(int id)
    {
        print(id);
        Resolution res = resolutions[id];
        print(res.width + " x " + res.height);
        Screen.SetResolution(res.width, res.height, Screen.fullScreen);
    }

    public void setFullscreen(bool isFullscreen)
    {
        print("Fullscreen " + isFullscreen);
        Screen.fullScreen = isFullscreen;
    }
}