using UnityEngine;
using System.Collections;

public class CameraTouchDetection : MonoBehaviour {
	
	public GameObject[] notifiedObjects;
	public int numNotifiedObjects;
	
	public bool isTouched = false;
	public bool isDragging = false;
	
	private Camera mainCamera;
	private Vector3 touchPos;
	
	public bool gameStarted = false;
	public AudioSource backgroundAudio;
	
	public bool isPlayingInstrument = false;
	public bool isViewingForm = false;
	
	public GameObject leftBoundary;
	public GameObject rightBoundary;
	
	public GameObject stage;
	
	public GameObject form;
	
	public DeviceManager deviceManager;
	
	public PlayableBanjo pb;
	public PlayableCowbell pc;
	public PlayableDrum pd;
	public PlayableTambo pt;
	public SubmitButton sendToSproutButton;
	
	private float startTime;
	public AudioClip introAudio;
	private bool playedIntro = false;
	private float viewFormTime;
	//public BackgroundTexture backgroundTexture;
	//public Texture invisTexture;
	
	//private string backgroundTextureName;
	
	
	// Use this for initialization
	void Start () {
	
		mainCamera = this.gameObject.GetComponent<Camera>();
		
	}
	
	void Awake() {
	//	backgroundTextureName = backgroundTexture.gameObject.GetComponent<GUITexture>().texture.name;
		if(deviceManager == null)
		{
			deviceManager = GameObject.Find("DeviceManager").GetComponent<DeviceManager>();
		}
		if(stage == null)
		{
			stage = GameObject.FindGameObjectWithTag("stage");
		}
		form.SetActiveRecursively(true);
		form.SetActiveRecursively(false);
		
		/*if(banjo == null)
		{
			banjo = GameObject.Find("PlayableBanjo").GetComponent<PlayableBanjo>();
		}
		if(cowbell == null)
		{
			cowbell = GameObject.Find("PlayableCowbell").GetComponent<PlayableCowbell>();
		}
		if(drum == null)
		{
			drum = GameObject.Find("PlayableDrum").GetComponent<PlayableDrum>();
		}
		if(tambo == null)
		{
			tambo = GameObject.Find("PlayableTambo").GetComponent<PlayableTambo>();
		}*/
		
		GameObject[] instruments = GameObject.FindGameObjectsWithTag("instrument");

		backgroundAudio = GameObject.Find("BackgroundAudio").GetComponent<AudioSource>();
		notifiedObjects = new GameObject[instruments.Length + 6];
		notifiedObjects[0] = GameObject.Find("HorizontalScrollbar");
		notifiedObjects[1] = GameObject.FindGameObjectWithTag("tamboPanel");
		notifiedObjects[2] = GameObject.FindGameObjectWithTag("cowbellPanel");
		notifiedObjects[3] = GameObject.FindGameObjectWithTag("drumPanel");
		notifiedObjects[4] = GameObject.FindGameObjectWithTag("banjoPanel");
		notifiedObjects[5] = GameObject.FindGameObjectWithTag("submitButton");
		for(int i = 6; i < instruments.Length+6; i++)
		{
			notifiedObjects[i] = instruments[i-6];
		}
		numNotifiedObjects = notifiedObjects.Length;
		
		switch(deviceManager.deviceType)
		{
			case DeviceManager.DeviceType.iPad_retina:
				leftBoundary.transform.position = new Vector3(-7f, leftBoundary.transform.position.y, leftBoundary.transform.position.x);
				rightBoundary.transform.position = new Vector3(7f, rightBoundary.transform.position.y, rightBoundary.transform.position.x);
				stage.SetActiveRecursively(true);
				break;
			case DeviceManager.DeviceType.iPad_non_retina:
				leftBoundary.transform.position = new Vector3(-7f, leftBoundary.transform.position.y, leftBoundary.transform.position.x);
				rightBoundary.transform.position = new Vector3(7f, rightBoundary.transform.position.y, rightBoundary.transform.position.x);
				stage.SetActiveRecursively(true);
				break;
			case DeviceManager.DeviceType.iPhone5:
				leftBoundary.transform.position = new Vector3(-9f, leftBoundary.transform.position.y, leftBoundary.transform.position.x);
				rightBoundary.transform.position = new Vector3(9f, rightBoundary.transform.position.y, rightBoundary.transform.position.x);
				stage.SetActiveRecursively(true);
			
			break;
			case DeviceManager.DeviceType.iPhone_retina:
				leftBoundary.transform.position = new Vector3(-7f, leftBoundary.transform.position.y, leftBoundary.transform.position.x);
				rightBoundary.transform.position = new Vector3(7f, rightBoundary.transform.position.y, rightBoundary.transform.position.x);
				stage.SetActiveRecursively(true);
			break;
			case DeviceManager.DeviceType.iPhone_non_retina:
				leftBoundary.transform.position = new Vector3(-7f, leftBoundary.transform.position.y, leftBoundary.transform.position.x);
				rightBoundary.transform.position = new Vector3(7f, rightBoundary.transform.position.y, rightBoundary.transform.position.x);
				stage.SetActiveRecursively(true);	
			break;
			case DeviceManager.DeviceType.unknown:
				leftBoundary.transform.position = new Vector3(-7f, leftBoundary.transform.position.y, leftBoundary.transform.position.x);
				rightBoundary.transform.position = new Vector3(7f, rightBoundary.transform.position.y, rightBoundary.transform.position.x);
				stage.SetActiveRecursively(true);	
			break;
		}
		
		pb.LoadResources();
		pc.LoadResources();
		pd.LoadResources();
		pt.LoadResources();
		sendToSproutButton.LoadResources();
		startTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		
		if(!playedIntro && Time.time > startTime + 0.5f)
		{
			playedIntro = true;
			audio.clip = introAudio;
			audio.Play();
		}
		if(audio.isPlaying)
		{
			backgroundAudio.volume = 0.5f;
		}
		else
		{
			backgroundAudio.volume = 1f;
		}
		if(gameStarted && !isPlayingInstrument && !isViewingForm)
		{
			/*Texture bgTexture = backgroundTexture.gameObject.GetComponent<GUITexture>().texture;
			if(bgTexture == null || bgTexture == invisTexture)
			{
				backgroundTexture.gameObject.GetComponent<GUITexture>().texture = (Texture)Resources.Load("MyTextures/screens/"+backgroundTextureName, typeof(Texture));
			}*/
			if(Debug.isDebugBuild)
			{
				MouseControls();
			}
			else
			{
				TouchControls();
			}
		}
		else if(!gameStarted)
		{
			InitUnloadResources();
			gameStarted = true;
		}
		
		if(gameStarted && !isPlayingInstrument && isViewingForm)
		{
			if(Input.GetMouseButtonDown(0) || Input.touchCount > 0)
			{
				if(Time.time > viewFormTime + 0.5f)
				{
					sendToSproutButton.GetComponent<AudioSource>().Stop();
				}
			}
		}
		
		
		/*if(gameStarted && isPlayingInstrument)
		{
			Texture bgTexture = backgroundTexture.gameObject.GetComponent<GUITexture>().texture;
			if(bgTexture != null)
			{
				Resources.UnloadAsset(backgroundTexture.gameObject.GetComponent<GUITexture>().texture);
				backgroundTexture.gameObject.GetComponent<GUITexture>().texture = invisTexture;
			}
		}*/
		
		
	}
	
