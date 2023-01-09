using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MoneyUIManager : MonoBehaviour
{
    [SerializeField] TMP_Text moneyText;

    GameManager gameManager => GameManager.Instance;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        moneyText.text = gameManager.money.ToString();
    }
}
