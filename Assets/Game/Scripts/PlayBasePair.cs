using UnityEngine;
using System.Collections;

public class PlayBasePair : MonoBehaviour {
	public PlayBase _left;
	public PlayBase _right;
	public float all_ = 0;
	public bool _round = false;

	public void stop(){
		_round = false;
	}

    public void hide()
    {

        _left.gameObject.SetActive(false);
        _right.gameObject.SetActive(false);
        all_ = 0;
        var q = Quaternion.AngleAxis(all_, Vector3.down);
        _right.transform.localRotation = q;
        _left.transform.localRotation = q;

    }
    public void reset(){
		
		_left.gameObject.SetActive (true);
		_right.gameObject.SetActive (true);
		all_ = 0;
		var q = Quaternion.AngleAxis(all_, Vector3.down);
		_right.transform.localRotation = q;
		_left.transform.localRotation = q;

	}
	public void play(){
		_round = true;

	}
	// Update is called once per frame
	void Update () {
		if (_round) {
			all_ += Time.deltaTime * 20;
			var q = Quaternion.AngleAxis (all_, Vector3.down);
			_right.transform.localRotation = q;
			_left.transform.localRotation = q;
		}
	}

}
