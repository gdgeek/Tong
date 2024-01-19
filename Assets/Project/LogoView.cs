using UnityEngine;
using System.Collections;
using GDGeek;

public class LogoView : GDGeek.Singleton<LogoView> {
	public GameObject _logo;
	public LogoInput _input;
	public Task show(){
		Task task = new TaskTween (delegate() {
			return TweenLocalPosition.Begin(_logo, 1.3f, new Vector3(-23, -56, 10));	
		});

		return task;
	}

	public Task hide(){
		Task task = new TaskTween (delegate() {
			return TweenLocalPosition.Begin(_logo, 1.3f, new Vector3(-23, -100, 10));	
		});

		return task;
	}
}
