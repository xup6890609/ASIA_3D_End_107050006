using UnityEngine;

/// <summary>
/// ScriptableObject:腳本化物件→將腳本變成物件保存在專案內
/// </summary>
[CreateAssetMenu(fileName ="NPC資料",menuName = "107050006/NPC資料")]         //建立資源選單("檔案名稱,選單名稱")
public class NPCData : ScriptableObject

{
    [Header("第一段對話"),TextArea(1,5)]
    public string dialogueA;
    [Header("第二段對話"),TextArea(1, 5)]
    public string dialogueB;
    [Header("第三段對話"),TextArea(1, 5)]
    public string dialogueC;
    [Header("第四段對話"),TextArea(1, 5)]
    public string dialogueD;
    [Header("任務要求數量")]
    public int count;
    [Header("已完成數量")]
    public int countCurrent;
}
