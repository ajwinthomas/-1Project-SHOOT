using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyManager : MonoBehaviour
{
    [Header("Scene Name")]
    public string winingScene = "Winning";
    public int enemyCount;
    private int totalEnemyCount;
    public int killCount;
    void Start()
    {
        totalEnemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;

    }

    // Update is called once per frame
    void Update()
    {   
         enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length; 
       
         
        if(SceneManager.GetActiveScene().name == "Level" && enemyCount <= 0)
        {
            LoadWinningScene();
        }

    }

    private void LoadWinningScene()
    {
        SceneManager.LoadScene(winingScene);
    }
}
