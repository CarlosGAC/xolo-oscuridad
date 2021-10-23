using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseScreen : MonoBehaviour
{
    public Button pauseButton;
    private void ToggleButtonText(bool pauseScreenActive)
    {
        if(pauseScreenActive)
        {
            pauseButton.GetComponentInChildren<Text>().text = ">";
        }
        else
        {
            pauseButton.GetComponentInChildren<Text>().text = "II";
        }
    }
    public void TogglePauseScreen()
    {
        bool inverse = !gameObject.activeInHierarchy;
        ToggleButtonText(inverse);
        gameObject.SetActive(inverse);
    }
}
