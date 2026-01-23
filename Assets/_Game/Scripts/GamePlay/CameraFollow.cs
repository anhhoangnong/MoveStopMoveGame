using System.Collections;
using System.Collections.Generic;
using UIExample;
using UnityEngine;

public class CameraFollow : Singleton<CameraFollow>
{
    //Camera trạng thái cho 3 camvas
    public enum State { MainMenu, Gameplay, Shop }
    private int startindex;
    [SerializeField] Transform tf;
    
    [Header("Rotation")]
    [SerializeField] Vector3 playerRotate;
    [SerializeField] Vector3 gamePlayRotate;

    [Header("Offset")]
    [SerializeField] Vector3 playerOffset;
    [SerializeField] Vector3 offset;
    [SerializeField] Vector3 offsetMax;
    [SerializeField] Vector3 offsetMin;

    [SerializeField] Transform[] offsets;

    [SerializeField] float moveSpeed = 5f;
    Transform target;

    private Vector3 targetOffset;
    private Quaternion targetRotate;

    public Camera Camera;


    private void Awake()
    {
        tf = transform;
        target = FindObjectOfType<Player>().transform;
        Camera = Camera.main;
        if (offsets[0] == true)
        {
            startindex = 1;
            Debug.Log("Start at MainMenu with index =" +startindex);
        }
        tf.rotation = Quaternion.Euler(20, 0, 0);
       
    }


    private void LateUpdate()
    {

        if (target == null) return;
        if (startindex == 1)
        {
            tf.position = new Vector3(3.635f, 11.36f, -25.31f);
            tf.rotation = Quaternion.Euler(20, 0, 0);
            
        }
        else
        {
            offset = Vector3.Lerp(offset, targetOffset, Time.deltaTime * moveSpeed);
            tf.rotation = Quaternion.Lerp(tf.rotation, targetRotate, Time.deltaTime * moveSpeed);
            tf.position = Vector3.Lerp(tf.position, target.position + offset, Time.deltaTime * moveSpeed);
            //Debug.Log("Camera Position: " + tf.position.ToString());
            //Debug.Log("Camera Offset: " + offset.ToString());
        }
    }


    // dùng Lerp để set offset giữa min và max theo rate
    public void SetRateOffset(float rate)
    {
        targetOffset = Vector3.Lerp(offsetMin, offsetMax, rate);
    }

    // thay đổi trạng thái camera theo state
    public void ChangeState(State state)
    {
        if(state == State.Shop || state == State.Gameplay)
        {
            startindex = 0;
            Debug.Log("change state"+state.ToString()+ " with index =" + startindex);
        }    
        targetOffset = offsets[(int)state].localPosition;
        //Debug.Log("Target Offset: " + targetOffset.ToString());
        //targetRotate = offsets[(int)state].localRotation;
        targetRotate = Quaternion.Euler(offsets[(int)state].localEulerAngles);

        return;

       
        
    }
}
