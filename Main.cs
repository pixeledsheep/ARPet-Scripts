using UnityEngine;
using System.Collections;

public class Main : MonoBehaviour {
	
	private Plane GroundPlane;
	private float Dist;
	
	private Ray CameraRay;
	private Vector3 ClickPoint;
	
	public SmoothMoves.BoneAnimation TheGoodGuy_Anim;
	public GameObject TheGoodGuy_Ref;
	
	public int LastAnimIndex;
	
	// Use this for initialization
	void Start () {
		Init();
		//	Invoke("StartWalkingAnim", 3f);
		//	Invoke("StopWalkingAnim", 10f);
	}
	
	/// <summary>
	/// Init this instance.
	/// </summary>
	void Init() {
		GroundPlane = new Plane(Vector3.up, 0);
		
		LastAnimIndex = 1;
		TheGoodGuy_Anim.RegisterUserTriggerDelegate(CheckMyTag);
	}
	
	/// <summary>
	/// Plaies one random idle animation.
	/// </summary>
	void PlayRandomIdle() {
		int _tempIndex = Random.Range(1, 4);
		
		while (_tempIndex == LastAnimIndex) {
			//	Debug.Log(_tempIndex);
			_tempIndex = Random.Range(1, 4);
		}
		
		
		string _tempAnimName = "Idle_Clip_" + _tempIndex;
		
		TheGoodGuy_Ref.GetComponent<Animation>().animation.Play(_tempAnimName);
		LastAnimIndex = _tempIndex;
	}
	
	/// <summary>
	/// Checks animation tag.
	/// </summary>
	/// <param name='_userTriggerEvent'>
	/// _user trigger event.
	/// </param>
	void CheckMyTag(SmoothMoves.UserTriggerEvent _userTriggerEvent) {
		//	Debug.Log("UserTriggerEvent -> tag: " + _userTriggerEvent.tag);
		
		if (_userTriggerEvent.tag == "end_of_idle") {
			if (Random.Range(0, 100) > 75) {
				PlayRandomIdle();
			} else {
				TheGoodGuy_Ref.GetComponent<Animation>().animation.Play("Idle_Clip_1");
			}
		}
		
		if (_userTriggerEvent.tag == "start_walking") {
			TheGoodGuy_Ref.GetComponent<Animation>().animation.Play("Walk_2");
		}
		
		if (_userTriggerEvent.tag == "stop_walking") {
			TheGoodGuy_Ref.GetComponent<Animation>().animation.Play("Idle_Clip_1");
		}
	}
	
	/// <summary>
	/// Starts the walking animation.
	/// </summary>
	public void StartWalkingAnim() {
		TheGoodGuy_Ref.GetComponent<Animation>().animation.Play("Walk_1");
		
		Go.to(TheGoodGuy_Ref.transform, 2f, new GoTweenConfig()
					.position(ClickPoint)
					.setEaseType(GoEaseType.BackOut)
					.setDelay(1f / 3f)
					);
		
		Invoke("StopWalkingAnim", 2f);
	}
	
	/// <summary>
	/// Stops the walking animation.
	/// </summary>
	public void StopWalkingAnim() {
		TheGoodGuy_Ref.GetComponent<Animation>().animation.Play("Walk_3");
	}
	
	// Update is called once per frame
	void Update () {
		
		if(Input.GetMouseButtonUp(0)) {	
			CameraRay = GameObject.Find("ARCamera").camera.ScreenPointToRay(Input.mousePosition);
			
			if (GroundPlane.Raycast(CameraRay, out Dist)) {
				ClickPoint = CameraRay.GetPoint(Dist);
				
				StartWalkingAnim();
				Debug.Log("->	ClickPoint: " + ClickPoint);
			}
		}
	}
}
