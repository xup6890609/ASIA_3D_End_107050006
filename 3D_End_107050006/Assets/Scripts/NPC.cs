using UnityEngine;
using UnityEngine.UI;
using System.Collections;           //引用系統.集合(協同程序)

public class NPC : MonoBehaviour
{
    [Header("NPC資料")]
    public NPCData data;
    [Header("對話框")]
    public GameObject dialogue;
    [Header("對話內容")]
    public Text textContent;
    [Header("對話者名稱")]
    public Text textName;
    [Header("對話間隔")]
    public float interval = 0.1f;

    /// <summary>
    /// 玩家是否進入感應區
    /// </summary>
    public bool playerInArea;
    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "艾爾")
        {
            playerInArea = true;
            StartCoroutine(Dialogue());
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.name == "艾爾")
        {
            playerInArea = false;
        }
    }
    /// <summary>
    /// 停止對話
    /// </summary>
    private void StopDialogue() 
    {
        dialogue.SetActive(false);
        StopAllCoroutines();
    }
  
    /// <summary>
    /// 開啟對話
    /// </summary>
    /// StartCoroutine:啟動協程
    /// <summary>
    /// IEnumerator:協同程序
    /// </summary>
    private IEnumerator Dialogue()
    {
        dialogue.SetActive(true);                           //顯示對話框
        textContent.text = "";                              //清空對話框文字
        textName.text = name;


        for (int i = 0; i < data.dialogueA.Length; i++)     //字串長度       //for迴圈：重複處理相同程式
        {
            textContent.text += data.dialogueA[i] + "";     //將文字串聯
            yield return new WaitForSeconds(interval);
        }
    }
}
