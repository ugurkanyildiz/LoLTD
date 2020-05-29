using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu]
public class Wave : ScriptableObject
{
    public EnemyType enemyType;
    public int amount;
    public float statMultiplier;


}
public enum EnemyType { Fast, Medium, Slow }

public class Spawner : MonoBehaviour
{
    [SerializeField] GameObject enemy; //oluşturulacak enemy prefabi referansı
    public GameObject[] enemies; //Enemyler için bir liste oluşturduk
    public static Spawner instance;
    [SerializeField] Enemy[] enemyTypes;
    public float byteCoin = 50;
    [SerializeField] Text goldText;
    public List<Wave> waves = new List<Wave>();
    [SerializeField] Image timerBar;
    [SerializeField] Image timerText;
    [SerializeField] GameObject levelEndPanel;
    [SerializeField] PlayerHealth playerHealth;
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        goldText.text = "" + byteCoin;

        timerBar.fillAmount = 1;
    }
    float timer = 0;
    void Update()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy"); //enemyleri listeye aldık
        if (!waveSpawning)
        {
            timer += Time.deltaTime;
            timerBar.fillAmount = (10 - timer) / 10;
        }
        if (isLevelEnded && enemies.Length == 0)
        {
            EndLevel();
        }
    }
    void EndLevel()
    {
        levelEndPanel.SetActive(true);
        levelEndPanel.GetComponent<PanelController>().SetStarLevel(playerHealth.health);

    }
    public void NextLevel()
    {
        LevelController.instance.NextLevel(playerHealth.health);
    }
    public void RetryLevel()
    {
        LevelController.instance.RetryLevel();
    }
    public void Exit()
    {
        LevelController.instance.ExitToMenu(playerHealth.health);
    }
    public void EditCoin(float amount)
    {
        byteCoin += amount;
        goldText.text = "" + byteCoin;
    }
    IEnumerator timerCor;
    IEnumerator WaveTimer()
    {
        isWaveEnded = false;
        yield return new WaitForSeconds(10);
        isWaveEnded = true;
    }
    public void EndWave()
    {
        isWaveEnded = true;
        timer = 0;
        timerBar.fillAmount = 1;
        StopCoroutine(timerCor);
    }
    bool isWaveEnded = false;
    bool waveSpawning = true;
    bool isLevelEnded = false;
    public void StartLevel()
    {
        StartCoroutine(Spawn());
    }
    IEnumerator Spawn() //bekleme yapabilmek için bir coroutine oluşturduk 
    {
        for (int i = 0; i < waves.Count; i++)
        {
            Debug.Log("Wave " + i + " Started");
            waveSpawning = true;
            for (int t = 0; t < waves[i].amount; t++)
            {

                GameObject enm = Instantiate(enemy, transform);
                enm.GetComponent<EnemyBehaviour>().myType = enemyTypes[(int)waves[i].enemyType];
                enm.GetComponent<EnemyBehaviour>().mult = waves[i].statMultiplier;
                yield return new WaitForSeconds(0.5f);

            }
            waveSpawning = false;
            if (timerCor != null)
            {
                StopCoroutine(timerCor);

            }
            timer = 0;
            timerBar.fillAmount = 1;
            timerCor = WaveTimer();
            StartCoroutine(timerCor);
            while (!isWaveEnded) yield return new WaitForSeconds(0.1f);
        }
        waveSpawning = true;
        isLevelEnded = true;
        Debug.Log("LEVEL ENDED");
    }
}
