using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    //Timer functions
    //      StartDay: start an in-game day
    //      EndDay: end an in-game day
    //      UpdateTime: update the on-screen overall day time
    //      FishCatchWait: starts a ~2-minute intervals where the rod has been cast, and the player is waiting for a fish to grab the hook.
    //      SetTimer: the most important: a coroutine that counts the time until a callback should be run

    //Approximately two minutes for each fish catch, +/- up to 30 seconds for a bit of variability
    //Five fish catches per day (7.5 minutes maximum), OR, after 12 minutes the game automatically ends the day
    // Start is called before the first frame update

    //general variables
    public bool dayStarted;
    public int hour;
    public int minute;
    public static int fishCaught;

    public Coroutine dayCoroutine; //so we can forcibly end it after 5 fish have been caught
    public TMP_Text dayTimeText;

    private void Start()
    {
        StartDay();
    }

    public void StartDay()
    {
        hour = 6;
        minute = 0;
        dayCoroutine = StartCoroutine(SetTimer(720, EndDay, UpdateTime));
    }

    public void EndDay()
    {
        //whatever cleanup we need to do to end the day
    }

    public void UpdateTime()
    {
        //update overall time and the on-screen timer
        //overall time first
        minute++;
        if(minute >= 60)
        {
            minute = 0;
            hour++;
        }
        
        //then the on-screen timer
        if(minute < 10)
        {
            dayTimeText.text = hour + ":0" + minute;
        }
        else
        {
            dayTimeText.text = hour + ":" + minute;
        }
    }

    //timer to catch fish
    public void FishCatchWait()
    {
        //start with two minutes, flat
        //int secondsTilCatch = 120;
        int secondsTilCatch = 40;

        //give or take 30 seconds
        secondsTilCatch += UnityEngine.Random.Range(-30, 30); //idk why it wants me to specify UnityEngine.Random here. I've run just Random.Range fine before.
        Debug.Log(secondsTilCatch);

        StartCoroutine(SetTimer(secondsTilCatch, PlayerInteraction.CatchFish));

        if(fishCaught >= 5)
        {
            StopCoroutine(dayCoroutine);
            EndDay();
        }
    }

    //Sets a timer that runs callback (a parameterless function AKA Action) when maxTime seconds have passed.
    //  Optional parameter callbackEverySecond: runs a callback for a function to occur every second.
    //      Notes: Will actually run for one second *longer* than maxTime. For example, passing in 2 will yield a timer that calls callback after 3 seconds. I tested it.
    //             callback and callbackEverySecond can't have parameters, or it doesn't work.
    public static IEnumerator SetTimer(int maxTime, Action callback, Action callbackEverySecond=null)
    {
        int timePassed = 0;
        while (timePassed <= maxTime)
        {
            //if there's a callback to run every second (day timer), run it
            callbackEverySecond?.Invoke();

            timePassed++;
            yield return new WaitForSeconds(1); //every second, increase the timer
        }
        //once the while is over, run the final callback
        callback.Invoke();
    }
}
