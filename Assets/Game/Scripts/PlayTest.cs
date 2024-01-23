using UnityEngine;
using System.Collections;
using GDGeek;

public class PlayTest : MonoBehaviour {

	// Use this for initialization
	void Start () {
		TaskList tl = new TaskList ();

        tl.push(PlayView.Instance._ui.openUI());
        tl.push(PlayView.Instance._time.open(0.5f));
        tl.push(open());
        tl.push(open());
        tl.push (PlayView.Instance._ui.right ());
		tl.push (PlayView.Instance._ui.wrong ());
        tl.push(PlayView.Instance.close());
        tl.push(PlayView.Instance._ui.closeUI());
        tl.push(PlayView.Instance._time.close(0.5f));
        //tl.push(GameView.Instance.roundOver());

        
        TaskManager.Run (tl);

	}
	Task open(){
		TaskList tl = new TaskList ();
		Task o = PlayView.Instance.open ();
		TaskManager.PushFront (o, delegate() {
			//Debug.Log("!!!!");
			var data = PlayModel.Instance.data;
			var diff = VoxelStruct.Different (data.first, data.second);
			PlayView.Instance.setup(data.first, data.second, VoxelStruct.Create(diff,Color.white));
		});
		tl.push(o);
		return tl;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
