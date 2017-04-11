using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Nez;
using Nez.Sprites;
using Nez.Textures;
using HospitalCeo.Building;

/*
 * Brett Taylor
 * Base Tile Component
 */

namespace HospitalCeo.World
{
    public class Tile
    {
        private Subtexture texture;
        private Vector2 position;
        private Vector2 tileNumber;
        private BuildingLogic infrastructureItem;
        private BuildingLogic gameplayItem;
        private Pathfinding.PathfindingNode<Tile> pathfindNode;
        private Zoning.Zone zone;

        public Tile(Subtexture texture, Vector2 position, Vector2 tileNumber)
        {
            this.texture = texture;
            this.position = position;
            this.tileNumber = tileNumber;
        }

        public override string ToString()
        {
            return "Tile: " + position + " : " + tileNumber;
        }

        public Tile[] GetNeighbours(bool doDiaganol)
        {
            Tile[] ns = new Tile[doDiaganol ? 8 : 4];

            ns[0] = WorldController.GetTileAt((int) tileNumber.X,       (int) tileNumber.Y - 1);   // North
            ns[1] = WorldController.GetTileAt((int) tileNumber.X - 1,   (int) tileNumber.Y);       // East
            ns[2] = WorldController.GetTileAt((int) tileNumber.X,       (int) tileNumber.Y + 1);   // South
            ns[3] = WorldController.GetTileAt((int) tileNumber.X + 1,   (int) tileNumber.Y);       // West

            if (doDiaganol)
            {
                ns[4] = WorldController.GetTileAt((int) tileNumber.X - 1, (int) tileNumber.Y - 1); // North East
                ns[5] = WorldController.GetTileAt((int) tileNumber.X - 1, (int) tileNumber.Y + 1); // South East
                ns[6] = WorldController.GetTileAt((int) tileNumber.X + 1, (int) tileNumber.Y + 1); // South West
                ns[7] = WorldController.GetTileAt((int) tileNumber.X + 1, (int) tileNumber.Y - 1); // North West
            }

            return ns;
        }

        public Tile GetNeighbour(Compass Compass)
        {
            switch (Compass)
            {
                case Compass.N:
                    return WorldController.GetTileAt((int) tileNumber.X,        (int) tileNumber.Y - 1);
                case Compass.E:
                    return WorldController.GetTileAt((int) tileNumber.X + 1,    (int) tileNumber.Y);
                case Compass.S:
                    return WorldController.GetTileAt((int) tileNumber.X,        (int) tileNumber.Y + 1);
                case Compass.W:
                    return WorldController.GetTileAt((int) tileNumber.X - 1,    (int) tileNumber.Y);
                case Compass.NE:
                    return WorldController.GetTileAt((int) tileNumber.X + 1,    (int) tileNumber.Y - 1);
                case Compass.SE:
                    return WorldController.GetTileAt((int) tileNumber.X + 1,    (int) tileNumber.Y + 1);
                case Compass.SW:
                    return WorldController.GetTileAt((int) tileNumber.X - 1,    (int) tileNumber.Y + 1);
                case Compass.NW:
                    return WorldController.GetTileAt((int) tileNumber.X - 1,    (int) tileNumber.Y - 1);
                default:
                    return GetNeighbour(Compass.N);
            }
        }

        public void SetInfrastructureItem(Building.BuildingLogic item)
        {
            this.infrastructureItem = item;
        }

        public void SetGameplayItem(Building.BuildingLogic item)
        {
            this.gameplayItem = item;
        }

        public Building.BuildingLogic GetInfrastructureItem()
        {
            return infrastructureItem == null ? null : infrastructureItem;
        }

        public Building.BuildingLogic GetGameplayItem()
        {
            return gameplayItem == null ? null : gameplayItem;
        }

        public int SimilarItemsAroundTile(BuildingLogic building)
        {
            Compass[] Compasss = { Compass.N, Compass.E, Compass.S, Compass.W };
            int similarAmount = 0;

            if (building.GetBuildingType() == BuildingType.Infrastructure)
            {
                foreach (Compass d in Compasss)
                {
                    Tile t = GetNeighbour(d);
                    if (t != null)
                        if ( t.GetInfrastructureItem() != null && t.GetInfrastructureItem().GetBuildingCatergory() == building.GetBuildingCatergory())
                            similarAmount++;
                }
            }
            else if (building.GetBuildingType() == BuildingType.Gameplay)
            {
                foreach (Compass d in Compasss)
                {
                    Tile t = GetNeighbour(d);
                    if (t != null)
                        if (t.GetGameplayItem() != null && t.GetGameplayItem().GetType() == building.GetType())
                            similarAmount++;
                }
            }

            return similarAmount;
        }

        public bool SimilarItemNextToTile(Compass Compass, Building.BuildingLogic building)
        {
            if (building.GetBuildingType() == BuildingType.Infrastructure)
            {
                Tile t = GetNeighbour(Compass);
                if (t != null)
                {
                    Building.BuildingLogic neighbourBuilding = t.GetInfrastructureItem();
                    if (neighbourBuilding != null && neighbourBuilding.GetBuildingCatergory() == building.GetBuildingCatergory())
                    {
                        return true;
                    }
                }
            }
            else if (building.GetBuildingType() == BuildingType.Gameplay)
            {
                Tile t = GetNeighbour(Compass);
                if (t != null)
                {
                    Building.BuildingLogic neighbourBuilding = t.GetGameplayItem();
                    if (neighbourBuilding != null && neighbourBuilding.GetType() == building.GetType())
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public Vector2 GetTileNumber()
        {
            return tileNumber;
        }

        public Vector2 GetPosition()
        {
            return position;
        }

        public Subtexture GetTexture()
        {
            return texture;
        }

        public float GetMovementCost()
        {
            if (infrastructureItem == null) return 1;
            return 1 * infrastructureItem.GetMovementCost();
        }

        public void AddToPathfind(Pathfinding.PathfindingNode<Tile> node)
        {
            pathfindNode = node;
        }

        public void RemoveFromPathfind()
        {
            pathfindNode = null;
        }

        public bool CanPathfindTo()
        {
            return pathfindNode == null ? false : true;
        }

        public Pathfinding.PathfindingNode<Tile> GetPathfindNode()
        {
            return pathfindNode;
        }

        public void SetZone(Zoning.Zone zone)
        {
            this.zone = zone;
        }

        public Zoning.Zone GetZone()
        {
            return zone;
        }
    }
}
