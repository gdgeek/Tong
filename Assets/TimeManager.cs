using UnityEngine;
using System.Collections;
using GDGeek;
public class TimeManager : MonoBehaviour {
    public PlayTime _time = null;
    public Vector3 _up;
    public Vector3 _down;
    // Use this for initialization\
    public Task close(float time) {
        TaskTween tt = new TaskTween(delegate () {
            return TweenLocalPosition.Begin(this.gameObject, time, _down);
        });

        return tt;
    }

    public Task open(float time){
        TaskTween tt = new TaskTween(delegate () {
            return TweenLocalPosition.Begin(this.gameObject, time, _up);
        });

        return tt;
    }
}
