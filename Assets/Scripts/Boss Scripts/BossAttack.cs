using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack : MonoBehaviour
{
  public Vector3 attackOffset;
  public float attackRange = 1f;
  public LayerMask attackMask;
  Boss boss;

  void Start() 
  {
    boss = GetComponent<Boss>();
  }
  public void Attack()
  {
    Vector3 pos = transform.position;
    pos+= transform.right*attackOffset.x;
    pos+=transform.up* attackOffset.y;
    if(boss.isInCameraRange)
    {
      SoundManagerScript.PlaySound("bossDeath");
    }
    Collider2D colInfo = Physics2D.OverlapCircle(pos,attackRange,attackMask);
    if(colInfo != null)
    {
        colInfo.GetComponent<playerMovement>().DamagedbyBoss();
    }
  }
    void OnDrawGizmosSelected()
{
	    Vector3 pos = transform.position;
		pos += transform.right * attackOffset.x;
		pos += transform.up * attackOffset.y;

		Gizmos.DrawWireSphere(pos, attackRange);
}
}
