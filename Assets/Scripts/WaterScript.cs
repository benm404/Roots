using UnityEngine;

public class WaterScript : MonoBehaviour
{
    GameManager GM;

    void Start()
    {
        GM = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GM.WaterCollect();
            Destroy(gameObject);
        }
    }
}
