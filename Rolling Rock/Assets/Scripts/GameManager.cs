using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject equation;
    public GameObject box;
    public GameObject equalBox;
    public Text textE;
    public GameObject enemy;
    public Text scoreText;
    public Button restart;

    private Equation scriptE;
    private int a = 0, b, c;
    private float timeBeforeSolve = 5.0f, curTime;
    private GameObject[] boxes;
    private float scoreTime = 120.0f;
    private int score = 0;
    private float mobSpawnTime = 15.0f, curTimeM;
    
    void Start()
    {
        boxes = new GameObject[8];
        scriptE = equation.GetComponent<Equation>();
        curTime = timeBeforeSolve;
        curTimeM = mobSpawnTime;
        NewQuestion();
        Button btn = restart.GetComponent<Button>();
        btn.onClick.AddListener(restartGame);
    }

    void Update()
    {
        curTimeM -= Time.deltaTime;
        scoreTime -= Time.deltaTime;
        if (curTimeM <= 0)
        {
            SpawnMob();
            curTimeM = mobSpawnTime;
        }
        if (scriptE.solved)
        {
            curTime -= Time.deltaTime;
            if (curTime <= 0)
            {
                for (int i = 0; i < 6; i++)
                    Destroy(boxes[i]);
                score += (int) Mathf.Max(scoreTime, 0) + 5;
                scoreText.text = score.ToString();
                NewQuestion();
                equalBox.GetComponent<BoxController>().RandomPos();
                curTime = timeBeforeSolve;
                scriptE.solved = false;
                scoreTime = 120.0f;
            }
        }
    } 

    void NewQuestion()
    {
        SpawnMob();
        scriptE.ans = Random.Range(-10, 10);
        a = Random.Range(-10, 10);
        while (a == 0)
            a = Random.Range(-10, 10);
        b = Random.Range(-100, 100);
        c = a * scriptE.ans + b;

        scriptE.cmp = (char) (Random.Range(0, 26) + 'a');
        boxes[0] = CreateBox(scriptE.ans);
        boxes[1] = CreateBox(0, scriptE.cmp);

        if (b == 0)
            textE.text = a.ToString() + scriptE.cmp.ToString() + " = " + c.ToString();
        else if (b < 0)
            textE.text = a.ToString() + scriptE.cmp.ToString() + b.ToString() + " = " + c.ToString();
        else
            textE.text = a.ToString() + scriptE.cmp.ToString() + " + " + b.ToString() + " = " + c.ToString();

        int sets = 2, i = 2;
        while (sets-- >0)
        {
            char temp = (char)(Random.Range(0, 26) + 'a');
            boxes[i] = CreateBox(0, temp);
            i++;
            int tmp = (int)Random.Range(-10f, 10f);
            boxes[i] = CreateBox(tmp);
            i++;
        }
    }

    GameObject CreateBox(int x=0, char c='~')
    {
        GameObject temp = Instantiate(box, new Vector2(Random.Range(-10f, 10f), Random.Range(-10f, 10f)), Quaternion.identity);
        temp.GetComponent<BoxController>().num = x;
        temp.GetComponent<BoxController>().c = c;
        return temp;
    }

    void SpawnMob()
    {
        GameObject temp = Instantiate(enemy, new Vector2(Random.Range(-10f, 10f), Random.Range(-10f, 10f)), Quaternion.identity);
    }

    void restartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
