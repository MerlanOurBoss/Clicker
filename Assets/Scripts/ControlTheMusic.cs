using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlTheMusic : MonoBehaviour
{
    public Toggle tg;


    private void Start()
    {
        tg.isOn = Game.Instance.pl.music;
    }
    public void ChangeMusic()
    {
        Game.Instance.pl.music = tg.isOn;
    }
}
