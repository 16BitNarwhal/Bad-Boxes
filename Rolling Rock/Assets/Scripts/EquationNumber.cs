using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
public class EquationNumber : MonoBehaviour
{
    public int type;
    public GameObject equation;

    private Equation script;

    private void Start()
    {
        script = equation.GetComponent<Equation>();
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        GameObject box = col.gameObject;
        if(box.layer == 8)
        {
            BoxController s = box.GetComponent<BoxController>();
            if (s.state == 4) return;
            if (type == 1)
            {
                script.boxC = box;
                if (s.c != '~')
                    s.state = 2;
            }
            else if (type == 2)
            {
                script.boxB = box;
                if (s.c != '=')
                    s.state = 2;
            }
            else
            {
                script.boxA = box;
                if (s.c == '~' || s.c == '=')
                    s.state = 2;
            }
            if (s.state != 1 && s.state != 3)
                script.res = false;
            else
                script.res = true;
            
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        GameObject box = col.gameObject;
        if(box.layer == 8)
        {
            BoxController s = box.GetComponent<BoxController>();
            if (s.state == 4) return;
            s.state = 1;
        }
    }
}
