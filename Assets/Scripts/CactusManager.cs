using System.Collections;
using System.Collections.Generic;

using UnityEngine;

/*
 * [Class] CactusManager
 * 선인장의 스폰을 관리합니다.
 */
public class CactusManager : MonoBehaviour
{
	public GameObject[] cactuses = new GameObject[3]; // cactus prefabs

	public float spawnCooldown = 3f;

	public float speed = 0.15f;

	private float nowCooldown = 0f;

	private PlayerController player;

	private void Start()
	{
		player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
	}

	private void Update()
	{
		if (!player.isDead)
		{
			if (nowCooldown >= spawnCooldown)
			{
				nowCooldown = 0f;

				// 0~2 -> 선인장 || 3 -> 아무것도 스폰 X
				int random = Random.Range(0, 4);

				if (random != 3)
				{
					GameObject cactus = Instantiate(cactuses[random], new Vector2(13.5f, -0.5f), Quaternion.identity, transform);
				}
			}
			else
			{
				nowCooldown += Time.deltaTime;
			}
		}
	}
}
