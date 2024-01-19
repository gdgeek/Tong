﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace GDGeek
{
  public class BoxColliderCreater : MonoBehaviour
  {

    private enum State
    {
      none,
      start,
      running,
      end,
    }
    [SerializeField]
    private int unit_ = 40;
    private Renderer[] renderers_ = null;
    private State state_ = State.none;
    private int index_ = 0;
    private bool hasBounds_ = false;
    private Bounds bounds_;
    private BoxCollider collider_ = null;
    public BoxCollider box => collider_;

    public static Task GetTask(GameObject gameObject, int layer =65535) => GetTask(gameObject, gameObject.transform,layer);
    public static Task GetTask(GameObject gameObject, Transform target, int layer =65535)
    {
     
      bool isOver = false;
      return Task.T.pushFront(() =>
      {
        BoxColliderCreater bcc = gameObject.AskComponent<BoxColliderCreater>();
        bcc.target = target;
        bcc.layer = layer;
        bcc.onDestroy += delegate
        {
          isOver = true;
        };
      }).addisOver(() => isOver);
    
    }

    public int layer { private get; set; }

    private Transform target_ = null;
    public Transform target
    {
      get
      {
        if (target_ == null)
        {

          return this.transform;
        }
        return target_;

      }
      set { target_ = value; }
    }

    public Action onDestroy
    {
      get;
      set;
    }
    void Awake()
    {

      collider_ = this.gameObject.AskComponent<BoxCollider>();
      collider_.size = Vector3.zero;
      collider_.center = Vector3.zero;
    }

    public bool includeInactive { get; set; } = false;

    void Start()
    {

      renderers_ = target.transform.GetComponentsInChildren<Renderer>(includeInactive);

      state_ = State.running;
      index_ = 0;
      hasBounds_ = false;
      bounds_ = new Bounds(Vector3.zero, Vector3.zero);

    }
    void Update()
    {
      if (index_ < renderers_.Length)
      {
        Quaternion rotate = this.transform.rotation;
        Vector3 position = this.transform.position;
        Vector3 scale = this.transform.lossyScale;



        this.transform.rotation = Quaternion.identity;
        this.transform.position = Vector3.zero;
        this.transform.setGlobalScale(Vector3.one);

        int count = Mathf.Min(index_ + unit_, renderers_.Length);
        for (; index_ < count; ++index_)
        {
          
          int mark = (1<<renderers_[index_].gameObject.layer) & this.layer;
          

          if (renderers_[index_] && renderers_[index_].gameObject.activeSelf && mark !=0)
          {
            if(renderers_[index_].gameObject.layer == LayerMask.NameToLayer("UI")){
                continue;
            }
          

            if (hasBounds_)
            {
              bounds_.Encapsulate(renderers_[index_].bounds);
            }
            else
            {
              bounds_ = renderers_[index_].bounds;
              hasBounds_ = true;
            }
          }


        }

        this.transform.rotation = rotate;
        this.transform.position = position;
        this.transform.setGlobalScale(scale);
      }
      else
      {
        Destroy(this);
      }
    }
    void OnDestroy()
    {
      collider_.center = bounds_.center;
      collider_.size = bounds_.size;
      onDestroy?.Invoke();
    }

  }
}