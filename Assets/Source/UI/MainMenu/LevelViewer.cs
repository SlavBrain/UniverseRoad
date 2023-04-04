using System;
using UnityEngine;

public class LevelViewer : MonoBehaviour
{
    [SerializeField] private LevelConfig[] _levelConfigs;
    [SerializeField] private LevelView _template;
    [SerializeField] private GameObject _viewContainer;

    public event Action<LevelView> LevelViewCreated;

    private void OnEnable()
    {
        ClearContainer();

        for(int i=0;i<_levelConfigs.Length; i++)
        {
            CreateNewView(i);
        }
    }

    private void CreateNewView(int numberLevel)
    {
        LevelView newView = Instantiate(_template, _viewContainer.transform);
        newView.Initialize(numberLevel+1, _levelConfigs[numberLevel]);
        LevelViewCreated?.Invoke(newView);
    }

    private void ClearContainer()
    {
        if (_viewContainer.transform.childCount > 0)
        {
            foreach(Transform view in _viewContainer.transform)
            {
                Destroy(view.gameObject);
            }
        }
    }
}
