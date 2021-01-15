using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    private float hp = 100;

    [Header("移動速度"), Range(0, 50)]
    public float speed = 4;
    [Header("停止距離"), Range(0, 50)]
    public float stopDistance = 2f;
    [Header("攻擊冷卻時間"), Range(0, 50)]
    public float CD = 2f;
    [Header("攻擊中心點")]
    public Transform atkPoint;
    [Header("攻擊長度"), Range(0f, 5f)]
    public float atkLength; 
    [Header("攻擊力度"), Range(0, 500)]
    public float atk = 30;

    private Transform player;
    private NavMeshAgent nav;
    private Animator ani;

    /// <summary>
    /// 計時器
    /// </summary>
    private float timer;

    private void Awake()
    {
        nav = GetComponent<NavMeshAgent>();                //取得身上元件<代理器>
        ani = GetComponent<Animator>();                    //取得身上元件<動作檔>
                                                        
        player = GameObject.Find("艾爾").transform;       //搜尋其他遊戲物件("物件名稱").變形元件

        nav.speed = speed;
        nav.stoppingDistance = stopDistance;
    }

    /// <summary>
    /// 繪製圖示事件(僅在Unity內顯示)
    /// </summary>
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;                                               // 圖示.顏色
        Gizmos.DrawRay(atkPoint.position, atkPoint.forward * atkLength);        // 圖示.繪製設限(中心點,方向)
    }

    private void Update()
    {
        Track();
        Attack();
    }
    /// <summary>
    /// 追蹤
    /// </summary>
    private void Track()
    {
        nav.SetDestination(player.position);                                    // 代理器.設定目的地(玩家座標)
        ani.SetBool("跑步開關", nav.remainingDistance > stopDistance);          // 動畫控制器.設定布林值("參數名稱",剩餘距離 >停止距離)
    }

    /// <summary>
    /// 射線擊中的物件
    /// </summary>
    private RaycastHit hit;

    /// <summary>
    /// 攻擊
    /// </summary>
    private void Attack()
    {
        if (nav.remainingDistance < stopDistance)
        {
            timer += Time.deltaTime;                    // 時間累加(一幀的時間)
            Vector3 pos = player.position;              // 取得玩家座標
            pos.y = transform.position.y;               // 將玩家座標y軸指定為本物件的y軸
            transform.LookAt(pos);                   // 看向(玩家座標)
                                                     // 如果計時器 >= 冷卻時間就攻擊且計時器時間歸零
            if (timer >= CD)
            {
                ani.SetTrigger("攻擊觸發");
                timer = 0;

                if (Physics.Raycast(atkPoint.position, atkPoint.forward, out hit, atkLength, 1 << 8))        // 物理.射線碰撞(攻擊中心點前方，射線擊中的物件，攻擊長度，圖層)  // 圖層:1<< 圖層編號
                {
                    hit.collider.GetComponent<Player>().Damage(atk); //碰撞物件.取得元件<玩家>().受傷()
                }
            }
        }
    }


    /// <summary>
    /// 受傷
    /// </summary>
    /// <param name="damage"></param>  //接收傷害值
    public void Damage(float damage)
    {
        hp -= damage;
        ani.SetTrigger("受傷觸發");
        if (hp <= 0)
        {
            Dead();
        }
    }

    /// <summary>
    /// 死亡
    /// </summary>
    private void Dead()
    {
        nav.isStopped = true;               //關閉導覽器
        enabled = false;                    //關閉腳本
        ani.SetBool("死亡開關",true);       //死亡動畫執行
    }
}
