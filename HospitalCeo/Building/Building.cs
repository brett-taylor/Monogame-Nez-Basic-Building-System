using HospitalCeo.Building.Gameplay.Components;
using HospitalCeo.Building.Gameplay.Interfaces;
using HospitalCeo.Building.Infrastructure.Components;
using HospitalCeo.Building.Infrastructure.Interfaces;
using HospitalCeo.Building.Shared_Components;
using HospitalCeo.Building.Shared_Interfaces;
using HospitalCeo.World;
using Microsoft.Xna.Framework;
using Nez;
using System;
using System.Collections.Generic;

namespace HospitalCeo.Building
{
    [MoonSharp.Interpreter.MoonSharpUserData]
    public class Building : Component
    {
        public Dictionary<string, Component> components { get; private set; }
        protected PrimitiveBuilding primitiveData;
        protected List<Tile> tiles;
        protected Vector2 position;
        protected Vector2 tilePosition;
        protected Vector2 size;
        protected Action<Vector2> onTileBuilt;

        public Building()
        {
            components = new Dictionary<string, Component>();
        }

        public void SetTileSize(Vector2 size)
        {
            this.size = size;
        }

        public void AttachToTile(List<Tile> tiles)
        {
            this.tiles = tiles;

            foreach (Tile t in tiles)
            {
                if (primitiveData.GetBuildingType() == BuildingType.Infrastructure)
                    t.SetInfrastructureItem(this);
                else
                    t.SetGameplayItem(this);
            }
        }

        public void SetInfo(Vector2 pos, Vector2 size)
        {
            tiles = new List<Tile>();

            for (int x = 0; x < size.X; x++)
            {
                for (int y = 0; y < size.Y; y++)
                {
                    Tile tile = WorldController.GetTileAt((int) pos.X + x, (int) pos.Y + y);
                    if (tile != null)
                    {
                        tiles.Add(tile);

                        if (primitiveData.GetBuildingType() == BuildingType.Infrastructure)
                            tile.SetInfrastructureItem(this);
                        else
                            tile.SetGameplayItem(this);
                    }
                }
            }

            SetTileSize(size);
            SetPosition(pos, isTileCoords: true);
        }

        public void SetPosition(Vector2 position, bool shouldSetToPosition = false, bool isTileCoords = false)
        {
            if (entity == null)
            {
                Nez.Console.DebugConsole.instance.log(entity + " has not attached entity - could not set position");
                Nez.Console.DebugConsole.instance.Open();
                return;
            }

            Tile tile = WorldController.GetTileAt(position, isTileCoords: isTileCoords);
            if (tile == null)
            {
                if (entity == null)
                {
                    Nez.Console.DebugConsole.instance.log("Building -> Not a valid tile");
                    Nez.Console.DebugConsole.instance.Open();
                    return;
                }
            }

            tilePosition = tile.GetTileNumber();
            entity.transform.position = tile.GetPosition();

            if (shouldSetToPosition)
                entity.transform.position = position;
        }

        public Vector2 GetTilePosition()
        {
            return tilePosition;
        }

        public Vector2 GetTileSize()
        {
            return size;
        }

        public List<Tile> GetTiles()
        {
            return tiles;
        }

        public Vector2 GetPosition()
        {
            return entity.position;
        }

        public void SetPrimitiveBuilding(PrimitiveBuilding building)
        {
            this.primitiveData = building;

            // Check to see if the primitive building reqires Beforebuild stuff
            IBuildingBeforeBuild buildingBeforeBuild = building as IBuildingBeforeBuild;
            if (buildingBeforeBuild != null)
            {
                buildingBeforeBuild.BeforeBuild(this);

                if (!buildingBeforeBuild.ContinueBuild())
                {
                    entity.destroy();
                    return;
                }
            }

            // Check to see if the primitive building wants a repeated texture renderer
            IBuildingRepeatedTextureRenderer buildingRepeatedTextureRenderer = building as IBuildingRepeatedTextureRenderer;
            if (buildingRepeatedTextureRenderer != null)
                entity.addComponent(new BuildingRepeatedTextureRenderer(this, buildingRepeatedTextureRenderer.GetTexture()));

            // Check to see if the primitive building wants a dynamic texture renderer
            IBuildingDynamicTexture buildingDynamicTextureRenderer = building as IBuildingDynamicTexture;
            if (buildingDynamicTextureRenderer != null)
                entity.addComponent(new BuildingDynamicTextureRenderer(this, buildingDynamicTextureRenderer));
            
            // Check to see if the primitive building wants a standard gameplay object renderer.
            IBuildingStandardGameplayRenderer buildingStandardGameplayRenderer = building as IBuildingStandardGameplayRenderer;
            if (buildingStandardGameplayRenderer != null)
                entity.addComponent(new BuildingStandardGameplayRenderer(this, buildingStandardGameplayRenderer.GetTexture()));

            // Check to see if the primitive building wishes to be consturcted instead of appear
            IBuildingRequiresConstruction buildingRequiresConstruction = building as IBuildingRequiresConstruction;
            if (buildingRequiresConstruction != null)
                entity.addComponent(new UnderConstruction(this, buildingRequiresConstruction.GetTimeRequiredToBuild()));
            else
                Finish();
        }

        public void Finish()
        {
            UnderConstruction cr = entity.getComponent<UnderConstruction>();
            for (int x = 0; x < size.X; x++)
                for (int y = 0; y < size.Y; y++)
                    cr.RemoveConstructionRenderer(new Vector2(x, y));
        }

        public void FinishAtTile(Vector2 tilePosition, bool isWorldSpace)
        {
            if (!isWorldSpace)
                tilePosition = LocalSpaceToWorldSpace(tilePosition);

            // Update the pathfinding
            Tile tile = WorldController.GetTileAt(tilePosition, isTileCoords: true);
            if (tile != null)
                WorldController.PATHFINDING_HUMAN_GRID.RebuildTile(tile);

            // Now make the building's renderer draw the sprite at that tile
            BuildingRenderer renderer = components["renderer"] as BuildingRenderer;
            if (renderer != null)
                renderer.ToggleSpriteAtPosition(tilePosition, true);

            if (onTileBuilt != null)
                onTileBuilt.Invoke(tilePosition);
        }

        public PrimitiveBuilding GetPrimitiveObject()
        {
            return primitiveData;
        }

        public Vector2 WorldSpaceToLocalSpace(Vector2 worldSpace)
        {
            return worldSpace - tilePosition;
        }

        public Vector2 LocalSpaceToWorldSpace(Vector2 localSpace)
        {
            return tilePosition + localSpace;
        }

        public void RegisterOnTileBuilt(Action<Vector2> a)
        {
            onTileBuilt += a;
        }

        public bool IsConstructed(Vector2 tilePosition, bool isWorldSpace)
        {
            if (isWorldSpace)
                tilePosition = WorldSpaceToLocalSpace(tilePosition);

            UnderConstruction underConstruction = entity.getComponent<UnderConstruction>();
            if (underConstruction == null)
                return true;

            return underConstruction.IsTileBuilt(tilePosition, false);
        }
    }
}
