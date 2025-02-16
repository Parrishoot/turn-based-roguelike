using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistentGameObject : MonoBehaviour
{
    void Awake() {
        DontDestroyOnLoad(this.gameObject);
    }
}
