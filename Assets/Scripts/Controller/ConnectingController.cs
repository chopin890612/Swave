using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ConnectingController : BaseController
{
    public GameObject connecting;
    public GameObject succeed;

    public GameObject fail;
    private void Start()
    {
        connecting.SetActive(true);
        succeed.SetActive(false);
        fail.SetActive(false);
        Invoke("Succeed", 2f);
    }
    private void OnEnable()
    {
        GameManager.GM.ConfirmEvent += Confirm;
    }
    private void OnDisable()
    {
        GameManager.GM.ConfirmEvent -= Confirm;
    }
    private void Confirm()
    {
        Debug.LogWarning("Confirm");
        if (succeed.activeSelf == true)
            ChangeScene(GameManager.Scenes.¿ï¾Ü¸}¦â);
    }
    private void Succeed()
    {
        connecting.SetActive(false);
        succeed.SetActive(true);
        fail.SetActive(false);
    }
}
