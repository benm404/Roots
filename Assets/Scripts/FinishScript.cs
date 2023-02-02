using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishScript : MonoBehaviour
{
    SceneSwitcher SM;

    private void Start()
    {
        SM = GameObject.FindGameObjectWithTag("SceneSwitcher").GetComponent<SceneSwitcher>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            SM.SceneSwitch(2);
        }
    }
}
