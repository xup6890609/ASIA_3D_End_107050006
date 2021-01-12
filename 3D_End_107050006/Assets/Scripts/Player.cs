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

    private void Awake()
    {
        ani = GetComponent<Animator>();
    }

    private void Update()
    {
        Attack();
    }

    private void Attack()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            atkCount++;
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
    public void Dead()
    {
        ani.SetTrigger("死亡觸發");
        vThirdPersonController vt = GetComponent<vThirdPersonController>();
        vt.lockMovement = true;
        vt.lockRotation = true;
    }
   
}
