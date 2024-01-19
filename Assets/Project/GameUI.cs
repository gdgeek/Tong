using UnityEngine;
using System.Collections;
using GDGeek;

public class GameUI : MonoBehaviour {
	public GameObject _right = null;
	public GameObject _wrong = null;
    public AudioSource _rightSource = null;
    public AudioSource _wrongSource = null;
    public Score _score = null;

    public Task closeUI()
    {
        TaskTween tt = new TaskTween(delegate () {
            return TweenLocalPosition.Begin(this.gameObject, 1.3f, new Vector3(2, -55, 47));
        });

        return tt;

    }
    public Task openUI()
    {
        TaskTween tt = new TaskTween(delegate () {
            return TweenLocalPosition.Begin(this.gameObject, 1.3f, new Vector3(2, 1, 47));
        });

        return tt;

    }
    //  public PlayTime _time = null;
    public Task right(){
		Task task = new TaskWait (1.0f);
		float allTime = 0;
		TaskManager.AddUpdate (task, delegate(float d) {
			allTime += d*10;
			_right.SetActive(Mathf.Floor(allTime) %2 !=0);
		});
		TaskManager.PushBack (task, delegate() {
			_right.SetActive(false);
		});
        TaskManager.PushFront(task, delegate ()
        {
            _rightSource.Play();
        });
		return task;

	}
	public Task wrong(){
		Task task = new TaskWait (0.4f);
		float allTime = 0;
		TaskManager.AddUpdate (task, delegate(float d) {
			allTime += d*10;
			_wrong.SetActive(Mathf.Floor(allTime) %2 !=0);
		});
		TaskManager.PushBack (task, delegate() {
			_wrong.SetActive(false);
		});

        TaskManager.PushFront(task, delegate ()
        {
            _wrongSource.Play();
        });
        return task;
	}
}
