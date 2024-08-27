using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IA_PNJ_BaseBehaviour : StateMachineBehaviour
{
    [SerializeField] protected IA_PNJ_Brain brain = null;
    [SerializeField] protected Color debugColor = Color.black;

    public void Init(IA_PNJ_Brain _brain)
    {
        brain = _brain;

    }
   
}
