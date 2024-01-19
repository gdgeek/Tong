using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class LogoInput : MonoBehaviour {


	public delegate void Function();


	[SerializeField]
	private UnityEvent _onMouseExit;
	public event Function onMouseExit;
	[SerializeField]
	private UnityEvent _onMouseEnter;
	public event Function onMouseEnter;
	[SerializeField]
	private UnityEvent _onMouseClick;
	public event Function onMouseClick;
	[SerializeField]
	private UnityEvent _onMouseDown;
	public event Function onMouseDown;


	public void OnMouseEnter()
	{
		_onMouseEnter?.Invoke();
       // Debug.Log("OnMouseEnter");
		if (onMouseEnter != null) {
			onMouseEnter ();
		}


	}
    public void OnMouseDown(){
	    _onMouseDown?.Invoke();
		if (onMouseDown != null) {

			onMouseDown ();
		}
	}
    public void OnMouseUpAsButton(){
	    _onMouseClick?.Invoke();
		if (onMouseClick != null) {
			
			onMouseClick ();
		}
	}
    public void OnMouseExit()
    {
	    _onMouseClick?.Invoke();
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
