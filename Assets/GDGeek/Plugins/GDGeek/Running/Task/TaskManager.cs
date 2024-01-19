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
using System.Collections;
using System;

namespace GDGeek
{
    public class TaskManager : Singleton<TaskManager> {

		//public TaskFactories _factories = null;
		//public TaskRunner _runner = null;
		
		private TaskRunner partRunner_  = null;

		public TaskRunner partRunner{
			set{this.partRunner_ = value as TaskRunner;}
		}

        protected void Awake(){
           // base.Awake();
			//TaskManager.instance_ = this;
			/*if (_runner == null) {
				_runner = this.gameObject.GetComponent<TaskRunner>();			
			}
			if (_runner == null) {
				_runner = this.gameObject.AddComponent<TaskRunner>();	
			}*/
		}
		public static TaskManager GetInstance(){

			return Singleton<TaskManager>.GetOrCreateInstance;
		}

		public ITaskRunner globalRunner{
			get{
				TaskRunner runner = this.gameObject.AskComponent<TaskRunner> ();
				return runner;
			}
		}

#if UNITY_EDITOR
		public ITaskRunner editorRunner{
			get{
				TaskRunnerInEditor runner = this.gameObject.AskComponent<TaskRunnerInEditor> ();
				return runner;
			}
		}
#endif
		public ITaskRunner runner{
		get{
#if UNITY_EDITOR
			/*	if(!Application.isPlaying){
					return editorRunner;
				}*/

#endif
				if(partRunner_ != null){
					return partRunner_;
				}
				return globalRunner;
			}

		}
		
		public static void AddOrIsOver(Task task, Func<bool> function)
		{
			Func<bool> isOver = task.isOver;
			task.isOver = () => {
				return (isOver() || function.Invoke());
			};
		}
		
		public static void AddAndIsOver(Task task, Func<bool> function) => AddIsOver(task, function);

		public static void AddIsOver(Task task, Func<bool> function)
        {
			Func<bool> isOver = task.isOver;
            task.isOver = ()=> {
                return (isOver() && (function.Invoke()));
            };
        }

        public static void Delayed(Action shotdown)
        {
            Task task = new Task();
            task.shutdown = shotdown;
            Run(task);
        }

        public static void AddUpdate(Task task, Action<float> function)
		{
			Action <float> update = task.update;
			task.update = (float d) => {
				update?.Invoke(d);
				function?.Invoke(d);
			};
		}

		public static void PushBack(Task task, Action function)
		{
			Action shutdown = task.shutdown;
			task.shutdown = () => {
				shutdown?.Invoke();
				function?.Invoke();
			};
		}
	
		public static void Run(Task task){
			TaskManager.GetInstance().runner.addTask(task);
		}

		public static void PushFront(Task task, Action function){
			Action init = task.init;
			task.init = () =>{
				function?.Invoke();
				init?.Invoke();
			};
		}

       
    }
}
