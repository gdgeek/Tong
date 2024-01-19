using UnityEngine;
using System.Collections;
using GDGeek;
using UnityEngine.SceneManagement;

public class LogoLogic {
	private FSM fsm_ = null;
	private string output_ = null;
	public LogoLogic(FSM fsm, string output){
		fsm_ = fsm;
		output_ = output;


		fsm_.addState ("logo", this.root());
		fsm_.addState ("logo_loading", this.loading(), "logo");
		fsm_.addState ("logo_show", this.show(), "logo");
		fsm_.addState ("logo_input", this.input(), "logo");
		fsm_.addState ("logo_hide", this.hide(), "logo");
	}
	public State root(){
		var state =  new State();
		state.addAction("load:Logo", "logo_show");
		return state;

	}

	public State loading(){
		
		var state =  new State();
		state.onStart += delegate() {

			SceneManager.LoadSceneAsync("Logo",LoadSceneMode.Additive);
		};
	
		return state;
	}
	public void doClick(){
		fsm_.post("click");
	}
	public State show(){
		State state = TaskState.Create (delegate() {
			TaskSet ts = new TaskSet();


			ts.push(GameView.Instance.open());
			ts.push(LogoView.Instance.show ());
			return ts;
		}, fsm_, "logo_input");

		return state;
	}
	public State hide(){
		State state = TaskState.Create (delegate() {
			TaskSet ts = new TaskSet();
			ts.push(GameView.Instance.close());
			ts.push(LogoView.Instance.hide ());
			return ts;
		}, fsm_, output_);
		state.onOver += delegate() {
			SceneManager.UnloadScene("Logo");
		};
		return state;

	}
	public State input(){

		State state = new State ("input");
		state.addAction ("click", "logo_hide");
		state.onOver += delegate() {

			//Debug.Log("over!!!");
			LogoView.Instance._input.onMouseClick -= doClick;
		};
		state.onStart += delegate() {
			//Debug.Log("start!!!");
			LogoView.Instance._input.onMouseClick += doClick;
		};
		return state;
	}
}
