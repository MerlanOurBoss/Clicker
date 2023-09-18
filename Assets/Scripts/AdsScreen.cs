using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;

public class AdsScreen : MonoBehaviour
{

    [DllImport("__Internal")]
    private static extern void ShowAdv();

    public TextMeshProUGUI advScreenText;
    private float timer = 3;
    public bool pauseStart = false;


    // Update is called once per frame
    void Update()
    {
        
        if (timer >= 0)
        {
            timer -= (1 * Time.deltaTime);
        }
        else
        {
            pauseStart = false;
            timer = 3;
            gameObject.SetActive(false);
            ShowAdv();
        }
        int roundedValue = Mathf.RoundToInt(timer);

        if (Language.Instance.CurrentLanguage == "en")
        {
            advScreenText.text = "Advertising in " + roundedValue + " seconds";
        }
        else if (Language.Instance.CurrentLanguage == "ru")
        {
            advScreenText.text = "Реклама через " + roundedValue + " секунды";
        }
    }

    public void stopTheTime()
    {
        pauseStart = true;
        gameObject.SetActive(true);
    }
}
