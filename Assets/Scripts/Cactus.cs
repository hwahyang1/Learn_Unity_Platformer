using System.Collections;
using System.Collections.Generic;

using UnityEngine;

/*
 * [Class] Cactus
 * 선인장의 제거와 이동을 관리하고 플레이어와의 충돌을 감지합니다.
 */
public class Cactus : MonoBehaviour
{
	private CactusManager manager;

	private PlayerController player;

	private void Start()
	{
		manager = transform.parent.GetComponent<CactusManager>();

		player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
	}

	private void Update()
	{
		if (!player.isDead)
		{
			transform.Translate(manager.speed, 0, 0);

			if (transform.position.x <= -13.5f)
			{
				Destroy(gameObject);
			}
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.tag == "Player")
		{
			collision.gameObject.GetComponent<PlayerController>().isDead = true;
		}
	}
}
