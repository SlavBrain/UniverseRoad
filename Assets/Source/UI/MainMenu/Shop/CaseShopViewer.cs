using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CaseShopViewer : MonoBehaviour
{
    [SerializeField] private List<CaseConfig> _cases;
    [SerializeField] private CaseView _caseViewTemplate;
    [SerializeField] private Transform _viewContainer;
    private List<CaseConfig> _createdViews;

    private void OnEnable()
    {
        Clear();

        foreach (CaseConfig config in _cases)
        {
            CreatedView(config);
        }
    }

    public void CreatedView(CaseConfig config)
    {
        CaseView newView = Instantiate(_caseViewTemplate, _viewContainer);
        newView.Initialize(config);
    }

    private void Clear()
    {
        foreach(Transform view in _viewContainer.transform)
        {
            Destroy(view.gameObject);
        }
    }
}
