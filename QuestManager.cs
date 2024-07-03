using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.XR;

public class QuestManager : MonoBehaviour
{
    [SerializeField] List<Quest> questList = new List<Quest>();
    public Quest currentQuest;
    private int questIndex = 0;
    // Start is called before the first frame update
    void Start()
    {
        Quest[] FiledQuests = Resources.LoadAll<Quest>("Quests");
        for (int i = 0; i < FiledQuests.Length; i++)
        {
            questList.Add(FiledQuests[i]);        
        }
        currentQuest = questList[questIndex];
        currentQuest.Initiate();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P)) 
        {
            PreviousQuest();
        }
    }
    private void NextQuest() 
    {
        questIndex++;
        currentQuest = questList[questIndex];
        currentQuest.Initiate();
    }
    private void PreviousQuest() 
    {
        questIndex--;
        currentQuest = questList[questIndex];
        currentQuest.Initiate();
    }
    public IEnumerator CheckProgress()
    {      
        while (!currentQuest.isComplete)
        {
            Debug.Log("Check Tick");
            switch (currentQuest.questType)
            {
                case Quest.QuestType.destination:
                    //check if destination is reached, mark isComplete accordingly
                    break;
                case Quest.QuestType.supplyRun:
                    //check if all items are collected, mark isComplete accordingly
                    break;
                case Quest.QuestType.singleItem:
                    //check if specified item is returned, mark isComplete accordingly
                    break;
            }
            yield return new WaitForSeconds(1f);
        }
    }
}
