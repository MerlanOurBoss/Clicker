using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundChange : MonoBehaviour
{
    public Image sr;
    public Sprite[] background;
    

    public TextMeshProUGUI buttonText;

    void Update()
    {
        sr.sprite = background[Game.Instance.pl.i_background];

        if (Language.Instance.CurrentLanguage == "en")
        {
            buttonText.text = "New background for " + Game.Instance.pl.priceForBuyBackground;
        }
        else if (Language.Instance.CurrentLanguage == "ru")
        {
            buttonText.text = "Новое место за " + Game.Instance.pl.priceForBuyBackground;
        }
        if (Game.Instance.pl.i_background == 10)
        {
            sr.sprite = background[0];
            Game.Instance.pl.i_background = 0;
        }

    }

    public void changeButtons()
    {
        if (Game.Instance.pl.currentScore >= Game.Instance.pl.priceForBuyBackground)
        {
            Game.Instance.pl.currentScore -= Game.Instance.pl.priceForBuyBackground;
            Game.Instance.pl.i_background++;
            sr.sprite = background[Game.Instance.pl.i_background];
            Game.Instance.pl.priceForBuyBackground *= Random.Range(2, 5);
            
        }
    }

    public void DeleteAllBackground()
    {
        Game.Instance.pl.priceForBuyBackground = 50;
        Game.Instance.pl.i_background = 0;
    }
}
