using UnityEngine;

[CreateAssetMenu(fileName = "EnviromentConfig", menuName = "Config/EnviromentConfig")]
public class EnviromentConfig : ScriptableObject
{
    [SerializeField] private GameObject[] _roadTemplates;
    [SerializeField] private GameObject[] _offroadTemplates;
    [SerializeField] private GameObject[] _bigObjectTemplates;
    [SerializeField] private GameObject[] _smallObjectsTemplates;
}
