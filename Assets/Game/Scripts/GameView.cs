using UnityEngine;
using System.Collections;
using GDGeek;

public class GameView : Singleton<GameView> {
	public GameObject _axis = null;
	public GameObject _offset = null;
	public GameObject _board = null;
	public LogoTouch _logo = null;
    public Task open()
    {
	    TweenTask tt = new TweenTask(delegate () {
            return TweenRotation.Begin(_axis, 1.3f, Quaternion.AngleAxis(180, Vector3.left));
        });
        TaskManager.PushFront(tt, delegate () {
            _board.transform.SetParent(_axis.transform);
        });
        return tt;
    }


    public Task roundOver(float time = 1.3f)
    {
	    TweenTask tt = new TweenTask(delegate () {
            return TweenRotation.Begin(_offset, time, Quaternion.AngleAxis(0, Vector3.left));
        });
        TaskManager.PushFront(tt, delegate () {
            _board.transform.SetParent(_offset.transform);
            _offset.transform.localRotation = Quaternion.AngleAxis(180, Vector3.left);
            _logo.gameObject.SetActive(true);
        });

        return tt;
    }
    public Task round(float time = 1.3f){
	    TweenTask tt = new TweenTask (delegate() {
			return TweenRotation.Begin(_offset, time, Quaternion.AngleAxis(180, Vector3.right));
		});
		TaskManager.PushFront (tt, delegate() {
			_board.transform.SetParent(_offset.transform);	
			_offset.transform.localRotation =Quaternion.AngleAxis(0, Vector3.right);
		});

		return tt;
	}
	public Task close(){
		TweenTask tt = new TweenTask (delegate() {
			return TweenRotation.Begin(_axis, 1.3f, Quaternion.AngleAxis(0, Vector3.left));
		});
		TaskManager.PushFront (tt, delegate() {
			_board.transform.SetParent(_axis.transform);	
		});
		return tt;
	}
}
