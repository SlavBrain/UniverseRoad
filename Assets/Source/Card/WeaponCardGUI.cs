using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(WeaponCard))]
public class WeaponCardGUI : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        WeaponCard card = (WeaponCard)target;
        
        if (GUILayout.Button("CreateWeaponStatsSO"))
        {
            card.CreateWeaponStatsSO();
        }
    }
}
