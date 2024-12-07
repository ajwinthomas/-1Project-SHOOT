using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class KillCountUI : MonoBehaviour
{

    [Header("GameManager Reference")]
    public EnemyManager enemyManager;
    private TextMeshProUGUI KillCount;
    void Start()
    {
        KillCount = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        KillCount.text = enemyManager.killCount.ToString();
    }
}
