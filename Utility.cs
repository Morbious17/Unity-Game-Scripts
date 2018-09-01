

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utility
{
    
    public static void DecrementCounter(ref float counter, float speed)
    {
        counter -= Time.fixedDeltaTime * speed;
    }



}
