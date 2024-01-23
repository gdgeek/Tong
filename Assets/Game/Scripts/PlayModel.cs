using UnityEngine;
using System.Collections;
using GDGeek;
public class PlayModel : Singleton<PlayModel> {
	private GameData[] datas = null;
    private int index_ = 0;
	// Use this for initialization
	void Awake () {
		datas = this.gameObject.GetComponentsInChildren<GameData> ();

	}

	public GameData.Pair data{
		get{
            index_++;
            return datas [index_%datas.Length].data;
		}

	}
}
