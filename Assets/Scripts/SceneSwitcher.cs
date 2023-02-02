using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    public void SceneSwitch(int scene)
    {
        SceneManager.LoadScene(scene);
    }

    public void Quit()
    {
        Application.Quit(0);
    }

    public void TextDisplay(GameObject Text)
    {
        if (Text.activeInHierarchy)
        {
            Text.gameObject.SetActive(false);
        }
        else { Text.gameObject.SetActive(true); }
    }
}
