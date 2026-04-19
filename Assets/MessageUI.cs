using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [Header("Message Feed")]
    public Transform messageContainer;
    public GameObject messagePrefab;
    public float messageDuration = 3f;

    private List<GameObject> messageList = new List<GameObject>();

    void Awake()
    {
        if (instance == null)
            instance = this;
    }

    void Start()
    {
        ShowMessage("UI Ready"); // test
    }

    public void ShowMessage(string msg)
    {
        Debug.Log("ShowMessage: " + msg);

        GameObject item = Instantiate(messagePrefab, messageContainer);
        
        TextMeshProUGUI tmp = item.GetComponent<TextMeshProUGUI>(); // ✅ GetComponent thay vì GetComponentInChildren
        if (tmp == null)
        {
            Debug.LogError("TextMeshProUGUI not found on MessageItem prefab");
            return;
        }

        tmp.text = msg;
        messageList.Add(item);
        StartCoroutine(RemoveAfter(item, messageDuration));
    }

    IEnumerator RemoveAfter(GameObject item, float delay)
    {
        yield return new WaitForSeconds(delay);
        if (item != null)
        {
            messageList.Remove(item);
            Destroy(item);
        }
    }
}