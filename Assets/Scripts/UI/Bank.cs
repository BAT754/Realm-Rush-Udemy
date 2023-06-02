using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Bank : MonoBehaviour
{
    [SerializeField] int startingBalance = 150;

    int currentBalance;
    public int CurrentBalance { get { return currentBalance; }}

    [SerializeField] TextMeshProUGUI balance;

    [SerializeField] Shop shop;

    private void Awake() {
        currentBalance = startingBalance;
        UpdateBalanceDisplay();
    }

    public void Deposit(int amount)
    {
        currentBalance += Mathf.Abs(amount);
        UpdateBalanceDisplay();
    }

    public void Withdraw(int amount)
    {
        currentBalance -= Mathf.Abs(amount);
        UpdateBalanceDisplay();

        if (currentBalance < 0)
            GameOver();
    }

    void GameOver()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.buildIndex);
    }

    void UpdateBalanceDisplay()
    {
        balance.text = "Gold: " + currentBalance;
        shop.CheckBankBalance(currentBalance);
    }



}
