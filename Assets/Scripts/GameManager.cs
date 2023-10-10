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

    private string[] trainTypes = {"SmallTrain","NormalTrain","LongTrain"};

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
        difficulty = (level / 5)+1;
    }

    public void StartGame(){
        level = 1;
        difficulty = 1;
        SceneManager.LoadScene("NormalTrain");
    }

    public void NextLevel(){
        int randomIndex =Random.Range(0,3);
        SceneManager.LoadScene(trainTypes[randomIndex]);
    }
}
