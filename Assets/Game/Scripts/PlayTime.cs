using UnityEngine;
using System.Collections;

public class PlayTime : MonoBehaviour {
	public int _timeInt = 0;
	private Renderer[] renderer_ = null;
    public Number _number = null;
    private float per = 1.0f;
    void Start () {
		renderer_ = this.GetComponentsInChildren<Renderer> ();
	}
	

	void updateTime(){
		int i = 0;
		int n = Mathf.FloorToInt (per* renderer_.Length);
	//	Debug.Log (_timeInt);
		for (; i < n; ++i) {
			renderer_ [i].gameObject.SetActive (true);
		}
		for (; i < renderer_.Length; ++i) {
			renderer_ [i].gameObject.SetActive (false);
		}
	}
	public void updateTime(float time, float allTime){
        per = time / allTime;
        _number.number = Mathf.FloorToInt(time) % 100;

        int itime = Mathf.FloorToInt (time*100);
		if ( itime != _timeInt) {
			_timeInt = itime;
			updateTime ();
		}
	}

}
