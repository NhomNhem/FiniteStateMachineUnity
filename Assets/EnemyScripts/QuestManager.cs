using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class QuestSystem : MonoBehaviour
{
    public static QuestSystem Instance;
    public List<Quest> quests = new List<Quest>();
    public int playerLevel = 1;
    public int experience = 0;

    public GameObject questPanel;
    public Button acceptButton;
    public Button completeButton;
    public Button cancelButton;
    public TMP_Text questInfoText;

    private Quest currentQuest;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (quests.Count > 0)
            {
                ShowQuestUI(quests[0]); // Hi?n th? nhi?m v? ??u tiên trong danh sách
            }
            else
            {
                Debug.Log("Không có nhi?m v? nào ?? hi?n th?!");
            }
        }
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        questPanel.SetActive(false);

        acceptButton.onClick.AddListener(() =>
        {
            if (currentQuest != null)
                AcceptQuest(currentQuest);
        });

        completeButton.onClick.AddListener(() =>
        {
            if (currentQuest != null)
                CompleteQuest(currentQuest);
        });

        cancelButton.onClick.AddListener(() =>
        {
            if (currentQuest != null)
                CancelQuest(currentQuest);
        });
    }


    public void ShowQuestUI(Quest quest)
    {
        currentQuest = quest;
        questInfoText.text = "Nhi?m v?: " + quest.questName + "\nYêu c?u c?p: " + quest.requiredLevel + "\nPh?n th??ng: " + quest.experienceReward + " XP";
        questPanel.SetActive(true);
    }

    public void AcceptQuest(Quest quest)
    {
        if (playerLevel < quest.requiredLevel)
        {
            Debug.Log("C?p ?? không ?? ?? nh?n nhi?m v?!");
            return;
        }

        if (!quests.Contains(quest))
        {
            quests.Add(quest);
            Debug.Log("?ã nh?n nhi?m v?: " + quest.questName);
        }
    }

    public void CompleteQuest(Quest quest)
    {
        if (quests.Contains(quest))
        {
            experience += quest.experienceReward;
            quests.Remove(quest);
            Debug.Log("Hoàn thành nhi?m v?! +" + quest.experienceReward + " XP");

            if (currentQuest == quest)
                questPanel.SetActive(false);
        }
    }

    public void CancelQuest(Quest quest)
    {
        if (quests.Contains(quest))
        {
            quests.Remove(quest);
            Debug.Log("Nhi?m v? b? h?y: " + quest.questName);

            if (currentQuest == quest)
                questPanel.SetActive(false);
        }
    }

}