using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

/*
 * [Class] GameManager
 * 게임의 전반적인 실행을 관리합니다.
 */
[RequireComponent(typeof(AudioSource))] // 해당 GameObject에 Component가 없을 경우 자동 추가
public class GameManager : MonoBehaviour
{
	public GameObject gameOver;

	public Image restart;

	public Sprite btnUp;
	public Sprite btnDown;

	/*
	 * [Note: Component] 유니티에서의 소리 구분
	 * AudioClip = 소리 파일 (Component 아님)
	 * AudioListener = 유니티 안에서 재생되는 소리를 사용자에게 들려주는 역할
	 * AudioSource = 유니티 소리 플레이어
	 * 
	 * AudioClip 찾음 -> AudioSource에서 재생 -> AudioListener가 전달
	 */
	public AudioClip jump;
	public AudioClip land;
	public AudioClip dead;
	public AudioClip button;

	public Text nowScoreText;
	public Text maxScoreText;

	public float scorePeriod = 1f;
	public int defaultScore = 10;

	private float nowScorePeriod = 0f;

	private int nowScore = 0;
	private int maxScore = 0;

	private AudioSource audioSource;

	private PlayerController player;

	private void Start()
	{
		player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
		audioSource = GetComponent<AudioSource>();

		maxScore = PlayerPrefs.GetInt("MaxScore", 0/*기본값*/);
		// TODO: 텍스트에 최고 점수 연결하기

		gameOver.SetActive(false);
	}

	private void Update()
	{
		if (player.isDead)
		{
			if (!gameOver.activeInHierarchy)
			{
				restart.sprite = btnUp;
				PlayerPrefs.SetInt("MaxScore", maxScore);
			}

			gameOver.SetActive(true);
		}
		else
		{
			if (nowScorePeriod >= scorePeriod)
			{
				nowScorePeriod = 0f;
				nowScore += defaultScore;
				// TODO: 현재 점수와 최고 점수 텍스트 연결하기

				if (nowScore >= maxScore)
				{
					maxScore = nowScore;
				}
			}
			else
			{
				nowScorePeriod += Time.deltaTime;
			}
		}
	}

	/*
	 * [Method] OnRestartButtonClicked(): void
	 * 게임을 다시 시작 할 수 있도록 설정합니다.
	 */
	public void OnRestartButtonClicked()
	{
		player.isDead = false;

		gameOver.SetActive(false);

		GameObject[] cactuses = GameObject.FindGameObjectsWithTag("Cactus");
		foreach (GameObject obj in cactuses)
		{
			Destroy(obj);
		}

		Rigidbody2D rig = player.GetComponent<Rigidbody2D>();

		rig.bodyType = RigidbodyType2D.Dynamic;
		rig.simulated = true;

		player.GetComponent<Animator>().SetBool("Die", false);

		audioSource.PlayOneShot(button);
		restart.sprite = btnDown; // ?
	}

	/*
	 * [Method] LandSound(): void
	 * 착지하는 효과음을 재생합니다.
	 */
	public void LandSound()
	{
		/*
		 * [Note: Method] AudioSource.Play(): void
		 * 독점 재생 (다른 소리가 재생 중인 경우 정지시키고 본인 소리 재생)
		 * 
		 * [Note: Method] AudioSource.PlayOneShot(AudioClip clip): void
		 * 중첩 재생 (다른 소리가 재생 중인 경우 같이 재생)
		 */
		audioSource.PlayOneShot(land);
	}

	/*
	 * [Method] JumpSound(): void
	 * 뛰는 효과음을 재생합니다.
	 */
	public void JumpSound()
	{
		audioSource.PlayOneShot(jump);
	}

	/*
	 * [Method] DeadSound(): void
	 * 죽는 효과음을 재생합니다.
	 */
	public void DeadSound()
	{
		audioSource.PlayOneShot(dead);
	}
}
