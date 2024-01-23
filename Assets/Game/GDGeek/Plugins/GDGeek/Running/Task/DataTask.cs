using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using UnityEngine;
namespace GDGeek
{
    
    public class DataTask : GDGeek.Task//, IDataTask<T>
    {
        protected Action doThen;
        protected Action<Exception> doError;

        public DataTask then(Action callback)
        {
            doThen += callback;
            return this;
        }

        public DataTask error(Action<Exception> callback)
        {
            doError += callback;
            return this;
        }
        public void reject(Exception exception) {
            over = true;
            if (doError.IsNotNull())
            {
                doError.Invoke(exception);
            }
            else
            {
                throw exception;
            }


        }
        public void resolve() {
           
            over = true;
            doThen?.Invoke();
        }




        public DataTask()
        {
            this.isOver =()=>over;
        }



        protected bool over
        {
            get;
            set;
        } = false;

        public DataTask t => this;
    }
    public class DataTask<T> : GDGeek.Task//, IDataTask<T>
    {
        protected Action<T> doThen;
        protected Action<Exception> doError;

        public DataTask<T> then(Action<T> callback)
        {
            doThen += callback;
            return this;
        }
        
        public DataTask<T> error(Action<Exception> callback)
        {
            doError += callback;
            return this;
        }
        public void reject(Exception exception) {
            over = true;
            if (doError.IsNotNull())
            {
                doError.Invoke(exception);
            }
            else
            {
              //  TaskThreading
                throw exception;
            }


        }
        public void resolve(T t) {
            data = t;
            over = true;
            doThen?.Invoke(data);
        }




        public DataTask()
        {
            this.isOver =()=>over;
        }



        private bool over
        {
            get;
            set;
        } = false;



      
        public T data { private set; get; }
 
        public DataTask<T> t => this;
  }
    
  
}