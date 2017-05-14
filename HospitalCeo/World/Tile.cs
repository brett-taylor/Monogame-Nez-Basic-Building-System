using HospitalCeo.Building;
using Microsoft.Xna.Framework;
using Nez.Textures;
using System;
using System.Collections.Generic;

/*
 * Brett Taylor
 * Base Tile Component
 */

namespace HospitalCeo.World
{
    [MoonSharp.Interpreter.MoonSharpUserData]
    public class Tile
    {
        private TileSprite tileSprite;
        private Subtexture texture;
        private Vector2 position;
        private Vector2 tileNumber;
        private Building.Building infrastructureItem;
        private Building.Building gameplayItem;
        private Pathfinding.PathfindingNode<Tile> pathfindNode;
        private Zoning.Zone zone;
        private Action<Tile> onInfrastructureObjectChanged;
        private Action<Tile> onGameplayObjectChanged;
        private Action<Tile> onZoneChanged;

        public Tile(Subtexture texture, Vector2 position, Vector2 tileNumber)
        {
            this.texture = texture;
            this.position = position;
            this.tileNumber = tileNumber;
        }

        public void SetTileSprite(TileSprite ts)
        {
            tileSprite = ts;
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

        public void SetInfrastructureItem(Building.Building item)
        {
            this.infrastructureItem = item;
            onInfrastructureObjectChanged?.Invoke(this);
        }

        public void SetGameplayItem(Building.Building item)
        {
            this.gameplayItem = item;
            onGameplayObjectChanged?.Invoke(this);
        }

        public Building.Building GetInfrastructureItem()
        {
            return infrastructureItem == null ? null : infrastructureItem;
        }

        public Building.Building GetGameplayItem()
        {
            return gameplayItem == null ? null : gameplayItem;
        }

        public int SimilarConstructedItemsAroundTile(Building.Building building)
        {
            Compass[] Compasss = { Compass.N, Compass.E, Compass.S, Compass.W };
            int similarAmount = 0;

            if (building.GetPrimitiveObject().GetBuildingType() == BuildingType.Infrastructure)
            {
                foreach (Compass d in Compasss)
                {
                    Tile t = GetNeighbour(d);
                    if (t != null)
                        if (t.GetInfrastructureItem() != null && t.GetInfrastructureItem().GetPrimitiveObject().GetBuildingCategory() == building.GetPrimitiveObject().GetBuildingCategory())
                            if (t.GetInfrastructureItem().IsConstructed(t.GetTileNumber(), true))
                                similarAmount++;
                }
            }
            else if (building.GetPrimitiveObject().GetBuildingType() == BuildingType.Gameplay)
            {
                foreach (Compass d in Compasss)
                {
                    Tile t = GetNeighbour(d);
                    if (t != null)
                        if (t.GetGameplayItem() != null && t.GetGameplayItem().GetPrimitiveObject().GetBuildingCategory() == building.GetPrimitiveObject().GetBuildingCategory())
                            if (t.GetGameplayItem().IsConstructed(t.GetTileNumber(), true))
                                similarAmount++;
                }
            }

            return similarAmount;
        }

        public int SimilarItemsAroundTile(Building.Building building)
        {
            Compass[] Compasss = { Compass.N, Compass.E, Compass.S, Compass.W };
            int similarAmount = 0;

            if (building.GetPrimitiveObject().GetBuildingType() == BuildingType.Infrastructure)
            {
                foreach (Compass d in Compasss)
                {
                    Tile t = GetNeighbour(d);
                    if (t != null)
                        if ( t.GetInfrastructureItem() != null && t.GetInfrastructureItem().GetPrimitiveObject().GetBuildingCategory() == building.GetPrimitiveObject().GetBuildingCategory())
                            similarAmount++;
                }
            }
            else if (building.GetPrimitiveObject().GetBuildingType() == BuildingType.Gameplay)
            {
                foreach (Compass d in Compasss)
                {
                    Tile t = GetNeighbour(d);
                    if (t != null)
                        if (t.GetGameplayItem() != null && t.GetGameplayItem().GetPrimitiveObject().GetBuildingCategory() == building.GetPrimitiveObject().GetBuildingCategory())
                            similarAmount++;
                }
            }

            return similarAmount;
        }

        public bool SimilarItemNextToTile(Compass Compass, Building.Building building)
        {
            if (building.GetPrimitiveObject().GetBuildingType() == BuildingType.Infrastructure)
            {
                Tile t = GetNeighbour(Compass);
                if (t != null)
                {
                    Building.Building neighbourBuilding = t.GetInfrastructureItem();
                    if (neighbourBuilding != null && neighbourBuilding.GetPrimitiveObject().GetBuildingCategory() == building.GetPrimitiveObject().GetBuildingCategory())
                    {
                        return true;
                    }
                }
            }
            else if (building.GetPrimitiveObject().GetBuildingType() == BuildingType.Gameplay)
            {
                Tile t = GetNeighbour(Compass);
                if (t != null)
                {
                    Building.Building neighbourBuilding = t.GetGameplayItem();
                    if (neighbourBuilding != null && neighbourBuilding.GetPrimitiveObject().GetBuildingCategory() == building.GetPrimitiveObject().GetBuildingCategory())
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
            return 1 * infrastructureItem.GetPrimitiveObject().GetMovementCost();
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
            return pathfindNode != null;
        }

        public Pathfinding.PathfindingNode<Tile> GetPathfindNode()
        {
            return pathfindNode;
        }

        public void SetZone(Zoning.Zone zone)
        {
            this.zone = zone;
            onZoneChanged?.Invoke(this);
        }

        public Zoning.Zone GetZone()
        {
            return zone;
        }

        public void RegisterOnInfrastructureObjectChanged(Action<Tile> a)
        {
            onInfrastructureObjectChanged += a;
        }

        public void RegisterOnGameplayObjectChanged(Action<Tile> a)
        {
            onGameplayObjectChanged += a;
        }

        public void RegisterOnZoneChanged(Action<Tile> a)
        {
            onZoneChanged += a;
        }
    }
}
