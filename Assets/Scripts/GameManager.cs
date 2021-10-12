using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;


    #region Public Variables
    public Pulpit pulpitPrefab;
    public Doofus doofus;
    public GameObject gameOverPanel;
    public TextMeshProUGUI scoreText;
    #endregion

    #region private Variables
    
  
    bool isgameOver;
    Pulpit latestPulpit, oldPulpit;
    List<Pulpit> pulpitsList;
    List<Vector3> usedPositionsList;
    int score;
    #endregion 

    #region Getter Setters

    public int Score
    {
        get => score;
        set
        {
            score = value;
            scoreText.text = "" + score;
        }
    }
    public bool IsGameOver
    {
        get => isgameOver;
        set
        {
            isgameOver = value;
            gameOverPanel.SetActive(isgameOver);
            Time.timeScale = isgameOver ? 0 : 1f;
        }
    }


    #endregion

    #region UnityMethods
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }
    void Start()
    {
        pulpitsList = new List<Pulpit>();
        for (int i = 0; i < 5; i++) //5 pool
        {
            Pulpit newPulpit = Instantiate<Pulpit>(pulpitPrefab, this.transform);
            newPulpit.gameObject.name = "" + i;
            newPulpit.gameObject.SetActive(false);
            pulpitsList.Add(newPulpit);
        }
        StartGame();
    }
    #endregion

    #region public Methods

    public void StartGame()
    {
        Score = 0;
        usedPositionsList = new List<Vector3>();
        SpawnPulpitRandom();
        doofus.transform.position = new Vector3(0, 1.5f, 0);
        doofus.transform.rotation = Quaternion.identity;
        doofus.myBody.WakeUp();
        IsGameOver = false;
    }
    public void GameOver()
    {
        IsGameOver = true;
        foreach (var item in pulpitsList)
        {
            StopCoroutine(item.ActivateTimer());
            item.gameObject.SetActive(false);
        }
        doofus.myBody.Sleep();
        oldPulpit = latestPulpit = null;

    }
    public void SpawnPulpitRandom()
    {
        if (latestPulpit)
        {
            oldPulpit = latestPulpit;
            Score++;
        }
        latestPulpit = GetObjectFromPool();

        latestPulpit.transform.position = GetUnusedPosition();
        latestPulpit.gameObject.SetActive(true);
    }
    #endregion

    #region Private methods

    Vector3 GetUnusedPosition()
    {
        int i = 0;
        Vector3 newPos = Vector3.zero;
        i = Random.Range(0, 4);
        if (oldPulpit)
        {
            switch (i)
            {
                case 0: //forward
                    newPos = oldPulpit.transform.forward * 9;
                    break;
                case 1://right
                    newPos = oldPulpit.transform.right * 9;
                    break;
                case 2://back
                    newPos = -oldPulpit.transform.forward * 9;
                    break;
                case 3://left
                    newPos = -oldPulpit.transform.right * 9;
                    break;
                default:
                    break;
            }

            newPos += oldPulpit.transform.position;
        }
        if (!usedPositionsList.Contains(newPos))
        {
            usedPositionsList.Add(newPos);
            return newPos;
        }

        return GetUnusedPosition();
    }
    Pulpit GetObjectFromPool()
    {
        return pulpitsList.Find(x => !x.gameObject.activeInHierarchy);
    }

    #endregion

}//class
