using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using static System.Net.Mime.MediaTypeNames;
//using UnityEngine.UIElements;

public class Caught : MonoBehaviour
{
    public Slider timeSlider;
    private int havoc;
    private bool pressed = false;
    private bool deal = false;
    private float reloadTime = 1.5f;
    private float magazineSize = 100f;
    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.canGo = false;
        timeSlider.gameObject.SetActive(false);
        havoc = 3;
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.Instance.catching && havoc == 3)
        {
            timeSlider.gameObject.SetActive(true);
            //  if (timeSlider.value == 0)
            // {
            //   havoc = 4;
            ////    timeSlider.gameObject.SetActive(false);
            //   deal = false;
            // }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Debug.Log("entered");
                CancelInvoke("ReloadFinished");
                Invoke("ReloadFinished", 0);
                pressed = true;
                GameManager.Instance.canGo = true;
            }
            if(deal == false)
            {
                deal = true;
                InvokeRepeating("scrollIncrease", 0, reloadTime / magazineSize);

                Invoke("ReloadFinished", reloadTime);
                
            }
            
        }
        else if(GameManager.Instance.catching == false)
        {
            havoc = 3;
        }
    }
    private void scrollIncrease()
    {
        if ((pressed == false) && (timeSlider.value != 0))
        {
            Debug.Log("now");
            timeSlider.value -= 1;
        }
    }

    private void ReloadFinished()
    {
        Debug.Log("here");
        havoc = 4;
        timeSlider.gameObject.SetActive(false);
        deal = false;
        pressed = false;
        timeSlider.value = magazineSize;
        CancelInvoke("scrollIncrease");
        //bulletscount.text = "x " + bulletsLeft;
    }
}
