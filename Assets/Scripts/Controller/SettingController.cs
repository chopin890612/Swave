using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingController : BaseController
{
    public Transform[] difficults;//0~3
    public Transform[] highWeight;//high and weight
    public Steps nowStep = 0;
    public enum Steps
    {
        High = 0,
        Weight = 1,
        Difficult = 2
    }
    private string[] high = new string[5]{"~150", "151~160", "161~170", "171~180", "181~" };
    private string[] weight = new string[5] { "~40", "41~60", "61~70", "71~90", "91~" };
    private void OnEnable()
    {
        GameManager.GM.ConfirmEvent += Confirm;
        GameManager.GM.BackEvent += Back;
        GameManager.GM.LeftEvent += Left;
        GameManager.GM.RightEvent += Right;
    }
    private void OnDisable()
    {
        GameManager.GM.ConfirmEvent -= Confirm;
        GameManager.GM.BackEvent -= Back;
        GameManager.GM.LeftEvent -= Left;
        GameManager.GM.RightEvent -= Right;
    }
    private void Start()
    {
        #region Init

        nowStep = 0;
        ChangeDisplay(1);

        #endregion
    }
    void ChangeDisplay(int which)
    {
        for (int i = 0; i < 3; i++)
        {
            bool nowActive = highWeight[which].GetChild(i).gameObject.activeSelf;
            highWeight[which].GetChild(i).gameObject.SetActive(!nowActive);
        }
    }
    void Confirm()
    {
        if(nowStep == Steps.High)
        {
            ChangeDisplay(0);
            ChangeDisplay(1);
            nowStep = Steps.Weight;
        }
        else if(nowStep == Steps.Weight)
        {
            ChangeDisplay(1);
            nowStep = Steps.Difficult;
        }
        else if(nowStep == Steps.Difficult)
        {
            ChangeScene(GameManager.Scenes.¾Þ§@±Ð¾Ç);
        }
    }
    void Back()
    {
        if(nowStep == Steps.Weight)
        {
            ChangeDisplay(0);
            ChangeDisplay(1);
            nowStep = Steps.High;
        }
        else if(nowStep == Steps.Difficult)
        {
            ChangeDisplay(1);
            nowStep = Steps.Weight;
        }
    }
    void Left()
    {
        if(nowStep == Steps.High || nowStep == Steps.Weight)
        {
            foreach(Transform i in highWeight)
            {
                i.GetChild(0).GetComponent<OutlineShine>().OnShine();
            }
        }
        if (nowStep == Steps.Difficult)
        {

        }
    }
    void Right()
    {
        if (nowStep == Steps.High || nowStep == Steps.Weight)
        {
            foreach (Transform i in highWeight)
            {
                i.GetChild(1).GetComponent<OutlineShine>().OnShine();
            }
        }
        if(nowStep == Steps.Difficult)
        {

        }
    }
}
