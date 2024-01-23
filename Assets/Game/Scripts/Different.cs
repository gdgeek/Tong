using UnityEngine;
using System.Collections;


public class Different : MonoBehaviour {

	public delegate void Function();


	public event Function onMouseExit;
	public event Function onMouseEnter;


	public void setAlpha(float alpha){
		Debug.LogError("!!!!!");
		Renderer renderer = this.gameObject.GetComponent<Renderer> ();

		Color color = renderer.material.color;
		color.a = alpha;
		renderer.material.color = color;

	}
	void OnMouseEnter()
	{
		
		if (onMouseEnter != null) {
			onMouseEnter ();
		}

	}

	void OnMouseExit()
	{
		Renderer renderer = this.gameObject.GetComponent<Renderer> ();
		Color color = renderer.material.color;
		color.a = 0;
		renderer.material.color = color;
		if (onMouseExit != null) {
			onMouseExit ();
		}

	}	


}
