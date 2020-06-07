using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equation : MonoBehaviour
{
    public int ans;
    public GameObject platV, platE, platN, boxA, boxB, boxC;
    public bool res = false, solved = false;
    public char cmp = 'x';

    private void Start()
    { 
    }

    void Update()
    {
        if (res && boxA != null && boxB != null && boxC != null)
        {
            if (boxA.GetComponent<BoxController>().c == cmp && boxC.GetComponent<BoxController>().num == ans)
            {
                solved = true;
                boxA.GetComponent<BoxController>().state = 3;
                boxB.GetComponent<BoxController>().state = 3;
                boxC.GetComponent<BoxController>().state = 3;
            }
        }
    }
}
