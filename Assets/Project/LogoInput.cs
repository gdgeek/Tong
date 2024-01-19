using UnityEngine;
using System.Collections;

public class LogoInput : MonoBehaviour {


	public delegate void Function();


	public event Function onMouseExit;
	public event Function onMouseEnter;
	public event Function onMouseClick;
	public event Function onMouseDown;


	public void OnMouseEnter()
	{
       // Debug.Log("OnMouseEnter");
		if (onMouseEnter != null) {
			onMouseEnter ();
		}


	}
    public void OnMouseDown(){
		if (onMouseDown != null) {

			onMouseDown ();
		}
	}
    public void OnMouseUpAsButton(){
		if (onMouseClick != null) {
			
			onMouseClick ();
		}
	}
    public void OnMouseExit()
    {
      //  Debug.Log("OnMouseExit");
        //Renderer renderer = this.gameObject.GetComponent<Renderer> ();
        //Color color = renderer.material.color;
        //color.a = 0;
        //renderer.material.color = color;
        if (onMouseExit != null) {
			onMouseExit ();
		}

	}	

	
}
