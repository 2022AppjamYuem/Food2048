using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainManager : MonoBehaviour
{
    public TMP_Text coinText;

    private void Start()
    {
        coinText.text = GameManager.Instance.saveData.money.ToString();
    }

    public void GameStart()
    {
        SceneManager.LoadScene("GameScene");
    }
}
