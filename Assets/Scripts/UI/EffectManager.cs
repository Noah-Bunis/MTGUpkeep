using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
        [SerializeField] public Camera camera;

        public void CameraShake(float time, float intensity)
        {
                camera.GetComponent<CameraMovement>().CameraShake(time, intensity);
        }
}
