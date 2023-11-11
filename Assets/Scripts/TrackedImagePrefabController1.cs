using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackedImagePrefabController1 : MonoBehaviour
{
    public AudioSource en;

    //计时器  计时五秒
  /*  CountTimeModel model1;
    void Start(){
        model1=CountTime.Count(5)
        .OnBegin(delegate{
            Debug.Log("start"):
        })

        .OnEnd(delegate{
            Debug.Log("end"):
        })

        .OnStop(delegate{
            Debug.Log("pause"):
        
        })

        //更新，确定更新频率
        .onUpdate(onTimeUpdate);

        model1.Begin();
        model1.Pause();
        model1.Continue();
        model1.Stop();
    }
    //更新对应的回调需要有一个float参数
    void onTimeUpdate(float time)
    {
        Debug.Log(time);
    }  */
    
    public float coolDown = 5;
    float touchStartTime;

    enum State{
        touchable,
        unTouchable
    }

    State cubeState = State.touchable;



    private void Update()
    {
        switch(cubeState){
            case State.touchable:
                // do sth: do nothing
            break;
            case State.unTouchable:
                // do sth
                if(Time.time - touchStartTime >5){
                    cubeState = State.touchable;
                }
            break;
        }
    }



    [SerializeField] private Color m_TriggerColor = Color.blue;
    [SerializeField] private GameObject m_CustomMesh;
   // int n = 0;
    private void OnTriggerEnter(Collider other)
    {
        switch(cubeState){
            case State.touchable:
            // do sth
            // timer start
            touchStartTime = Time.time;
            CreateCustomMeshAndPlayAudio();
            cubeState = State.unTouchable;
            break;
            case State.unTouchable:
            // do sth
            break;
        }
    }

    void CreateCustomMeshAndPlayAudio(){
        // set color
        GetComponent<MeshRenderer>().material.color = m_TriggerColor;
        // todo: create custom mesh and play animation:
        var go = Instantiate(m_CustomMesh);
        go. transform.position = this.gameObject.transform.position + new Vector3(0,0.25f,0);
        // play audio:
        en.Play();
    }

    private void OnTriggerExit(Collider other) {
        GetComponent<MeshRenderer>().material.color = Color.white;
    }
}
