using UnityEngine;
using System.Collections;

public class NumberMesh : MonoBehaviour {

    public bool _debug = false;
    public int number{
        set
        {
            for (int i = 0; i < this.transform.childCount; ++i) {


                if (i == value)
                {
                    this.transform.GetChild(i).gameObject.SetActive(true);
                }
                else
                {
                   this.transform.GetChild(i).gameObject.SetActive(false);
                }
            }

        }    
        
    }
	// Use this for initialization
	void Awake() {

        this.number = 0;
    }

	

}
