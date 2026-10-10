using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragHander : MonoBehaviour , IBeginDragHandler , IDragHandler , IEndDragHandler {

	public static GameObject ItemBeingDragged;

	public GameObject CharacterObj;
	public GameObject BackgroundSlider;
	public GameObject FrameNoBuy;

	public Slider sliderCountDown;

	public float CountDown;

	public int CoinsBuy;

	public int PiratesID;

	private Vector3 startPosition;
	private Transform startParent;

	private float timer;
	private float currentCount;


	void Start()
	{
		timer = CountDown;
		currentCount = 0f;
		BackgroundSlider.SetActive (false);
	}

	void Update()
	{
		timer += Time.deltaTime;

		UpdateCountDown ();

		UpdateSlideCountDown ();

		UpdateBuyPirates ();
	}

	void UpdateCountDown()
	{
		if (timer >= CountDown) 
		{
			BackgroundSlider.SetActive (false);
		} 
		else 
		{
			BackgroundSlider.SetActive (true);
		}
	}

	void UpdateBuyPirates()
	{
		if (GameManager.Instance.CoinBuyPirates < CoinsBuy) 
		{
			FrameNoBuy.SetActive (true);
		} else 
		{
			FrameNoBuy.SetActive (false);
		}
	}

	void UpdateSlideCountDown()
	{
		currentCount -= Time.deltaTime;

		sliderCountDown.value = currentCount / CountDown;
	}

	#region IBeginDragHandler implementation

	public void OnBeginDrag (PointerEventData eventData)
	{
		AudioManager.Instance.PlayTakePirart (PiratesID);
		
		ItemBeingDragged = gameObject;
		startPosition = transform.position;
		startParent = transform.parent;
	}

	#endregion


	#region IDragHandler implementation
	public void OnDrag (PointerEventData eventData)
	{
		transform.position = Input.mousePosition;

		GameManager.Instance.IsDragCharacter = true;

		Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);

		RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);

		//If something was hit, the RaycastHit2D.collider will not be null.
		if (hit.collider != null) 
		{
			if (hit.collider.gameObject.tag == "LightWay") 
			{
				GameManager.Instance.LightWayName = hit.collider.name;
			}
		}
	}
	#endregion


	#region IEndDragHandler implementation

	public void OnEndDrag (PointerEventData eventData)
	{
		ItemBeingDragged = null;

		GameManager.Instance.IsDragCharacter = false;

		Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);

		RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);

		//If something was hit, the RaycastHit2D.collider will not be null.
		if (hit.collider != null) 
		{
			if (hit.collider.gameObject.tag == "LightWay" && GameManager.Instance.CoinBuyPirates >= CoinsBuy) 
			{
				Vector3 SpawnPosition = new Vector3 (hit.point.x, hit.collider.gameObject.transform.position.y , 0f);

				GameObject CharacterClone = (GameObject)Instantiate (CharacterObj, SpawnPosition, transform.rotation);

				timer = 0f;
				currentCount = CountDown;

				GameManager.Instance.LightWayName = "";

				GameManager.Instance.CoinBuyPirates -= CoinsBuy;

				sliderCountDown.gameObject.SetActive (true);

				AudioManager.Instance.PlayPiratAudio ();

				if (PlayerPrefs.GetInt ("FirtsPlay") == 0) 
				{
					UIManager.Instance.HideAllToturial ();

					PlayerPrefs.SetInt ("FirtsPlay", 1);

					UIManager.Instance.SpawnEnemyCurrentLevel ();

				}
			} 
		}

		transform.position = startPosition;
	}

	#endregion
}
