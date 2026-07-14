using _Game.Scripts.OpenWorld.Sun;
using UnityEngine;

namespace _Game.Scripts.OpenWorld.Biomes.Structures
{
public class StructureController: MonoBehaviour
{
    [SerializeField] private StructureData[] _configs;

    private SunFaces _type;
    private StructureData _currentData;
    private GameObject _structure;

    public void UpdateType(SunFaces type)
    {
        if (_configs == null || _configs.Length == 0) return;
        if (_type == type && _currentData != null) return;
        foreach (var config in _configs)
        {
            if (_currentData == null || config.Type == type)
            {
                _type = type;
                _currentData = config;
                SetNewStructure();
                return;
            }
            
        }
    }

    private void SetNewStructure()
    {
        if (_structure)
        {
            Destroy(_structure);
        }
        if (_currentData.Prefab)
        {
            _structure = Instantiate(_currentData.Prefab, transform);
        }
    }
}
}