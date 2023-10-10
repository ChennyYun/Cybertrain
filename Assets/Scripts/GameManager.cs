using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static GameManager Instance;

    public int difficulty = 1;
    public int level = 1;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void UpdateLevel(){
        level +=1;
        difficulty = (level / 3)+1;
    }

    public void StartGame(){
        level = 1;
        difficulty = 1;
        SceneManager.LoadScene("NormalTrain");
    }
}
