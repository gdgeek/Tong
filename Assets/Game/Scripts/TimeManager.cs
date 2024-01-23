using UnityEngine;
using System.Collections;
using GDGeek;
public class TimeManager : MonoBehaviour {
    public PlayTime _time = null;
    public Vector3 _up;
    public Vector3 _down;
    // Use this for initialization\
    public Task close(float time) {
        TweenTask tt = new TweenTask(delegate () {
            return TweenPosition.Begin(this.gameObject, time, _down, Space.Self);
        });

        return tt;
    }

    public Task open(float time){
        TweenTask tt = new TweenTask(delegate () {
            return TweenPosition.Begin(this.gameObject, time, _up, Space.Self);
        });

        return tt;
    }
}
