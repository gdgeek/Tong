using UnityEngine;
using System.Collections;
using GDGeek;

public class Logic : MonoBehaviour {
	private DataBuilding data_ = null;
	private FSM fsm_ = new FSM ();
	private Different diff_ = null;
	State getNormal(){
		State state = new State ();
		state.addAction("enter", "different");
		return state;

	}
	State getRight(){

		State state = new State ();

		return state;
	}
	State getDifferent(){

		State state = new State ();

		state.onStart += delegate() {
			TweenValue.Begin(diff_.gameObject, 0.5f, 0,1, delegate(float v) {
				diff_.setAlpha(v);	
			});
		};
		state.onOver += delegate() {
			TweenValue.Begin(diff_.gameObject, 0.0f, 1,0, delegate(float v) {
				diff_.setAlpha(v);	
			});
		};
		state.addAction("exit", "normal");
		state.addAction ("click", delegate(FSMEvent evt) {
			TaskManager.Run(data_.slected());
			return "right";
		});
		return state;
			

	}
	// Use this for initialization
	void Start () {
		data_ = Component.FindObjectOfType<DataBuilding> ();
		diff_ = data_.gameObject.GetComponentInChildren<Different> ();
		diff_.onMouseEnter += delegate() {
			fsm_.post("enter");
		};
		diff_.onMouseExit += delegate(){
			fsm_.post("exit");
		};
		fsm_.addState("normal", getNormal());
		fsm_.addState ("different", getDifferent());
		fsm_.addState ("right", getRight());
		fsm_.init ("normal");
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (0)) {

			fsm_.post("click");
			Debug.Log ("11");
		}
	}
}
