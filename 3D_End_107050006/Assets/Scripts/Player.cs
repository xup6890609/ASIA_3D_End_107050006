using UnityEngine;
using Invector.vCharacterController;  //引用套件

public class Player : MonoBehaviour
{
    private float hp = 100;
    private Animator ani;
    /// <summary>
    /// 連擊次數
    /// </summary>
    private int atkCount;
    
    private float timer;
    [Header("連擊間隔時間"), Range(0, 3)]
    public float interval = 1;
    [Header("攻擊中心點")]
    public Transform atkPoint;
    [Header("攻擊長度"), Range(0f, 5f)]
    public float atkLength;
    [Header("攻擊力度"), Range(0, 500)]
    public float atk = 30;

    private void Awake()
    {
        ani = GetComponent<Animator>();
    }

    private void Update()
    {
        Attack();
    }

    /// <summary>
    /// 繪製圖示事件(僅在Unity內顯示)
    /// </summary>
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;                                               // 圖示.顏色
        Gizmos.DrawRay(atkPoint.position, atkPoint.forward * atkLength);        // 圖示.繪製設限(中心點,方向)
    }

    private void Attack()

    {
        if (atkCount < 3)
        {
            if (timer < interval)
            {
                timer += Time.deltaTime;

                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    atkCount++;
                    timer = 0;
                    ani.SetInteger("連擊", atkCount);
                }
            }
            else
            {
                atkCount = 0;
                timer = 0;

                if (Physics.Raycast(atkPoint.position, atkPoint.forward, out hit, atkLength, 1 << 9))        // 物理.射線碰撞(攻擊中心點前方，射線擊中的物件，攻擊長度，圖層)  // 圖層:1<< 圖層編號
                {
                    hit.collider.GetComponent<Enemy>().Damage(atk); //碰撞物件.取得元件<玩家>().受傷()
                }
            }
        }
        if (atkCount == 3) atkCount = 0;

        ani.SetInteger("連擊", atkCount);
        
    }
    /// <summary>
    /// 射線擊中的物件
    /// </summary>
    private RaycastHit hit;

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
    public void Dead()
    {
        ani.SetTrigger("死亡觸發");
        vThirdPersonController vt = GetComponent<vThirdPersonController>();
        vt.lockMovement = true;
        vt.lockRotation = true;
    }
   
}
