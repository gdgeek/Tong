using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class LogoInput : MonoBehaviour {


	public delegate void Function();


	[SerializeField]
	private UnityEvent _onMouseExit;

	public UnityEvent onMouseExit
	{

		get
		{
			if (null == _onMouseExit)
			{
				_onMouseExit = new UnityEvent();
			}

			return _onMouseExit;
		}
	}

	[SerializeField]
	private UnityEvent _onMouseEnter;

	public UnityEvent onMouseEnter
	{

		get
		{
			if (null == _onMouseEnter)
			{
				_onMouseEnter = new UnityEvent();
			}

			return _onMouseEnter;
		}
	}
	
	[SerializeField]
	private UnityEvent _onMouseClick;
	public UnityEvent onMouseClick
	{

		get
		{
			if (null == _onMouseClick)
			{
				_onMouseClick = new UnityEvent();
			}

			return _onMouseClick;
		}
	}
	[SerializeField]
	private UnityEvent _onMouseDown;
	
	public UnityEvent onMouseDown 
	{

		get
		{
			if (null == _onMouseDown)
			{
				_onMouseDown = new UnityEvent();
			}

			return _onMouseDown;
		}
	}
	//public event Function onMouseDown;


	public void OnMouseEnter()
	{
		_onMouseEnter?.Invoke();
       


	}
    public void OnMouseDown(){
	    _onMouseDown?.Invoke();
		
	}
    public void OnMouseUpAsButton(){
	    _onMouseClick?.Invoke();
	
	}
    public void OnMouseExit()
    {
	    _onMouseClick?.Invoke();
    

	}	

	
}
