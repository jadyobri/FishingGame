using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInteraction : MonoBehaviour
{
    public static string rodQuality = "Super";
    public static float money = 120.0f;

    public static float bait = 0;

    public GameObject fishPrefab;
    public GameObject timeManager;
    public static bool rodIsOut; //don't cast a new line while one is already cast

    public Text balanceText, baitText, sellAllText;
    public GameObject Fish1, Fish2, Fish3, SellButton, BuySceneButton, FishingButton, Lootbox1, Lootbox2, Lootbox3, BuyBaitButton;

    public static List<GameObject> allFish = new List<GameObject>();

    void Start(){
        GameManager.Instance.catching = false;
        // if (allFish.Count == 0){
        //   for(int i = 0; i < 4; i++){
        //     allFish.Add(Instantiate(fishPrefab));
        //}
        //}

        buttonCheck();
    }

    void Update()
    {
        ClickCheck();
        updateMenuScreen();
    }

    void ClickCheck()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //cast the rod
            if (!rodIsOut && timeManager != null)
            {
                CastRod();
                rodIsOut = true;
            }
            
            //Instantiate(fishPrefab);

            //get a fish prefab and add it to the list of all fish

            //this is all buggy we just need a way to have the fish passed through to this scene and itll probably work?
            //allFish.Add(Instantiate(fishPrefab));
            
        }
    }

    void CastRod()
    {
        //run the function to start the coroutine
        timeManager.GetComponent<Timer>().FishCatchWait();
        Debug.Log("Rod cast!");

        //do whatever sprite changes we need to make it clear the rod has been cast
    }

    void updateMenuScreen(){
        if(balanceText != null){
            balanceText.text = "Balance: $" + money.ToString();
        }
        if(baitText != null){
            baitText.text = "Bait: " + bait.ToString();
        }

        if(Fish1 != null && Fish2 != null && Fish3 != null){
            for(int i = 0; i < 3; i++){
                GameObject FishToModify = Fish1;
                if(i == 1){
                    FishToModify = Fish2;
                } else if(i == 2){
                    FishToModify = Fish3;
                }

                FishToModify.GetComponentInChildren<Text>().text = "";
                FishToModify.GetComponentInChildren<Button>().GetComponentInChildren<Text>().text = "";
                FishToModify.GetComponentInChildren<Image>().sprite = null;
            }
        }
        

        for(int i = 0; i < allFish.Count && i < 3 && Fish1 != null && Fish2 != null && Fish3 != null; i++){ //add in fish to all fish from the fron of the list
            float price = allFish[i].GetComponent<FishInfo>().price;
            float weight = allFish[i].GetComponent<FishInfo>().weight;

            GameObject FishToModify = Fish1;

            if(i == 1){
                FishToModify = Fish2;
            } else if(i == 2){
                FishToModify = Fish3;
            }

            FishToModify.GetComponentInChildren<Text>().text = "" + weight + " lbs";

            FishToModify.GetComponentInChildren<Button>().GetComponentInChildren<Text>().text = "Sell: $" + price;

            FishToModify.GetComponentInChildren<Image>().sprite = allFish[i].GetComponent<FishInfo>().spriteRenderer.sprite;

        }

        if(sellAllText != null){
            sellAllText.text = "Sell All: $" + ReturnTotalPrice();
        }
    }

    float ReturnTotalPrice(){
        float total = 0.0f;
        foreach(GameObject fish in allFish){
            total += fish.GetComponent<FishInfo>().price;
        }
        return total;
    }

    public static void CatchFish()
    {
        //pause the day timer (I wish there was a way to easily pause coroutines SO BAD)
        GameObject timeManager = GameObject.Find("TimeManager");
        //timeManager.SetActive(false);

        //run the code/functions to catch the fish
        Timer.fishCaught++;
        

        Debug.Log("Caught a fish!");

        //Game Manager value.
        //checks if fish is caught.
        GameManager.Instance.catching = true;
        
        GameObject fishPrefab = GameObject.Find("Fish");
        allFish.Add(Instantiate(fishPrefab));
        allFish[^1].gameObject.transform.Translate(-400, -45, 0);

        //unpause the day timer (does not currently work)
        //timeManager.SetActive(true);

        //reel it back in
        rodIsOut = false;

    }
    
    void buttonCheck(){

        if(Fish1 != null && Fish2 != null && Fish3 != null){
            Fish1.GetComponentInChildren<Button>().onClick.AddListener(() => {
                GameObject fish = allFish[0];
                money += allFish[0].GetComponent<FishInfo>().price;
                allFish.RemoveAt(0);
                Destroy(fish);
            });

            Fish2.GetComponentInChildren<Button>().onClick.AddListener(() => {
                GameObject fish = allFish[1];
                money += allFish[1].GetComponent<FishInfo>().price;
                allFish.RemoveAt(1);
                Destroy(fish);
            });

            Fish3.GetComponentInChildren<Button>().onClick.AddListener(() => {
                GameObject fish = allFish[2];
                money += allFish[2].GetComponent<FishInfo>().price;
                allFish.RemoveAt(2);
                Destroy(fish);
            });
        }

        if(Lootbox1 != null && Lootbox2 != null && Lootbox3 != null){
            Lootbox1.GetComponentInChildren<Button>().onClick.AddListener(() => {
                if(money >= 10){
                    money -= 10;
                    //buy brown lootbox
                }
            });

            Lootbox2.GetComponentInChildren<Button>().onClick.AddListener(() => {
                if(money >= 50){
                    money -= 50;
                    //buy orange lootbox
                }
            });

            Lootbox3.GetComponentInChildren<Button>().onClick.AddListener(() => {
                if(money >= 100){
                    money -= 100;
                    //buy purple lootbox
                }
            });
        }

        if (SellButton != null)
        {
            SellButton.GetComponent<Button>().onClick.AddListener(() =>
            {
                money += ReturnTotalPrice();
                allFish.Clear();
            });
        }

        if (BuySceneButton != null)
        {
            BuySceneButton.GetComponent<Button>().onClick.AddListener(() =>
            {
                updateFish();
                UnityEngine.SceneManagement.SceneManager.LoadScene("BuyMenu");
            });
        }

        if (FishingButton != null)
        {
            FishingButton.GetComponent<Button>().onClick.AddListener(() =>
            {
                updateFish();
                UnityEngine.SceneManagement.SceneManager.LoadScene("SampleScene");
            });
        }

        if(BuyBaitButton != null){
            BuyBaitButton.GetComponent<Button>().onClick.AddListener(() => {
                if(money >= 50){
                    money -= 50;
                    bait += 5;
                }
            });
        }


    }

    void updateFish(){
        for(int i = 0; i < allFish.Count; i++){
            DontDestroyOnLoad(allFish[i]);
        }
    }
}
