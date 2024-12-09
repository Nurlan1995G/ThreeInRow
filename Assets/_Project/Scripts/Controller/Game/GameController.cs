using Assets._project.CodeBase;
using Assets._project.Config;
using System.Collections.Generic;
using UnityEngine;

namespace Assets._Project.Scripts.Controller
{
    public class GameController : MonoBehaviour
    {
        [SerializeField] private GameConfig _gameConfig;
        [SerializeField] private List<Point> _cells;
        [SerializeField] private List<Item> _items;
        [SerializeField] private GameView _gameView;
        [SerializeField] private PlayerInputHandler _playerInput;
        [SerializeField] private RecordsController _recordsController;
        [SerializeField] private MenuControllerUI _menuControllerUI;

        private ItemManagerModel _itemManager;
        private PlayerModel _playerModel;
        private List<ItemModel> _itemModels;
        private ItemDropHandler _itemDropHandler;

        private void Start() => 
            Initialize();

        public void ResetGame()
        {
            _playerModel = new PlayerModel(_gameConfig.PlayerData.StartCountScore, _gameView);
            _gameView.Initialize(_gameConfig.ManagerData);
            _gameView.InitializeGrid(_cells, _itemManager);
        }

        private void Initialize()
        {
            GameInitializer initializer = new GameInitializer();

            _itemManager = initializer.InitializeItemManager(_items, out _itemModels);
            GridManagerModel gridManagerModel = initializer.InitializeGridManager(_gameConfig, _cells, _itemManager);
            _playerModel = initializer.InitializePlayer(_gameConfig, _gameView);
            ItemClickHandler itemClickHandler = new ItemClickHandler(_itemManager, _gameConfig, _playerModel, DeactivateItems, EndGame);
            _itemDropHandler = new ItemDropHandler(gridManagerModel, _gameConfig);

            _gameView.Initialize(_gameConfig.ManagerData);
            _gameView.InitializeGrid(_cells, _itemManager);
            _playerInput.OnItemClicked += itemClickHandler.HandleClick;
        }

        private void DeactivateItems(List<ItemModel> items)
        {
            foreach (var item in items)
            {
                _itemManager.ReplaceItem(item);
                _gameView.RemoveItem(item);

                _playerModel.UpdateCurrentScore(_gameConfig.LogicData.Reward);
            }

            List<ItemModel> getAllItems = _itemManager.OnItemsMatched();
            StartCoroutine(_itemDropHandler.HandleFalling(getAllItems));
        }

        private void EndGame()
        {
            _recordsController.AddNewRecord("Player", _playerModel.CurrentScore);
            _recordsController.SaveCurrentScore(_playerModel.CurrentScore);

            _menuControllerUI.EndGame(_playerModel.CurrentScore);
        }
    }

    public enum Direction
    {
        Vertical,
        Horizontal
    }
}
