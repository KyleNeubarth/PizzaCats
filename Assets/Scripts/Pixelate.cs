using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Pixelate : MonoBehaviour {

	public Material effectMaterial;

	void OnRenderImage(RenderTexture src, RenderTexture dest) {

		Graphics.Blit (src,dest,effectMaterial);

	}
	
	/*
	  Meow
		)
	  ^__^
	=('.')=
	 |`[]\ |
	|____|/
	 
	 Text cat hopes your code works
	*/
}
