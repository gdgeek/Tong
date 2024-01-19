using UnityEngine;
using System.Collections;
using GDGeek;
using UnityEngine.SceneManagement;
public class TheTime{
    public float _allTime = 30;
	private float time_ = 30;

	public delegate void Function();
	public event Function onTimeOver;

	public float time{
		get{ 
			return time_;
		}
		set{
            time_ = value ;
		}
	}

	private void update(float d){
		if (time_ > 0f) {
            time_ -= Time.deltaTime;
			if (time_ <= 0f) {

                time_ = 0f;
				if (onTimeOver != null) {
					onTimeOver ();
				}
			} 

			PlayView.Instance._time._time.updateTime (time_, _allTime);
		
		}
	}
	public void open(){
		UpdateManager.Instance.onUpdate += update;
        time_ = _allTime;

    }
	public void close(){

		UpdateManager.Instance.onUpdate -= update;
	}
}
public class PlayLogic{
	private TheTime time_ = new TheTime();
    private bool timeup_ = false;
    private int score_ = 0;
    private FSM fsm_ = null;

	private string exit_ = null;
	public PlayLogic(FSM fsm, string exit){
		fsm_ = fsm;
		exit_ = exit;
		time_.onTimeOver += delegate() {
			fsm_.post("timeup");
		};
	}

	public State root(){
		var state =  new State();
		state.addAction("load:Play", "play_show");
        state.addAction("timeup", delegate
        {
            timeup_ = true;
        });
		return state;

	}
	public State loading(){

		var state =  new State();
		state.onStart += delegate() {

            time_.time = 15f;
            SceneManager.LoadSceneAsync("Play",LoadSceneMode.Additive);
		};

		return state;
	}

	public State show(){
		State state = TaskState.Create (delegate() {
			TaskSet ts = new TaskSet();
			ts.push(GameView.Instance.round());
			ts.push(PlayView.Instance.open());
            ts.push(PlayView.Instance._ui.openUI());
            ts.push(PlayView.Instance._time.open(1.3f));
            score_ = 0;
            timeup_ = false;
            PlayView.Instance._ui._score.setScore(score_);
            this.time_.open();

            return ts;
		}, this.fsm_, "play_input");
		state.onStart += delegate() {
			var data = PlayModel.Instance.data;
			var diff = VoxelStruct.Different (data.first, data.second);
			PlayView.Instance.setup(data.first, data.second, VoxelStruct.Create(diff,Color.white));
		};
		state.onOver += delegate() {
			GameView.Instance._logo.gameObject.SetActive(false);
		};
		return state;
	}
	public void doClick(bool insert){
		if (insert) {
			this.fsm_.post ("right");
		} else {

			this.fsm_.post ("wrong");
		}
	}

	public State input(){
		State state = new State ();
		state.onStart += delegate() {
			PlayView.Instance.onClick.AddListener(doClick);
		};
		state.onOver += delegate() {

			PlayView.Instance.onClick.RemoveListener(doClick);
		};
		state.addAction ("timeup", "play_over");
		state.addAction ("right", "play_right");
		state.addAction ("wrong", "play_wrong");
		return state;
	}

	public State next (){
		State state = TaskState.Create (delegate() {

			TaskSet ts = new TaskSet();
			ts.push(GameView.Instance.round(0.6f));
			ts.push(PlayView.Instance.open(0.6f));
			return ts;
		}, this.fsm_, delegate {
            if (timeup_) {
                return "play_over";
            } else {
                return "play_input";
            }
        } );

		state.onStart += delegate() {
			var data = PlayModel.Instance.data;
			var diff = VoxelStruct.Different (data.first, data.second);
			PlayView.Instance.setup(data.first, data.second, VoxelStruct.Create(diff,Color.white));
		};
		return state;
	}
	public State right(){
		State state = TaskState.Create (delegate() {
            score_++;
            PlayView.Instance._ui._score.setScore(score_);
			return PlayView.Instance._ui.right();
		}, this.fsm_, "play_next");

        return state;
	}

	public State over(){
		State state = TaskState.Create (delegate() {
            TaskSet ts = new TaskSet();
            ts.push(GameView.Instance.roundOver());
            ts.push(PlayView.Instance.close());
            ts.push(PlayView.Instance._ui.closeUI());
            ts.push(PlayView.Instance._time.close(1.3f));

            this.time_.close();
            return ts;
        }, this.fsm_, "start");


        state.onOver += delegate () {
            SceneManager.UnloadScene("Play");
        };

        return state;
	}
	public State wrong(){
		State state = TaskState.Create (delegate() {
			return PlayView.Instance._ui.wrong();
		}, this.fsm_, "play_next");

		return state;
	}

	
}
