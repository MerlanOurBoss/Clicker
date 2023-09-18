using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ButtonsForHit : MonoBehaviour
{
    public GameObject[] buttons;
    public TextMeshProUGUI buttonText;
    void Update()
    {
        buttons[Game.Instance.pl.i].SetActive(true);

        
        if (Language.Instance.CurrentLanguage == "en")
        {
            buttonText.text = "New girl for " + Game.Instance.pl.priceForBuy;
        }
        else if (Language.Instance.CurrentLanguage == "ru")
        {
            buttonText.text = "Новая одежда за " + Game.Instance.pl.priceForBuy;
        }
        if (Game.Instance.pl.i == 10)
        {
            buttons[0].SetActive(true);
            Game.Instance.pl.i = 0;
        }

    }

    public void changeButtons()
    {
        if (Game.Instance.pl.currentScore >= Game.Instance.pl.priceForBuy)
        {
            Game.Instance.pl.currentScore -= Game.Instance.pl.priceForBuy;
            buttons[Game.Instance.pl.i].SetActive(false);
            Game.Instance.pl.i++;
            buttons[Game.Instance.pl.i].SetActive(true);
            Game.Instance.pl.priceForBuy *= Random.Range(2, 5);
        }
    }

    public void DeleteAllGirl()
    {
        buttons[Game.Instance.pl.i].SetActive(false);
        Game.Instance.pl.priceForBuy = 150;
        Game.Instance.pl.i = 0;
        buttons[0].SetActive(true);
    }
}
