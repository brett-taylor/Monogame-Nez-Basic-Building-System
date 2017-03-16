using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Nez;
using Nez.Sprites;
using HospitalCeo.World;
using HospitalCeo.Building;

/*
 * Brett Taylor
 * Base Tile Class
 */

namespace HospitalCeo.World
{
    public class Tile
    {
        public Vector2 position { get; private set; }
        public Vector2 tileNumber { get; private set; }
        private Entity entity;
        private Building.Building infrastructureItem;
        private Building.Building gameplayItem;

        public Tile(Texture2D texture, Vector2 position, Vector2 tileNumber)
        {
            entity = WorldController.SCENE.createEntity("tile: " + position + " : " + tileNumber);
            this.position = position;
            this.tileNumber = tileNumber;
            entity.position = position;
            entity.addComponent<Sprite>(new Sprite(texture));
        }

        public void SetSprite(Texture2D texture)
        {
            entity.removeComponent<Sprite>();
            entity.addComponent<Sprite>(new Sprite(texture));
        }

        public override string ToString()
        {
            return "Tile: " + position + " : " + tileNumber;
        }

        public Tile[] GetNeighbours(bool doDiaganol)
        {
            Tile[] ns = new Tile[doDiaganol ? 8 : 4];

            ns[0] = WorldController.GetTileAt((int) position.X,      (int) position.Y + 1);   // North
            ns[1] = WorldController.GetTileAt((int) position.X + 1,  (int) position.Y);       // East
            ns[2] = WorldController.GetTileAt((int) position.X,      (int) position.Y - 1);   // South
            ns[3] = WorldController.GetTileAt((int) position.X - 1,  (int) position.Y);       // West

            if (doDiaganol)
            {
                ns[4] = WorldController.GetTileAt((int) position.X + 1, (int) position.Y + 1); // North East
                ns[5] = WorldController.GetTileAt((int) position.X + 1, (int) position.Y - 1); // South East
                ns[6] = WorldController.GetTileAt((int) position.X - 1, (int) position.Y - 1); // South West
                ns[7] = WorldController.GetTileAt((int) position.X - 1, (int) position.Y + 1); // North West
            }

            return ns;
        }

        public Tile GetNeighbour(Compass direction)
        {
            switch (direction)
            {
                case Compass.N:
                    return WorldController.GetTileAt((int) position.X,       (int)position.Y + 1);
                case Compass.E:
                    return WorldController.GetTileAt((int) position.X + 1,   (int)position.Y);
                case Compass.S:
                    return WorldController.GetTileAt((int) position.X,       (int)position.Y - 1);
                case Compass.W:
                    return WorldController.GetTileAt((int)position.X - 1,   (int)position.Y);
                case Compass.NE:
                    return WorldController.GetTileAt((int)position.X + 1,   (int)position.Y + 1);
                case Compass.SE:
                    return WorldController.GetTileAt((int)position.X + 1,   (int)position.Y - 1);
                case Compass.SW:
                    return WorldController.GetTileAt((int)position.X - 1,   (int)position.Y - 1);
                case Compass.NW:
                    return WorldController.GetTileAt((int)position.X - 1,   (int)position.Y + 1);
                default:
                    return GetNeighbour(Compass.N);
            }
        }

        public void SetInfrastructureItem(Building.Building item)
        {
            this.infrastructureItem = item;
        }

        public void SetGameplayItem(Building.Building item)
        {
            this.gameplayItem = item;
        }

        public Building.Building GetInfrastructureItem()
        {
            return infrastructureItem == null ? null : infrastructureItem;
        }

        public Building.Building GetGameplayItem()
        {
            return gameplayItem == null ? null : gameplayItem;
        }

        public int SimilarItemsAroundTile(Building.Building building)
        {
            Compass[] directions = { Compass.N, Compass.E, Compass.S, Compass.W };
            int similarAmount = 0;

            if (building.GetBuildingType() == BuildingType.Infrastructure)
            {
                foreach (Compass d in directions)
                {
                    Tile t = GetNeighbour(d);
                    if (t != null)
                        if (t.GetInfrastructureItem() != null && t.GetInfrastructureItem().GetType() == building.GetType())
                            similarAmount++;
                }
            }
            else if (building.GetBuildingType() == BuildingType.Gameplay)
            {
                foreach (Compass d in directions)
                {
                    Tile t = GetNeighbour(d);
                    if (t != null)
                        if (t.GetGameplayItem() != null && t.GetGameplayItem().GetType() == building.GetType())
                            similarAmount++;
                }
            }

            return similarAmount;
        }

        public bool SimilarItemNextToTile(Compass direction, Building.Building building)
        {
            if (building.GetBuildingType() == BuildingType.Infrastructure)
            {
                Tile t = GetNeighbour(direction);
                if (t != null)
                {
                    Building.Building neighbourBuilding = t.GetInfrastructureItem();
                    if (neighbourBuilding != null && neighbourBuilding.GetType() == building.GetType())
                    {
                        return true;
                    }
                }
            }
            else if (building.GetBuildingType() == BuildingType.Gameplay)
            {
                Tile t = GetNeighbour(direction);
                if (t != null)
                {
                    Building.Building neighbourBuilding = t.GetGameplayItem();
                    if (neighbourBuilding != null && neighbourBuilding.GetType() == building.GetType())
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}
