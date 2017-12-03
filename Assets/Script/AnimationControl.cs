using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationControl : MonoBehaviour {

    public Animation[] MagnetsAnim;
    private int NumAnim;
    
	
	// Update is called once per frame
	void Update () {
        StartCoroutine(AnimController(NumAnim, 3f));
	}

    IEnumerator AnimController(int Num, float time)
    {
        if (Num == 0)
        {
            MagnetsAnim[Num].Play();
            MagnetsAnim[Num + 1].Stop();
            yield return new WaitForSeconds(time);
            NumAnim = 1;
        }

        if (Num == 1)
        {
            MagnetsAnim[Num].Play();
            MagnetsAnim[Num - 1].Stop();
            yield return new WaitForSeconds(time);
            NumAnim = 0;
        }
    }
}
