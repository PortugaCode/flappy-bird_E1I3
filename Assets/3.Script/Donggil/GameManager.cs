using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        SaveScore();
    }

    public int score = 0;

    public void Score(int i)
    {
        score += i;
        TestUI_Donggil.instance.UpdateScore();
    }

    public List<JsonTest> test = new List<JsonTest>();

    public void SaveScore()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            test.Add(new JsonTest(1, score, "AAAA"));
            string filename = "Test.json";
            filename = Path.Combine("TestJson/", filename);
            string toJson = JsonConvert.SerializeObject(test[0], Formatting.Indented);
            File.WriteAllText(filename, toJson);
        }
    }
}
[System.Serializable]
public class JsonTest
{
    public int Rank;
    public int Score;
    public string name;

    public JsonTest(int rank, int score, string name)
    {
        Rank = rank;
        Score = score;
        this.name = name;
    }
}
