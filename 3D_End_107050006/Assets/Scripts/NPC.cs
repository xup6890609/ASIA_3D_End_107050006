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

    /// <summary>
    /// 定義列舉(下拉是選單)
    /// </summary>
    public enum NPCState
    {
        FirstDialogue,Counitune,Missioning,Finish
    }
    public NPCState state = NPCState.FirstDialogue;


    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "艾爾")
        {
            playerInArea = true;
            StartCoroutine(Dialogue());                 //啟用協程
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.name == "艾爾")
        {
            playerInArea = false;
            StopDialogue();
        }
    }
    /// <summary>
    /// 停止對話
    /// </summary>
    private void StopDialogue() 
    {
        dialogue.SetActive(false);                  //關閉對話框
        StopAllCoroutines();                        //關閉所有協程
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

        string dialogueString = data.dialogueA;             //要說的對話
        switch (state)
        {
            case NPCState.FirstDialogue:
                dialogueString = data.dialogueA;
                break; 
            case NPCState.Counitune:
                dialogueString = data.dialogueB;
                break;
            case NPCState.Missioning:
                dialogueString = data.dialogueC;
                break;
            case NPCState.Finish:
                dialogueString = data.dialogueD;
                break;
          
        }

        for (int i = 0; i < dialogueString.Length; i++)     //字串長度       //for迴圈：重複處理相同程式
        {
            textContent.text += dialogueString[i] + "";     //將文字串聯
            yield return new WaitForSeconds(interval);
        }
    }
}