	 void InitUnloadResources()
	{
		Resources.UnloadUnusedAssets();
	}
	
	public void StartGame()
	{
		gameStarted = true;
		backgroundAudio.Play();
	}
	
	void MouseControls()
	{
		if(Input.GetMouseButtonDown(0))
		{
			audio.Stop ();
			RaycastHit hit;
			Ray ray = mainCamera.ScreenPointToRay (Input.mousePosition);
			/*int noteLayer = LayerMask.NameToLayer("notified");
			int panelLayer = LayerMask.NameToLayer("panel");
			int noteLayerMask = 1 << noteLayer;
			int panelLayerMask = 1 << panelLayer;
			int finalMask = noteLayerMask | panelLayerMask;*/
			if (!Physics.Raycast (ray, out hit, 10000)) return;
			touchPos = mainCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
			isTouched = true;
			foreach(GameObject notifiedObj in notifiedObjects)
			{
				
				if(hit.transform.gameObject == notifiedObj.transform.gameObject)
				{ 
					Debug.Log ("Notify selected " + notifiedObj.transform.gameObject  + " at " + Time.time);
					notifiedObj.transform.gameObject.SendMessage("NotifySelected", touchPos, SendMessageOptions.DontRequireReceiver);
				}
				if(hit.transform.gameObject.name == sendToSproutButton.transform.name)
				{
					viewFormTime = Time.time;
				}
					
			}
			
			
			
             //Debug.Log ("Hit " + hit.transform.name + " detected at " + Time.time);
			
		}
		
		if(Input.GetMouseButtonUp(0))
		{
			foreach(GameObject notifiedObj in notifiedObjects)
			{
				notifiedObj.SendMessage("NotifyMouseUp", touchPos, SendMessageOptions.DontRequireReceiver);
			}
			isTouched = false;
		}
		
		if(Input.GetMouseButton(0))
		{
			if(isTouched)
			{
				touchPos = mainCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
				foreach(GameObject notifiedObj in notifiedObjects)
				{
					//Debug.Log ("notified obj " + notifiedObj.name);
					notifiedObj.SendMessage("NotifyMouseDrag", touchPos, SendMessageOptions.DontRequireReceiver);
				}
			}
		}
	}

	
	void TouchControls()
	{
		if(Input.touchCount > 0)
		{
			audio.Stop ();
			foreach (Touch touch in Input.touches)
        	{
				//began touch
				if (touch.phase == TouchPhase.Began)
            	{
					
					//touched detected for the first time
						RaycastHit hit;
						Ray ray = Camera.main.ScreenPointToRay (new Vector3(touch.position.x, touch.position.y, 0));
						if (!Physics.Raycast (ray, out hit, 10000))
						{
							return;
						}
              
						touchPos = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, 0));
						foreach(GameObject notifiedObj in notifiedObjects)
						{
				
							if(hit.transform.gameObject == notifiedObj.transform.gameObject)
							{ 
								Debug.Log ("Notify selected " + notifiedObj.transform.gameObject  + " at " + Time.time);
								notifiedObj.transform.gameObject.SendMessage("NotifySelected", touchPos, SendMessageOptions.DontRequireReceiver);
							}
							if(hit.transform.gameObject.name == sendToSproutButton.transform.name)
							{
								viewFormTime = Time.time;
							}
					
						}
						isTouched = true;
				}
				//moved finger
				else if(touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary)
				{
					//a touch was moved, but only do somehting if we are touched
					if(isTouched)
					{
						touchPos = mainCamera.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, 0));
						foreach(GameObject notifiedObj in notifiedObjects)
						{
							notifiedObj.SendMessage("NotifyMouseDrag", touchPos, SendMessageOptions.DontRequireReceiver);
						}
					}
				}
				else if(touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
				{
					foreach(GameObject notifiedObj in notifiedObjects)
					{
						notifiedObj.SendMessage("NotifyMouseUp", touchPos, SendMessageOptions.DontRequireReceiver);
					}
					isTouched = false;
				}
				
			}         
		
		}
	}
		
	
}
