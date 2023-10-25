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



        LoadScore();


    }

    public int current_score = 0;

    public void Score(int s)
    {
        current_score += s;
        TestUI_Donggil.instance.UpdateScore();
    }


    int index = 0;
    public void SaveScore()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            string filename = "Score" + index + ".json";
            filename = Path.Combine("TestJson/", filename);
            string toJson = JsonConvert.SerializeObject(new JsonTest(current_score, "AAA"), Formatting.Indented);
            
            File.WriteAllText(filename, toJson);
            Debug.Log("Save " + filename);
            index++;
        }
    }

    public List<JsonTest> test = new List<JsonTest>();
    public void LoadScore()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            JsonTest temp = new JsonTest(0, "");
            for (int i = 0; i < test.Count; i++)
            {
                var loadscore = JsonConvert.DeserializeObject<JsonTest>("Score" + i + ".json");
                test.Add(loadscore);
            }

            for (int i = 0; i < test.Count - 1; i++)
            {
                if (test[i].Score < test[i + 1].Score)
                {
                    temp = test[i];
                    test[i] = test[i + 1];
                    test[i + 1] = temp;
                }
                Debug.Log(test[i]);
                TestUI_Donggil.instance.UpdateRanking(i);
            }
        }
    }
}
[System.Serializable]
public class JsonTest
{
    public int Score;
    public string name;

    public JsonTest(int score, string name)
    {
        Score = score;
        this.name = name;
    }
}
