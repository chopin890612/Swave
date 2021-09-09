using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectController : BaseController
{
    public Transform[] people;
    public int index = 2;
    private void Start()
    {
        people[index].GetComponent<Outline>().enabled = true;
    }
    private void OnEnable()
    {
        GameManager.GM.LeftEvent += SelectL;
        GameManager.GM.RightEvent += SelectR;
        GameManager.GM.ConfirmEvent += Confirm;
    }
    private void OnDisable()
    {
        GameManager.GM.LeftEvent -= SelectL;
        GameManager.GM.RightEvent -= SelectR;
        GameManager.GM.ConfirmEvent -= Confirm;
    }
    void SelectL()
    {
        if(index < 5 && index > 0)
        {
            index--;
            for(int i = 0; i < 5; i++)
            {
                if (index == i)
                    people[index].GetComponent<Outline>().enabled = true;
                else
                    people[i].GetComponent<Outline>().enabled = false; 
            }
        }
    }
    void SelectR()
    {
        if (index < 4 && index > -1)
        {
            index++;
            for (int i = 0; i < 5; i++)
            {
                if (index == i)
                    people[index].GetComponent<Outline>().enabled = true;
                else
                    people[i].GetComponent<Outline>().enabled = false;
            }
        }
    }
    void Confirm()
    {
        if(people[index].transform.GetChild(1).gameObject.activeSelf == false)
        {
            ChangeScene(GameManager.Scenes.³]©w¼Æ­È);
        }
        else
        {
            Debug.Log("LOCKED");
        }
    }
}
