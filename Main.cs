using UnityEngine;
using System.Collections;

public class Main : MonoBehaviour {
	
	public SmoothMoves.BoneAnimation TheGoodGuy_Anim;
	public GameObject TheGoodGuy_Ref;
	
	public int LastAnimIndex;
	
	// Use this for initialization
	void Start () {
		//	Invoke("Init", 3f);
		Init();
	}
	
	/// <summary>
	/// Init this instance.
	/// </summary>
	void Init() {
		LastAnimIndex = 1;
		TheGoodGuy_Anim.RegisterUserTriggerDelegate(CheckMyTag);
	}
	
	/// <summary>
	/// Plaies one random idle animation.
	/// </summary>
	void PlayRandomIdle() {
		int _tempIndex = Random.Range(1, 4);
		
		while (_tempIndex == LastAnimIndex) {
			Debug.Log(_tempIndex);
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
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
