using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class HttpManager : MonoBehaviour
{
    [SerializeField] Text[] nameArray;
    [SerializeField] Text[] scoreArray;

   [SerializeField]
    private string URL;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    public void ClickGetScores()
    {
        StartCoroutine(GetScores());
    }

    IEnumerator GetScores()
    {
        string url = URL + "/scores";
        UnityWebRequest www = UnityWebRequest.Get(url);

        yield return www.SendWebRequest();

        if (www.isNetworkError)
        {
            Debug.Log("NETWORK ERROR " + www.error);
        }
        else if(www.responseCode == 200){
            //Debug.Log(www.downloadHandler.text);
            Scores resData = JsonUtility.FromJson<Scores>(www.downloadHandler.text);

            foreach (ScoreData score in resData.scores)
            {
                Debug.Log(score.user_id + " | " + score.score);

            }
            for (int i = 0; i < resData.scores.Length; i++)
            {
                scoreArray[i].text = resData.scores[i].score.ToString();
                nameArray[i].text = resData.scores[i].user_id.ToString();
            }
        }
        else
        {
            Debug.Log(www.error);
        }

    }
}






[System.Serializable]
public class ScoreData
{
    public string user_id;
    public int score;

}

[System.Serializable]
public class Scores
{
    public ScoreData[] scores;
}
