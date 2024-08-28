using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimCompo : MonoBehaviour
{
    [SerializeField] Animator characterAnimator = null;
    [SerializeField] float animDamping = 0.07f;

    void Start()
    {
        characterAnimator = GetComponent<Animator>();
    }

    void Update()
    {
        
    }

    public void UpdateForwardAnimatorParam(float _value)
    {
        if (!characterAnimator) return;
        characterAnimator.SetFloat(AnimParams.FORWARD_PARAM, _value, animDamping, Time.deltaTime);
    }

    public void UpdateRightAnimatorParam(float _value)
    {
        if (!characterAnimator) return;
        characterAnimator.SetFloat(AnimParams.RIGHT_PARAM, _value, animDamping, Time.deltaTime);
    }
}
