using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingController : BaseController
{
    public Transform[] difficults;//0~3
    public Transform[] highWeight;//high 0 and weight 1
    public Steps nowStep = 0;
    public enum Steps
    {
        High = 0,
        Weight = 1,
        Difficult = 2
    }
    private Text[] highWeightText = new Text[2];
    private string[] high = new string[5]{"~150", "151~160", "161~170", "171~180", "181~" };
    private string[] weight = new string[5] { "~40", "41~60", "61~70", "71~90", "91~" };
    private int highIndex = 0;
    private int weightIndex = 0;
    private int diffIndex = 0;
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

        nowStep = Steps.High;
        ChangeDisplay(1);
        int index = 0;
        foreach(Transform i in highWeight)
        {
            highWeightText[index] = i.GetChild(3).GetComponent<Text>();
            index++;
        }
        highWeightText[0].text = high[0];
        highWeightText[1].text = weight[0];
        difficults[0].GetComponent<Outline>().enabled = true;

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
                i.GetChild(1).GetComponent<OutlineShine>().OnShine();
            }
            if(nowStep == Steps.High)
            {
                if (highIndex < 5 && highIndex > 0)
                {
                    highIndex--;
                    highWeightText[0].text = high[highIndex];                   
                }
            }
            if(nowStep == Steps.Weight)
            {
                if(weightIndex < 5 && weightIndex > 0)
                {
                    weightIndex--;
                    highWeightText[1].text = weight[weightIndex];
                }
            }
        }
        if (nowStep == Steps.Difficult)
        {
            if (diffIndex < 4 && diffIndex > 0)
            {
                diffIndex--;
                for (int i = 0; i < 4; i++)
                {
                    if (diffIndex == i)
                        difficults[diffIndex].GetComponent<Outline>().enabled = true;
                    else
                        difficults[i].GetComponent<Outline>().enabled = false;
                }
            }
        }
    }
    void Right()
    {
        if (nowStep == Steps.High || nowStep == Steps.Weight)
        {
            foreach (Transform i in highWeight)
            {
                i.GetChild(0).GetComponent<OutlineShine>().OnShine();
            }
            if (nowStep == Steps.High)
            {
                if (highIndex < 4 && highIndex > -1)
                {
                    highIndex++;
                    highWeightText[0].text = high[highIndex];
                }
            }
            if (nowStep == Steps.Weight)
            {
                if (weightIndex < 4 && weightIndex > -1)
                {
                    weightIndex++;
                    highWeightText[1].text = weight[weightIndex];
                }
            }
        }
        if(nowStep == Steps.Difficult)
        {
            if (nowStep == Steps.Difficult)
            {
                if (diffIndex < 3 && diffIndex > -1)
                {
                    diffIndex++;
                    for (int i = 0; i < 4; i++)
                    {
                        if (diffIndex == i)
                            difficults[diffIndex].GetComponent<Outline>().enabled = true;
                        else
                            difficults[i].GetComponent<Outline>().enabled = false;
                    }
                }
            }
        }
    }
}
