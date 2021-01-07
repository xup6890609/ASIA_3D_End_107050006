using UnityEngine;
using Invector.vCharacterController;  //引用套件

public class Player : MonoBehaviour
{
    private float hp = 100;
    private Animator ani;

    private void Awake()
    {
        ani = GetComponent<Animator>();
    }

    /// <summary>
    /// 受傷
    /// </summary>
    public void Damage()
    {
        hp -= 25;
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
