using Assets._project.CodeBase;
using Assets._project.Config;
using System.Collections.Generic;

namespace Assets._Project.Scripts.Controller
{
    public class GameInitializer
    {
        public ItemManagerModel InitializeItemManager(List<Item> items, out List<ItemModel> itemModels)
        {
            itemModels = new List<ItemModel>();

            foreach (Item item in items)
            {
                ItemModel itemModel = new ItemModel(item);
                item.Initialize(itemModel);
                itemModels.Add(itemModel);
            }

            return new ItemManagerModel(itemModels);
        }

        public PlayerModel InitializePlayer(GameConfig gameConfig, GameView gameView) => 
            new PlayerModel(gameConfig.PlayerData.StartCountScore, gameView);

        public GridManagerModel InitializeGridManager(GameConfig gameConfig, List<Point> cells
            ,ItemManagerModel itemManager) => 
            new GridManagerModel(itemManager, gameConfig.ManagerData, cells);
    }
}
