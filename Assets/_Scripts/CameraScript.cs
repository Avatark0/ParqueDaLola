using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour{
    private Transform lookAt;
    private Transform camTransform;

    private Camera cam;

    private const float Y_ANGLE_MIN = -80f;
    private const float Y_ANGLE_MAX = 89f;

    private float distance = 8f;
    private float currentX=24f;
    private float currentY=94f;
    private float sensivityX=2.3f;
    private float sensivityY=1.6f;

    private bool cameraAnimacao;//muda o modo da camera para animação/controle
    private float tempo;

    void Start(){
        lookAt=GameObject.FindWithTag("Personagem").GetComponent<Transform>();
        camTransform=transform;
        cam=Camera.main;

        cameraAnimacao=true;
        tempo=Time.time;

        //currentY=24;
        //currentX=94;
    }

    void Update(){
        if(Time.time-tempo>=6.15f)cameraAnimacao=false;
        if(!cameraAnimacao){
            currentY-= Input.GetAxis("Mouse Y")*sensivityY;
            currentY = Mathf.Clamp(currentY,Y_ANGLE_MIN,Y_ANGLE_MAX);
            currentX+= Input.GetAxis("Mouse X")*sensivityX;
        }
    }

    private void LateUpdate(){
        Vector3 dist = new Vector3(0,0,-distance);
        Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);
        
        if(!cameraAnimacao)
            camTransform.position = lookAt.position+rotation*dist;
        camTransform.LookAt(lookAt.position);
    }
}
