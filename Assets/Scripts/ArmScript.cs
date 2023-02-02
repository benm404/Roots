using Unity.VisualScripting;
using UnityEngine;

public class ArmScript : MonoBehaviour
{
    GameManager GM;
    [SerializeField] GameObject Leaf;

    Transform tr;
    Vector3 defScale;
    public float growSpeed;
    private bool mouseDown = false;

    private bool canShoot;
    private float timer;
    public float shootInterval = 0.3f;
    void Start()
    {
        tr = GetComponent<Transform>();
        defScale = tr.Find("Arm").localScale;
        GM = GameObject.FindWithTag("GameController").GetComponent<GameManager>();
    }


    void Update()
    {
        Vector3 ArmPos = new Vector3(tr.Find("Arm").position.x, tr.Find("Arm").position.y, tr.Find("Arm").position.z);

        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 0f;

        Vector3 objectPos = Camera.main.WorldToScreenPoint(tr.Find("Arm").position);

        mousePos.x = mousePos.x - objectPos.x;
        mousePos.y = mousePos.y - objectPos.y;

        float xBase = mousePos.x / Mathf.Abs(mousePos.x);
        float yBase = mousePos.y / Mathf.Abs(mousePos.y);

        float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
        Quaternion ArmAngle = Quaternion.Euler(new Vector3(0f, 0f, angle));
        tr.Find("Arm").rotation = ArmAngle;
        tr.Find("StartPoint").rotation = ArmAngle;

        Vector3 BranchPos = tr.Find("StartPoint").Find("EndPoint").position;


        if (!canShoot)
        {
            timer += Time.deltaTime;
            if (timer >= shootInterval)
            {
                canShoot = true;
                timer = 0;
            }
        }
        if (Input.GetMouseButton(0) && GM.growBar.value > 0f)
        {
            tr.Find("Arm").localScale += new Vector3(growSpeed * Time.deltaTime, 0f, 0f);
            mouseDown = true;
        }
        if (Input.GetMouseButtonUp(0) && mouseDown)
        {
            Transform Branch = Instantiate(tr.Find("Arm"), BranchPos, ArmAngle);
            Branch.AddComponent<Rigidbody2D>();
            Branch.tag = "Ground";
            Branch.Find("ArmModel").tag = "Ground";
            tr.Find("Arm").localScale = defScale;
            mouseDown = false;
        }
        if (Input.GetMouseButtonDown(1) && !Input.GetMouseButton(0) && canShoot)
        {
            if (GM.leaves > 0)
            {
                GM.leaves -= 1;
                canShoot = false;
                Instantiate(Leaf.transform, tr.Find("StartPoint").Find("LeafPoint").position, Quaternion.identity);
            }
        }
        // print("MouseX: " + xBase + " " + "MouseY: " + yBase);
    }
}