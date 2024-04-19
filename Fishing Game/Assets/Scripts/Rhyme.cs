using JetBrains.Annotations;
using TMPro;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using UnityEditor.Presets;
using UnityEngine;
using UnityEngine.UI;
public class Rhyme : MonoBehaviour
{
    public Slider timeSlider;

    public TextMeshProUGUI text;
    private bool completition = false;
    private GameObject checker;
    public GameObject W;
    public GameObject A;
    public GameObject S;
    public GameObject D;
    public GameObject E;
    public GameObject Space;
    //Change to be with GameManger
    //Checks level of fish.
    //bad = level 1;
    //average = level 1;
    //good = level 2;
    //super = level 3;

    
    //number of inputs determined by rod
    //if worst rod, number = 5
    //if bad rod, number = 4
    //if average rod, number = 3
    //if good rod, number = 2
    //if super rod, number = 1

    private int level = 3;
    private int number = 3;
    private int index = 0;
    private bool press = false;
    private float reloadTime = 20f;
    private float magazineSize = 1000f;
    private List<KeyCode> possible = new List<KeyCode>();
    private List<KeyCode> firstList = new List<KeyCode>();
    private string ink = "inked: ";
    private bool correct = false;
    // Start is called before the first frame update
    void Start()
    {
        W.SetActive(false);
        A.SetActive(false);
        S.SetActive(false);
        D.SetActive(false);
        E.SetActive(false);
        Space.SetActive(false);
        timeSlider.gameObject.SetActive(false);
        for(int i = 0; i < 6; i++)
        {
            if (i == 0)
            {
                possible.Add(KeyCode.Space);
            }
            else if (i == 1)
            {
                possible.Add(KeyCode.W);
            }
            else if (i == 2)
            {
                possible.Add(KeyCode.A);
            }
            else if (i == 3)
            {
                possible.Add(KeyCode.S);
            }
            else if ((i == 4))
            {
                possible.Add(KeyCode.D);
            }
            else
            {
                possible.Add(KeyCode.E);
            }
        }
        //firstList.Add(KeyCode.Space);
    }

    // Update is called once per frame
    void Update()
    {
        if ((press == true)&&(index < firstList.Count))
        {
            //Debug.Log("Listed length " + firstList.Count +  " /index " + index);
            //if(first)
            if (Input.GetKeyDown(firstList[index]))
            {
               // Debug.Log("got here");
                index++;
                checker.SetActive(false);
                if (index == number)
                {
                   // Debug.Log("correction");
                    correct = true;
                    CancelInvoke("ReloadFinished");
                    Invoke("ReloadFinished", 0);
                }
                else
                {
                    SetKey();
                }
                //timeSlider.value = magazineSize;
                //CancelInvoke("scrollIncrease");
                //CancelInvoke("ReloadFinished");
                //InvokeRepeating("scrollIncrease", 0, reloadTime / magazineSize);
                //Invoke("ReloadFinished", reloadTime);
            }
            else if (Input.anyKeyDown)
            {
                //Debug.Log("not correct");
                GameManager.Instance.canGo = false;
                timeSlider.gameObject.SetActive(false);
                //CancelInvoke("scrollIncrease");
                CancelInvoke("ReloadFinished");
                Invoke("ReloadFinished", 0);
            }
            //{
            //  Debug.Log("not good");
            //GameManager.Instance.canGo = false;
            //timeSlider.gameObject.SetActive(false);
            //}
        }
        if ((level != 0) && (press == false) && (GameManager.Instance.canGo == true))
        {
            press = true;
            Debug.Log("inked doo");
            while (firstList.Count < number)
            {
               // Debug.Log("here " + firstList.Count);
                int numb = Random.Range(0, possible.Count);
               // Debug.Log( "this " + numb);
                KeyCode n = possible[numb];
                //text += (possible[numb]).ToString();
               // Debug.Log( "that " + n.ToString());
                firstList.Add(n);
                ink += possible[numb].ToString() + " ";

            }
           // Debug.Log(ink);
            text.text = ink;
            SetKey();
           // Debug.Log("Nother" + checker);
            timeSlider.gameObject.SetActive(true);
            InvokeRepeating("scrollIncrease", 0, reloadTime / magazineSize);

            Invoke("ReloadFinished", reloadTime);
        }
        if(level == 0){
            reloadTime = 20.0f;
        }
        
    }
    private void scrollIncrease()
    {
        if ((press == true) && (timeSlider.value != 0))//((pressed == false) && (timeSlider.value != 0))
        {
            Debug.Log("now");
            timeSlider.value -= 1;
        }
    }
    private void SetKey()
    {
        if (firstList[index] == KeyCode.W)
        {
            W.SetActive(true);
            checker = W;
        }
        else if (firstList[index] == KeyCode.A)
        {
            A.SetActive(true);
            checker = A;
        }
        else if (firstList[index] == KeyCode.S)
        {
            S.SetActive(true);
            checker = S;
        }
        else if (firstList[index] == KeyCode.D)
        {
            D.SetActive(true);
            checker = D;
        }
        else if (firstList[index] == KeyCode.E)
        {
            E.SetActive(true);
            checker = E;
        }
        else if (firstList[index] == KeyCode.Space)
        {
            Space.SetActive(true);
            checker = Space;
        }

    }
    private void ReloadFinished()
    {
        //if (level != 0)
        //{
          //  GameManager.Instance.canGo = true;
        //}
       // Debug.Log("here");
        //havoc = 4;
        timeSlider.gameObject.SetActive(false);
        press = false;
        index = 0;
        //deal = false;
        //pressed = false;
        if(correct == true)
        {
            level--;
            number++;
            reloadTime -= 5.0f;
            if(level == 0)
            {
                completition = true;
                GameManager.Instance.caught = true;
            }
        }
        else
        {
            GameManager.Instance.caught = false;
            level = 0;
            Destroy(PlayerInteraction.allFish[^1]);
        }
        checker.SetActive(false);
        firstList.Clear();
        firstList = new List<KeyCode>();
        ink = "inked: ";
       // Debug.Log("random" + firstList.Count);
        timeSlider.value = magazineSize;
        CancelInvoke("scrollIncrease");
        correct = false;
        GameManager.Instance.catching = false;
        //bulletscount.text = "x " + bulletsLeft;
    }
}

