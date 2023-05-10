using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IFindTargetSelectioner
{
    public void SetFindTargetVariant(FindTargetVariations variant)=>ChangeFindTargetVariant(variant);
    
    private void ChangeFindTargetVariant(FindTargetVariations variant)
    {
        switch(variant)
        {
            case FindTargetVariations.Random:
                OnRandomSelected();
                break;
            case FindTargetVariations.Near:
                OnNearSelected();
                break;
            case FindTargetVariations.HighHP:
                OnHighHPSelected();
                break;
            case FindTargetVariations.LowHP:
                OnLowHPSelected();
                break;
            default:
                Debug.LogWarning("FindTarget not selected");
                break;
        }
    }

    protected void OnRandomSelected();
    protected void OnNearSelected();
    protected void OnLowHPSelected();
    protected void OnHighHPSelected();


}
