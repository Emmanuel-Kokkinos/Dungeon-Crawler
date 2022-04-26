using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Level : MonoBehaviour
{
    public Text timerText;
    [SerializeField] float time = 30f;

    // Start is called before the first frame update
    void Start()
    {
        timerText.text = time.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        time -= Time.deltaTime;
        timerText.text = time.ToString("F"); // 2 decimal places

        if (time <= 0)
        {
            EndLevel();
        }

    }

    void EndLevel()
    {
        SceneManager.LoadScene("Shop");
    }
}
