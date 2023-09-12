using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
        [SerializeField] Transform bar;
        
        public void SetState(int current, int max)
        {
                float state = (float)current;
                if (state < 0f) state = 0f;
                else if (state > max) state = max;
                state /= max;
                bar.transform.localScale = new Vector3(state, 1f, 1f);
        }
}
