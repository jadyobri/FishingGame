using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomFunctions : MonoBehaviour
{
    //Start with the Worst rod, which correspondingly has the worst chances. Anything you get in a gacha is better

    //Randomness functions:
    //  1. Rod gacha: flat 0-100 roll, type of roll (best, decent, poor) determines which values fall into which ranges. I wrote percentages because that's how I did it for pseudo.
    //                  best chance roll (costs $100) -> 75% Bad Rod, 20% Average Rod, 4% Good Rod, 1% Super Rod
    //                  decent chance roll (costs $50) -> 40% Bad Rod, 35% Average Rod, 22% Good Rod, 3% Super Rod
    //                  poor chance roll (costs $10) -> 25% Bad Rod, 40% Average Rod, 30% Good Rod, 5% Super Rod
    //  2. Fish weight: dependant on the type of rod. Edge values 0.1 (for smallest fish, on Worst or Bad Rod) or 20.0 (on Super Rod)
    //                  Worst Rod (0.1 lbs to 1.0 lbs)
    //                  Bad Rod (0.1 lbs to 3.0 lbs)
    //                  Average Rod (3.0 lbs to 7.0 lbs)
    //                  Good Rod (7.0 lbs to 14.0 lbs)
    //                  Super Rod (14.0 lbs to 20.0 lbs)
    //  3. Fish color: flat 0-100 roll, rod type determines the ranges of different colors/qualities
    //                  Worst Rod -> 0-79 brown (bad), 80-100 green (average)
    //                  Bad Rod -> 0-59 brown (bad), 60-94 green (average), 95-99 orange (good), 100 purple (super)
    //                  Average Rod -> 0-19 brown (bad), 20-79 green (average), 80-94 orange (good), 95-100 purple (super)
    //                  Good Rod -> 0-9 brown (bad), 10-29 green (average), 30-89 orange (good), 90-100 purple (super)
    //                  Super Rod -> 0 brown (bad), 1-14 green (average), 15-39 orange (good), 40-100 purple (super)

    public static string RodPull(string quality)
    {
        string result; 
        int rollResult = Random.Range(0, 100);
        //take quality of chance roll, return type of rod (Bad, Average, Good, Super)
        //1, 2, 3 on comment above the functions describe what rollResult for each quality returns what rollResult
        switch (quality)
        {
            case "Best":
                if(rollResult < 25)
                {
                    result = "Bad";
                }
                else if(rollResult < 65)
                {
                    result = "Average";
                }
                else if(rollResult < 95)
                {
                    result = "Good";
                }
                else
                {
                    result = "Super";
                }
                break;
            case "Decent":
                if (rollResult < 40)
                {
                    result = "Bad";
                }
                else if (rollResult < 75)
                {
                    result = "Average";
                }
                else if (rollResult < 97)
                {
                    result = "Good";
                }
                else
                {
                    result = "Super";
                }
                break;
            default: //Poor
                if (rollResult < 75)
                {
                    result = "Bad";
                }
                else if (rollResult < 95)
                {
                    result = "Average";
                }
                else if (rollResult < 99)
                {
                    result = "Good";
                }
                else
                {
                    result = "Super";
                }
                break;
        }

        return result;
    }

    public static float FishWeight(string rodType)
    {
        //switch expressions are so cool whoa
        float result = rodType switch
        {
            "Worst" => Random.Range(0.1f, 1.0f),
            "Bad" => Random.Range(0.1f, 3.0f),
            "Average" => Random.Range(3.0f, 7.0f),
            "Good" => Random.Range(7.0f, 14.0f),
            //Default: Super Rod
            _ => Random.Range(14.0f, 20.0f),
        };
            
        return result;
    }

    public static string FishQuality(string rodType)
    {
        string result;
        int rollResult = Random.Range(0, 101); //roll from 0-100

        switch (rodType)
        {
            case "Worst":
                if (rollResult < 80)
                {
                    result = "Bad";
                }
                else
                {
                    result = "Average";
                }
                break;
            case "Bad":
                if (rollResult < 60)
                {
                    result = "Bad";
                }
                else if (rollResult < 95)
                {
                    result = "Average";
                }
                else if (rollResult < 100)
                {
                    result = "Good";
                }
                else //only at exactly 100
                {
                    result = "Super";
                }
                break;
            case "Average":
                if (rollResult < 20)
                {
                    result = "Bad";
                }
                else if (rollResult < 80)
                {
                    result = "Average";
                }
                else if (rollResult < 95)
                {
                    result = "Good";
                }
                else //95-100
                {
                    result = "Super";
                }
                break;
            case "Good":
                if (rollResult < 10)
                {
                    result = "Bad";
                }
                else if (rollResult < 30)
                {
                    result = "Average";
                }
                else if (rollResult < 90)
                {
                    result = "Good";
                }
                else //90-100
                {
                    result = "Super";
                }
                break;
            default: //Super Rod
                if (rollResult == 0)
                {
                    result = "Bad";
                }
                else if (rollResult < 15)
                {
                    result = "Average";
                }
                else if (rollResult < 40)
                {
                    result = "Good";
                }
                else //40-100
                {
                    result = "Super";
                }
                break;
        }
        return result;
        //Key: (so I don't have to scroll up)
        //  Worst Rod -> 0-79 brown (bad), 80-100 green (average)
        //  Bad Rod -> 0-59 brown (bad), 60-94 green (average), 95-99 orange (good), 100 purple (super)
        //  Average Rod -> 0-19 brown (bad), 20-79 green (average), 80-94 orange (good), 95-100 purple (super)
        //  Good Rod -> 0-9 brown (bad), 10-29 green (average), 30-89 orange (good), 90-100 purple (super)
        //  Super Rod -> 0 brown (bad), 1-14 green (average), 15-39 orange (good), 40-100 purple (super)
    }
}
