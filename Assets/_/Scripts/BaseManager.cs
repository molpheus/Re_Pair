using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseManager : MonoBehaviour
{
    public virtual void Init () {
        gameObject.SetActive (true);
    }
    public virtual void Close () {
        gameObject.SetActive (false);
    }
}
