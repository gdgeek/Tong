using UnityEngine;
using System.Collections;

public class Number : MonoBehaviour {

    public NumberMesh[] _numbers = null;
	// Use this for initialization
    public int number {
        set {
            int n = value;
            for (int i = 0; i < _numbers.Length; ++i) {
                int num = value;
                _numbers[i].number = n%10;
                n /= 10;
            }
        }
    }
	void Start () {
      //  number = 12;
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
