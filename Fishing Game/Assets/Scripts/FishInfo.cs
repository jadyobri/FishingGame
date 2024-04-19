using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishInfo : MonoBehaviour
{
    public string quality = "Bad";
    public float weight = 0.0f;
    public float price = 0;

    public SpriteRenderer spriteRenderer;
    public Sprite[] fishSprites = new Sprite[4];

    private void Start()
    {
        //create the fish: determine the quality and weight of the fish based on the current rod type.
        quality = RandomFunctions.FishQuality(PlayerInteraction.rodQuality);
        weight = RandomFunctions.FishWeight(PlayerInteraction.rodQuality);

        //calculate the price of the fish based on those values
        price = ReturnPrice();

        //and then change the actual, visual sprite to reflect the quality of the fish.
        if(spriteRenderer == null)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }
        ChangeSpriteColor();
    }

    void ChangeSpriteColor()
    {
        switch (quality)
        {
            //Bad = Brown, Average = Green, Good = Orange, Super = Purple
            case "Bad":
                spriteRenderer.sprite = fishSprites[0];
                break;
            case "Average":
                spriteRenderer.sprite = fishSprites[1];
                break;
            case "Good":
                spriteRenderer.sprite = fishSprites[2];
                break;
            default: //Super
                spriteRenderer.sprite = fishSprites[3];
                break;
        }
    }

    float ReturnPrice()
    {
        float colorMult;
        switch(quality)
        {
            case "Bad":
                colorMult = 12.0f; //insert multiplier
                break;
            case "Average":
                colorMult = 30.0f; //insert multiplier
                break;
            case "Good":
                colorMult = 50.0f; //insert multiplier
                break;
            default:
                colorMult = 70.0f; //insert multiplier
                break;
        }

        //return colorMult * weight;
        return colorMult * 1;
    }
}
