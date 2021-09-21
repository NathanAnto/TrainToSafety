using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(BasicZombieBehaviour))]
public class ObjectBuilderEditor : Editor
{
    
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        BasicZombieBehaviour myScript = (BasicZombieBehaviour)target;
        if(GUILayout.Button("Deal Damage"))
        {
            myScript.TakeDamage(1);
        }
    }
}
