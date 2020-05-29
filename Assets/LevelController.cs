using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    public static LevelController instance;
    public int level = 0;
    private void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        DontDestroyOnLoad(this);
    }
    public void NextLevel(float hp)
    {
        string saveSlot = "Level" + level;
        level = SceneManager.GetActiveScene().buildIndex + 1;
        PlayerPrefs.SetInt(saveSlot,(int)hp);
        SceneManager.LoadScene(level);
    }
    public void StartLevel(int index)
    {
        level = index;
        SceneManager.LoadScene(level);

    }
    public void RetryLevel()
    {
        level = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(level);
    }
    public void ExitToMenu(float hp)
    {
        string saveSlot = "Level" + level;
        PlayerPrefs.SetInt(saveSlot, (int)hp);
        level = 0;
        SceneManager.LoadScene(level);
    }
}
