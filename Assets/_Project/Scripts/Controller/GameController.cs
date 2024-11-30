using Assets._project.CodeBase;
using Assets._project.Config;
using System.Collections.Generic;
using UnityEngine;

namespace Assets._Project.Scripts.Controller
{
    public class GameController : MonoBehaviour
    {
        [SerializeField] private GameConfig _gameConfig;
        [SerializeField] private List<Point> _cells; // Загружаются с уровня
        [SerializeField] private List<Item> _items;   // Загружаются с уровня
        [SerializeField] private GameView _gameView;

        private ItemManagerModel _itemManager;
        private GridManagerModel _gridManagerModel;

        private void Start()
        {
            Initialize();
        }

        private void Initialize()
        {
            _itemManager = new ItemManagerModel(_items, _gameConfig.ManagerData.StartPosition);
            _gridManagerModel = new GridManagerModel(_itemManager, _gameConfig.ManagerData, _cells);

            // Передача данных для визуализации в GameView
            _gameView.InitializeGrid(_cells, _itemManager, _gameConfig.ManagerData.CellSize);
        }
    }
}
