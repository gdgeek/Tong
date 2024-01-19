using UnityEngine;
using System.Collections;
using GDGeek;
using UnityEngine.SceneManagement;

public class MainLogic : MonoBehaviour {
	public LogoLogic _logo;
	public PlayLogic _play;
	private FSM fsm_ = new FSM();

	State getLogo(){
		State state = new State ();
		state.onStart += delegate() {
		};

		return state;

	}
	State getLogoLoading(){
		State state = new State ();
		state.onStart += delegate() {
			SceneManager.LoadSceneAsync("Logo",LoadSceneMode.Additive);
		};

		return state;

	}

//	getLogoShow
	State getPlay(){
		State state = new State ();
		state.onStart += delegate() {
			SceneManager.LoadSceneAsync("Play",LoadSceneMode.Additive);
		
		};
		state.addAction("unload:Play", "play");

		return state;
	}
	public void Awake(){
	
		SceneManager.activeSceneChanged += delegate(Scene arg0, Scene arg1) {
			//Debug.Log("active scene changed:" + arg0.name + "," + arg1.name);
		};
		SceneManager.sceneLoaded += delegate(Scene arg0, LoadSceneMode arg1) {
			fsm_.post("load:" + arg0.name);
		//	Debug.Log("scene loaded:" + arg0.name + "," + arg1);
		};
		SceneManager.sceneUnloaded += delegate(Scene arg0) {
			fsm_.post("unload:" + arg0.name);
			//Debug.Log("scene unloaded:" + arg0.name);
		};
	}
    State start() {
        State state = new State();
        state.addAction("logotouch", "logo_loading");
        return state;
    }
	public void Start(){
        GameView.Instance._logo.onEnter += delegate
        {
            fsm_.post("logotouch");
        };
		this._logo = new LogoLogic (fsm_, "play_loading");
		this._play = new PlayLogic (fsm_, "end");



        fsm_.addState("start", start());
        fsm_.addState ("play", this._play.root());
		fsm_.addState ("play_loading", this._play.loading(), "play");
		fsm_.addState ("play_show", this._play.show(), "play");
		fsm_.addState ("play_input", this._play.input(), "play");
		fsm_.addState ("play_right", this._play.right(), "play");
		fsm_.addState ("play_wrong", this._play.wrong(), "play");
		fsm_.addState ("play_next", this._play.next(), "play");
		fsm_.addState ("play_over", this._play.over(), "play");
		fsm_.init ("start");
	}
	public void Update(){
//		Debug.Log ("!" +LogoView.Instance);
	}
}
