using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public float growDecay = 1f;
    public float growAdd = 15f;
    public Slider growBar;

    public int leaves = 6;
    public TextMeshProUGUI leafText;

    void Start()
    {
        leafText.text = "Leaves: " + leaves;
    }


    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            growBar.value -= growDecay * Time.deltaTime;
        }

        leafText.text = "Leaves: " + leaves;
    }

    public void WaterCollect()
    {
        
        growBar.value += growAdd;
        leaves += 2;
        if (leaves >= 8)
        {
            leaves = 8;
        }

        if(growBar.value >= 100f)
        {
            growBar.value = 100f;
        }
            
    }

    
}
