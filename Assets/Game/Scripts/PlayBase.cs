using UnityEngine;
using System.Collections;

public class PlayBase : MonoBehaviour {
	public GameObject _son = null;
	public GameObject _diff = null;
	public void setDiff(GameObject go){
		if (_diff != null) {
			GameObject.Destroy (_diff);
		}
		_diff = go;
		_diff.transform.SetParent (transform);
		_diff.gameObject.transform.localEulerAngles = Vector3.zero;
        _diff.transform.localScale = Vector3.one;

    }
	public void setMesh(GameObject go){
		if (_son != null) {
			GameObject.Destroy (_son);
		}
		_son = go;
		_son.transform.SetParent (transform);
		_son.gameObject.transform.localEulerAngles = Vector3.zero;
        _son.transform.localScale = Vector3.one;

    }
}
