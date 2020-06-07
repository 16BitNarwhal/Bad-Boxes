using UnityEngine;
using UnityEngine.UI;
/* <------------   BOX STATES   ------------->
 * 1 : white : not picked up
 * 2 : red : wrong answer
 * 3 : green : right answer
 * 4 : blue : picked up
 * 
 * <------------   BOX TYPES    ------------->
 * 1 : number
 * 2 : variable
 * 3 : equals
 */
public class BoxController : MonoBehaviour
{
    public int state = 1;
    public Text val;
    public Text underscore;
    public float num = 0.0f;
    public char c = '~';

    private GameObject playerFront;
    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
        playerFront = GameObject.Find("/Player/PlayerFront");
    }
    private void Awake()
    {
        RandomPos();
    }

    void Update()
    {
        if (c != '~')
            val.text = c.ToString();
        else
            val.text = num.ToString();

        anim.SetInteger("state", state);
        val.transform.position = transform.position;
        underscore.text = "___";
        underscore.transform.position = transform.position;
        if (state == 4)
        {
            transform.position = playerFront.transform.position;
            transform.rotation = playerFront.transform.rotation;
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                state = 1;
                GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            }
        }
    }

    public void RandomPos()
    {
        transform.position = new Vector3(Random.Range(-9.0f, 9.0f), Random.Range(-9.0f, 9.0f), 0);
        state = 1;
    }

}
