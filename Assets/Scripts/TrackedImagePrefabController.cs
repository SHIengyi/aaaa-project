using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackedImagePrefabController : MonoBehaviour
{
    public AudioClip AudioClipA;
    public AudioClip AudioClipB;
    public AudioSource AS;

    public GameObject AnimationA;
    public GameObject AnimationB;

    GameObject ChoosenAnimation;
    
    public float coolDown = 5;

    float touchStartTime;

    enum State{
        touchable,
        unTouchable
    }

    State cubeState = State.touchable;

    private void Start() {
                AnimationA.SetActive(false);
        AnimationB.SetActive(false);

    }

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
                    if(Random.value>0.5f){
            Debug.Log("chhose a");
ChoosenAnimation = AnimationA;
AS.clip = AudioClipA;
        }else{
            Debug.Log("chhose b");
ChoosenAnimation = AnimationB;
AS.clip = AudioClipB;

        }
            CreateCustomMeshAndPlayAudio();
            cubeState = State.unTouchable;
            break;
            case State.unTouchable:
            // do sth
            break;
        }
    }

    void CreateCustomMeshAndPlayAudio(){
        if (ChoosenAnimation.activeSelf) return;
        // set color
        GetComponent<MeshRenderer>().material.color = m_TriggerColor;

        ChoosenAnimation.SetActive(true);
        ChoosenAnimation.GetComponentInChildren<Animator>().Update(0);
        // todo: create custom mesh and play animation:
        //var go = Instantiate(m_CustomMesh, transform.position + new Vector3(0f, 0.1f, 0f), Quaternion.identity);
        //go.transform.position = this.gameObject.transform.position + new Vector3(0,0.25f,0);
        // play audio:
        AS.Play();
        StartCoroutine(ResetMarkerAfterDelay(5f));
    }

    private IEnumerator ResetMarkerAfterDelay(float delay) {
        yield return new WaitForSeconds(delay);
        ChoosenAnimation.SetActive(false);
    }

    private void OnTriggerExit(Collider other) {
        GetComponent<MeshRenderer>().material.color = Color.white;
    }
}
