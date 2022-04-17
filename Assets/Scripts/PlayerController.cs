using System.Collections;
using System.Collections.Generic;

using UnityEngine;

/*
 * [Class] PlayerController
 * 플레이어의 움직임(점프, 생존 상태)를 제어합니다.
 */
public class PlayerController : MonoBehaviour
{
	public float jumpPower = 200f;

	public bool isDead = false;

	private Animator ani;
	private Rigidbody2D rig;

	private void Start()
	{
		ani = GetComponent<Animator>();
		rig = GetComponent<Rigidbody2D>();
	}

	private void Update()
	{
		if (isDead)
		{
			ani.SetTrigger("Die");
		}
		else
		{
			if (transform.position.y <= -0.485 && rig.velocity.y == 0)
			{
				ani.SetBool("Jump", false);
			}

			if (Input.GetKeyDown(KeyCode.Space))
			{
				if (rig.velocity.y == 0)
				{
					rig.AddForce(new Vector2(0, jumpPower));
					ani.SetBool("Jump", true);
				}
			}
		}
	}
}
