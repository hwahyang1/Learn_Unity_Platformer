using System.Collections;
using System.Collections.Generic;

using UnityEngine;

/*
 * [Class] Scrolling
 * 배경 GameObject의 이동을 제어합니다.
 */
public class Scrolling : MonoBehaviour
{
	public List<GameObject> scrollObject = new List<GameObject>();

	public List<GameObject> prefab = new List<GameObject>();

	public bool usingRandom = false; // Y좌표 + prefab 랜덤

	public bool forceSpawn = false; // 스폰할 때 고정 위치에서 스폰할지

	public float initY = 0f;
	public float destroyX = -13.5f;
	public float spawnX = 13.5f;
	public float speed = 0.15f;
	public float distance = 4.5f; // 앞 GameObject와의 간격

	public float minY = 0f;
	public float maxY = 0f;

	private void Update()
	{
		if (scrollObject.Count == 0)
		{
			int index = 0;
			float initX = distance;

			if (forceSpawn)
			{
				initX = spawnX;
			}

			if (usingRandom)
			{
				index = Random.Range(0, prefab.Count);
				initY = Random.Range(minY, maxY);
			}

			GameObject spawn = Instantiate(prefab[index], new Vector3(initX, initY, 0), Quaternion.identity, gameObject.transform);
			scrollObject.Add(spawn);
		}
		else
		{
			if (scrollObject[0].transform.position.x <= destroyX)
			{
				GameObject target = scrollObject[0];

				// 문제 방지를 위해 List에서 먼저 제거
				scrollObject.Remove(target);
				Destroy(target);

				float newX = distance;

				if (scrollObject.Count > 0)
				{
					newX = scrollObject[scrollObject.Count - 1].transform.position.x + distance;
				}

				int index = 0;

				if (forceSpawn)
				{
					newX = spawnX;
				}

				if (usingRandom)
				{
					index = Random.Range(0, prefab.Count);
					initY = Random.Range(minY, maxY);
				}

				GameObject spawn = Instantiate(prefab[index], new Vector3(newX, initY, 0), Quaternion.identity, gameObject.transform);
				scrollObject.Add(spawn);
			}
			else
			{
				foreach (GameObject target in scrollObject)
				{
					target.transform.Translate(-speed, 0, 0);
				}
			}
		}
	}
}
