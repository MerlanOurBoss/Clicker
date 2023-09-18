using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

[System.Serializable]
public class PlayerInfo
{
    public float currentScore;
    public float hitPower = 1;
    public float scoreIncreasedPerSecond = 1;
    public float x;

    public int shop1prize = 25;
    public int shop2prize = 125;

    public int amount1;
    public float amount1Profit;

    public int amount2;
    public float amount2Profit;

    public int upgradePrize = 500;
    public int allUpgradePrize = 1500;

    public int level;
    public int exp;
    public int expToNextLevel = 2;

    public int priceForBuy = 150;
    public int i;

    public int priceForBuyBackground = 50;
    public int i_background;

    public float timer = 60;

    public float extraN = 2;

    public bool music = false;
}
public class Game : MonoBehaviour
{
    public PlayerInfo pl;
    public static Game Instance;
    public AdsScreen ads;
    float n = 61;
 

    [DllImport("__Internal")]
    private static extern void SaveExtern(string date);

    [DllImport("__Internal")]
    private static extern void LoadExtern();

    [DllImport("__Internal")]
    private static extern void SetToLeaderboard(int value);

    [DllImport("__Internal")]
    private static extern void ShowAdv();

    [DllImport("__Internal")]
    private static extern void AddMoneyExtern();

    [DllImport("__Internal")]
    private static extern bool FirstTimePlay();

    public TextMeshProUGUI _playerInfo;

    //Clicker
    [Header("Clicker")]
    public TextMeshProUGUI scoreText;
    public AudioSource audi;
    public AudioSource audi2;

    //Shop
    [Header("Shop")]
    public TextMeshProUGUI shop1text;
    public TextMeshProUGUI shop2text;

    //Amount
    [Header("Amount")]
    public TextMeshProUGUI amount1Text;
    public TextMeshProUGUI amount2Text;


    //Upgrade
    [Header("Upgrade")]
    public TextMeshProUGUI upgradeText;
    public TextMeshProUGUI allUpgradeText;

    //LevelSystem
    [Header("LevelSystem")]
    public TextMeshProUGUI levelText;

    [Header("AdvAddMoney")]
    public TextMeshProUGUI advMoney;

    [Header("RandomEvent")]
    public bool nowIsEvent;
    public GameObject goldButton;

    [Header("ClickEffect")]
    public GameObject plusObject;
    public TextMeshProUGUI plusText;

    [Header("Audio")]
    public AudioSource[] audio;

    [Header("Gold Buttons")]
    public GameObject[] goldbuttons;
    private bool waitingForDeactivation = false;
    private GameObject currentGoldButton;

