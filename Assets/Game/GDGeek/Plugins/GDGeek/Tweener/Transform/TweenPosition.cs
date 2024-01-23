﻿/*-----------------------------------------------------------------------------
The MIT License (MIT)

This source file is part of GDGeek
    (Game Develop & Game Engine Extendable Kits)
For the latest info, see http://gdgeek.com/

Copyright (c) 2014-2021 GDGeek Software Ltd

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.
-----------------------------------------------------------------------------
*/
using UnityEngine;

/// <summary>
/// Tween the object's position.
/// </summary>
namespace GDGeek{
	public class TweenPosition : Tween
	{
		public Vector3 from;
		public Vector3 to;
		[SerializeField]
		public Space space = Space.World;
		Transform mTrans;
		
	//	public Transform cachedTransform { get { if (mTrans == null) mTrans = target.transform; return mTrans; } }

		public Vector3 position
		{
			get
			{
				if(space == Space.Self)
					return transform.localPosition;
				else
					return transform.position;
			}
			set
			{
				if(space == Space.Self)
					transform.localPosition = value;
				else
					transform.position = value;
			}
		}

		override protected void onUpdate (float factor, bool isFinished) { this.position = from * (1f - factor) + to * factor; }
		
		/// <summary>
		/// Start the tweening operation.
		/// </summary>
		
		static public TweenPosition Begin (GameObject go, float duration, Vector3 position, Space space = Space.World )
		{
			TweenPosition comp = Tween.Begin<TweenPosition>(go, duration);
			comp.space = space;
			comp.from = comp.position;
			comp.to = position;
			
			if (duration <= 0f)
			{
				comp.sample(1f, true);
				comp.enabled = false;
			}
			return comp;
		}
	}
}