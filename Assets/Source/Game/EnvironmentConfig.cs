using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnviromentConfig", menuName = "Config/EnviromentConfig")]
public class EnvironmentConfig : ScriptableObject
{
    [SerializeField] private GameObject[] _roadTemplates;
    [SerializeField] private GameObject[] _offroadTemplates;
    [SerializeField] private GameObject[] _bigObjectsTemplates;
    [SerializeField] private GameObject[] _smallObjectsTemplates;

    public IReadOnlyCollection<GameObject> RoadTemplates => _roadTemplates;
    public IReadOnlyCollection<GameObject> OffRoadTemplates => _offroadTemplates;
    public IReadOnlyCollection<GameObject> BigObjectTemplates => _bigObjectsTemplates;
    public IReadOnlyCollection<GameObject> SmallObjectTemplates => _smallObjectsTemplates;
}
