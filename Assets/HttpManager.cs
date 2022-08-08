using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class HttpManager : MonoBehaviour
{
    [SerializeField] Text[] Namearray;
    [SerializeField] Text[] Scorearray;

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
        string url = URL + "/leaders";
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

                Debug.Log(score.userId + " | " + score.value);

                Namearray[score.userId - 1].text = score.userId.ToString();

            }
            for (int i = 0; i < resData.scores.Length; i++)
            {

                Scorearray[i].text = resData.scores[i].value.ToString();

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
    public int userId;
    public int value;

}

[System.Serializable]
public class Scores
{
    public ScoreData[] scores;
}
