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

	private bool isJump = false;
	private bool isSoundPlayed = false;

	private Animator ani;
	private Rigidbody2D rig;

	private GameManager manager;

	private void Start()
	{
		ani = GetComponent<Animator>();
		rig = GetComponent<Rigidbody2D>();

		manager = GameObject.Find("GameManager").GetComponent<GameManager>();
	}

	private void Update()
	{
		if (isDead)
		{
			ani.SetBool("Die", true);

			if (!isSoundPlayed)
			{
				manager.DeadSound();
				isSoundPlayed = true;
			}

			// 물리연산 중단
			rig.bodyType = RigidbodyType2D.Kinematic/*=중력이 필요하지 않은 물리연산을 진행 할 때 사용*/;
			rig.simulated = false; // 물리연산 진행 여부
		}
		else
		{
			isSoundPlayed = false;

			if (Input.GetKeyDown(KeyCode.Space))
			{
				if (rig.velocity.y == 0)
				{
					isJump = true;
					rig.AddForce(new Vector2(0, jumpPower));
				}
			}
		}
	}

	// 부딪힌 GameObject 비교 할 때 성능 차이: 이름 <<<<<<<<<<<<< 태그!
	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.tag == "Ground")
		{
			ani.SetBool("Jump", false);
			
			if (isJump)
			{
				manager.LandSound();
			}

			isJump = false;
		}
	}

	private void OnCollisionStay2D(Collision2D collision)
	{
		if (collision.gameObject.tag == "Ground")
		{
			ani.SetBool("Jump", false);
		}
	}

	private void OnCollisionExit2D(Collision2D collision)
	{
		if (collision.gameObject.tag == "Ground")
		{
			if (rig.velocity.y != 0)
			{
				ani.SetBool("Jump", true);
				manager.JumpSound();
			}
		}
	}
}
