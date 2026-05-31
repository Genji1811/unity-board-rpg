using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [Header("Message Feed")]
    public Transform messageContainer;
    public GameObject messagePrefab;

    [Header("Scroll")]
    public ScrollRect scrollRect;

    [Header("Settings")]
    public int maxMessages = 20;

    private List<GameObject> messageList =
        new List<GameObject>();

    void Awake()
    {
        if (instance == null)
            instance = this;
    }

    public void ShowMessage(string msg)
    {
        Debug.Log("UI Log: " + msg);

        GameObject item =
            Instantiate(messagePrefab, messageContainer);

        // hỗ trợ TMP nằm trong prefab hoặc child
        TextMeshProUGUI tmp =
            item.GetComponentInChildren<TextMeshProUGUI>();

        if (tmp == null)
        {
            Debug.LogError(
                "TextMeshProUGUI not found on MessageItem prefab"
            );
            Destroy(item);
            return;
        }

        tmp.text = msg;

        // ===== Color =====

        if (msg.Contains("Reward"))
        {
            tmp.color = Color.yellow;
        }
        else if (msg.Contains("Trap"))
        {
            tmp.color = Color.red;
        }
        else if (msg.Contains("won"))
        {
            tmp.color = Color.green;
        }
        else if (msg.Contains("lost"))
        {
            tmp.color = new Color(1f, 0.5f, 0f);
        }
        else if (msg.Contains("Challenge"))
        {
            tmp.color = Color.magenta;
        }
        else
        {
            tmp.color = Color.white;
        }

        messageList.Add(item);

        // ===== Max Messages =====

        while (messageList.Count > maxMessages)
        {
            Destroy(messageList[0]);
            messageList.RemoveAt(0);
        }

        // ===== Auto Scroll =====

        Canvas.ForceUpdateCanvases();

        if (scrollRect != null)
        {
            scrollRect.verticalNormalizedPosition = 0f;
        }
    }
}