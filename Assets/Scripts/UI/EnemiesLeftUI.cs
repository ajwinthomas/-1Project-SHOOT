using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnemiesLeftUI : MonoBehaviour
{
    [Header("GameManager Reference")]
    public EnemyManager enemyManager;
    private TextMeshProUGUI enemyCountUI;
       
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        enemyCountUI = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        enemyCountUI.text = enemyManager.enemyCount.ToString();
    }
}
