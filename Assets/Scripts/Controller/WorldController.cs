using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorldController : BaseController
{
    public List<Transform> levels;
    public Transform confirmWin;
    public Transform settingWin;

    private int levelIndex = 0;
    private int index = 0;
    private void Start()
    {

        levels[levelIndex].GetChild(0).GetComponent<Outline>().enabled = true;
        confirmWin.transform.GetChild(index).GetComponent<Outline>().enabled = true;
    }
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
    void Confirm()
    {
        if (confirmWin.gameObject.activeSelf != true)
            confirmWin.gameObject.SetActive(true);
        else
        {
            if (index == 0)
            {
                ChangeScene(GameManager.Scenes.¹CÀ¸³õ´º);
            }
            else
                confirmWin.gameObject.SetActive(false);
        }
    }
    void Back()
    {
        if (confirmWin.gameObject.activeSelf != false)
            confirmWin.gameObject.SetActive(false);
    }
    void Left()
    {
        if (confirmWin.gameObject.activeSelf != true)
        {
            if (levelIndex < 10 && levelIndex > 0)
            {
                levelIndex--;
                levels[levelIndex].GetComponent<Animator>().Play("FLAG", 0, 0);
                for (int i = 0; i < 10; i++)
                {
                    if (levelIndex == i)
                        levels[levelIndex].GetChild(0).GetComponent<Outline>().enabled = true;
                    else
                        levels[i].GetChild(0).GetComponent<Outline>().enabled = false;
                }
            }
        }
        else if (index == 1)//When ConfirmWin active
        {
            index--;
            for (int i = 0; i < 2; i++)
            {
                if (i == index)
                {
                    confirmWin.transform.GetChild(i).GetComponent<Outline>().enabled = true;
                }
                else
                    confirmWin.transform.GetChild(i).GetComponent<Outline>().enabled = false;
            }
        }
    }
    void Right()
    {
        if (confirmWin.gameObject.activeSelf != true)
        {
            if (levelIndex < 9 && levelIndex > -1)
            {
                levelIndex++;
                levels[levelIndex].GetComponent<Animator>().Play("FLAG", 0, 0);
                for (int i = 0; i < 10; i++)
                {
                    if (levelIndex == i)
                        levels[levelIndex].GetChild(0).GetComponent<Outline>().enabled = true;
                    else
                        levels[i].GetChild(0).GetComponent<Outline>().enabled = false;
                }
            }
        }
        else if (index == 0)//When ConfirmWin active
        {
            index++;
            for (int i = 0; i < 2; i++)
            {
                if (i == index)
                {
                    confirmWin.transform.GetChild(i).GetComponent<Outline>().enabled = true;
                }
                else
                    confirmWin.transform.GetChild(i).GetComponent<Outline>().enabled = false;
            }
        }
    }
    public void OnSettingButtonClick()
    {
        settingWin.gameObject.SetActive(!settingWin.gameObject.activeSelf);
    }
}
