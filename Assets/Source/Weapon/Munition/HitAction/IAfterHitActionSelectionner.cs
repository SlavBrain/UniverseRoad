using UnityEngine;

public interface IAfterHitActionSelectionner
{
    public void SetAfterHitAction(AfterHitActionVariation variant) => ChangeAfterHitEffect(variant);
    protected void OnReboundSelected();
    protected void OnNothingSelected();
    private void ChangeAfterHitEffect(AfterHitActionVariation variant)
    {
        switch(variant)
        {
            case AfterHitActionVariation.Rebound:
                OnReboundSelected();
                break;
            case AfterHitActionVariation.Nothing:
                OnNothingSelected();
                break;
            default:
                Debug.LogWarning("AfterHitAction not selected");
                break;
        }
    }
}
