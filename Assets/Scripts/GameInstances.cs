using UnityEngine;

public class GameInstances
{
    public static GameInstances Instance
    {
        get
        {
            if (_instance == null)
            {
                return _instance = new GameInstances();
            }
            else
            {
                return _instance;
            }
        }
    }
    private static GameInstances _instance;

    public IPathFinder PathFinder
    {
        get
        {
            if(_pathFinder == null)
            {
                return _pathFinder = new HexPathFinder();
            }
            else
            {
                return _pathFinder;
            }
        }
    }
    private IPathFinder _pathFinder;

    public IMap Map
    {
        get
        {
            if (_map == null)
            {
                return _map = new HexMap();
            }
            else
            {
                return _map;
            }
        }
    }
    private IMap _map;

    public IMapSettings MapSettings
    {
        get
        {
            if (_mapSettings == null)
            {
                return _mapSettings = Resources.Load<HexMapSettings>("Configs/HexMapSettings");
            }
            else
            {
                return _mapSettings;
            }
        }
    }
    private IMapSettings _mapSettings;
}
