using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Money : MonoBehaviour
{
    public Text money;
    public static int total = 0;

    private void Awake()
    {
        setValue();
    }

    public void GainMoney(int value)
    {
        total += value;
        setValue();
    }

    public void SpendMoney(int value)
    {
        total -= value;
        setValue();
    }

    void setValue()
    {
        money.text = "$" + total.ToString();
    }
}
