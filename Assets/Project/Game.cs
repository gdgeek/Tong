using UnityEngine;
using System.Collections;
using GDGeek;

public class Game : MonoBehaviour {
	private FSM fsm_ = new FSM ();
	private DataBuilding _data = null;


	State left(){
		UpateState state = new UpateState ();
		state.onUpdate += delegate(float d) {
			_data.transform.localEulerAngles += new Vector3(0, 70, 0) * d;
		};
		state.addAction ("rleft", "normal");
		state.addAction ("right", "right");
		return state;
	}
	State right(){
		UpateState state = new UpateState ();
		state.onUpdate += delegate(float d) {
			_data.transform.localEulerAngles += new Vector3(0, -70, 0) * d;
		};
		state.addAction ("rright", "normal");
		state.addAction ("left", "left");
		return state;
	}

	State normal(){
		var state = new State ();
		state.addAction ("left", "left");
		state.addAction ("right", "right");
		return state;
	}
	public void Start(){
		/*HedgehogTeam.EasyTouch.EasyTouch.On_TouchStart += delegate(HedgehogTeam.EasyTouch.Gesture gesture) {
			if(gesture.pickedObject != null){
				TaskManager.Run(_data.slected());
			}
			//Debug.Log(gesture.pickObject.name);
		};
	*/
		_data = Component.FindObjectOfType<DataBuilding> ();
		
		fsm_.addState ("left", left ());
		fsm_.addState ("right", right ());
		fsm_.addState ("normal", normal ());
		fsm_.init ("normal");

	}
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.LeftArrow)) {
			
			fsm_.post ("left");
		} else if(Input.GetKeyDown (KeyCode.RightArrow)){
			fsm_.post ("right");
		}
		//if(Input.GetMouseButtonDown())

		if (Input.GetKeyUp (KeyCode.LeftArrow)) {

			fsm_.post ("rleft");
		} else if(Input.GetKeyUp (KeyCode.RightArrow)){
			fsm_.post ("rright");
		}
	}
}
