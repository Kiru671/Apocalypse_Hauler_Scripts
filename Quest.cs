using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Quest", menuName = "Quest")]
public class Quest : ScriptableObject
{
    public string questName;
    public string description;
    public bool isComplete = false;
    public enum QuestType{destination, supplyRun, singleItem};
    public QuestType questType;
    public Vector3 targetDestination;
    public QuestManager questManager;
   
    public void Initiate() 
    {
        if (questManager == null)
        {
            questManager = GameObject.Find("QuestManager").GetComponent<QuestManager>();
        }
        Debug.Log("New Quest: " + questName);
        Debug.Log(description);
        questManager.StartCoroutine("CheckProgress");
    }


}