    private void Awake()
    {
        if (Instance == null)
        {
            transform.parent = null;
            DontDestroyOnLoad(gameObject);
            Instance = this;    
            LoadExtern();
            ShowAdv();
            PauseAudi();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        ActivateRandomObject();
    }
    void Update()
    {

        if (Language.Instance.CurrentLanguage == "en")
        {
            //Clicker
            scoreText.text = "Money: " + (int)pl.currentScore + " $";

            //Shop
            shop1text.text = "Buy Tier 1: " + pl.shop1prize + " $";
            shop2text.text = "Buy Tier 2: " + pl.shop2prize + " $";

            //Amount
            amount1Text.text = "Tier 1: " + pl.amount1 + " arts: " + pl.amount1Profit + " $/s";
            amount2Text.text = "Tier 2: " + pl.amount2 + " arts: " + pl.amount2Profit + " $/s";

            //Upgrade
            upgradeText.text = "Cost: " + pl.upgradePrize + " $";

            allUpgradeText.text = "Cost: " + pl.allUpgradePrize + " $";

            //Level
            levelText.text = pl.level + " level";

            //AdvMoney
            advMoney.text = pl.extraN + "x Money for adv";
        }
        else if (Language.Instance.CurrentLanguage == "ru")
        {
            //Clicker
            scoreText.text = "Деньги: " + (int)pl.currentScore + " $";

            //Shop
            shop1text.text = "Купить ур. 1: " + pl.shop1prize + " $";
            shop2text.text = "Купить ур. 2: " + pl.shop2prize + " $";

            //Amount
            amount1Text.text = "Уровень 1: " + pl.amount1 + " куплено: " + pl.amount1Profit + " $/сек";
            amount2Text.text = "Уровень 2: " + pl.amount2 + " куплено: " + pl.amount2Profit + " $/сек";

            //Upgrade
            upgradeText.text = "Стоит: " + pl.upgradePrize + " $";

            allUpgradeText.text = "Стоит: " + pl.allUpgradePrize + " $";

            //Level
            levelText.text = pl.level + " уровень";

            //AdvMoney
            advMoney.text = pl.extraN + "х Денег за рекламу";
        }
        if (!ads.pauseStart)
        {
            pl.scoreIncreasedPerSecond = pl.x * Time.deltaTime;
            pl.currentScore = pl.currentScore + pl.scoreIncreasedPerSecond;

            n -= (1 * Time.deltaTime);
        }

        //Level
        if (pl.exp >= pl.expToNextLevel)
        {
            pl.level++;
            pl.exp = 0;
            pl.expToNextLevel *= 2;
        }

        if (pl.timer == -0.001182675)
        {
            pl.timer = 0;
        }

        if (pl.timer > 0)
        {
            pl.timer -= (1 * Time.deltaTime);
        }
    }

    public void FixedUpdate()
    {
        if (n <= 0)
        {
            n = 61;
            ads.stopTheTime();
            PauseAudi();
        }
    }
    public void Save()
    {
        string jsonString = JsonUtility.ToJson(pl);
        SaveExtern(jsonString);
    }

    public void Leaderboard()
    {
        int curScore = (int)pl.currentScore;
        SetToLeaderboard(curScore);
    }

    public void SetPlayerInfo(string value)
    {
        pl = JsonUtility.FromJson<PlayerInfo>(value);
    }

    public void ShowAdvButton()
    {
        AddMoneyExtern();
    }

    public void AddMoney()
    {
        pl.currentScore *= pl.extraN;
        pl.extraN++;
    }

    public void DeleteData()
    {
        pl = new PlayerInfo();
        pl.hitPower = 1;
        pl.scoreIncreasedPerSecond = 1;
        pl.expToNextLevel = 2;
        pl.shop1prize = 25;
        pl.shop2prize = 125;
        pl.upgradePrize = 500;
        pl.allUpgradePrize = 1500;
        pl.priceForBuy = 150;
        pl.priceForBuyBackground = 50;
        pl.timer = 60;
        pl.music = false;
    }

    public void PauseAudi()
    {
        foreach (AudioSource aui in audio)
        {
            aui.Pause();
        }
    }
    public void UnPauseAudi()
    {
        foreach (AudioSource aui in audio)
        {
            aui.UnPause();
        }
     
    }

    public void Hit()
    {
        audi.Play();
        pl.currentScore += pl.hitPower;

        pl.exp++;

        Instantiate(plusObject, transform.position, transform.rotation);
    }

    public void Shop1()
    {
        if (pl.currentScore >= pl.shop1prize)
        {
            pl.currentScore -= pl.shop1prize;
            pl.amount1 += 1;
            pl.amount1Profit += 1;
            pl.x += 1;
            pl.shop1prize += 25;
        }
    }

    public void Shop2()
    {
        if (pl.currentScore >= pl.shop2prize)
        {
            pl.currentScore -= pl.shop2prize;
            pl.amount2 += 1;
            pl.amount2Profit += 5;
            pl.x += 5;
            pl.shop2prize += 125;
        }
    }

    public void Upgrade()
    {
        if (pl.currentScore >= pl.upgradePrize)
        {
            pl.currentScore -= pl.upgradePrize;
            pl.hitPower *= 2;
            pl.upgradePrize *= 3;
        }
    }

    public void AllProfitUpgrade()
    {
        if(pl.currentScore >= pl.allUpgradePrize && pl.amount1 > 0 && pl.amount2 > 0 )
        {
            pl.currentScore -= pl.allUpgradePrize;
            pl.x *= 2;
            pl.allUpgradePrize *= 3;
            pl.amount1Profit *= 2;
            pl.amount2Profit *= 2;
        }
    }

    public void GetReward()
    {
        audi2.Play();
        pl.currentScore = pl.currentScore + Random.Range(1, 500);
        nowIsEvent = false;
        DeactivateCurrentObject();       
    }
    private void ActivateRandomObject()
    {
        int randomIndex = Random.Range(0, goldbuttons.Length);
        currentGoldButton = goldbuttons[randomIndex];
        currentGoldButton.SetActive(true);
    }

    private void DeactivateCurrentObject()
    {
        if (currentGoldButton != null)
        {
            currentGoldButton.SetActive(false);
            waitingForDeactivation = true;
            Invoke("ResetDeactivationFlag", 10f);
        }
    }

    private void ResetDeactivationFlag()
    {
        waitingForDeactivation = false;
        ActivateRandomObject(); 
    }

}
