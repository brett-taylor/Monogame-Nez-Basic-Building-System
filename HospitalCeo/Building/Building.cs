using Nez;
using Nez.Console;
using Nez.Sprites;
using Nez.Textures;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using HospitalCeo.World;

/*
 * Brett Taylor
 * Base building class, everything should be inherited from this
 */

/*
 * TO:DO
 * Convert buildings into a component system as well
 * BuildingComponent - Handles all building logic
 * BuildingRenderable - Handles the rendering of buildings
 * Custom Building Components:
 *  FoundationComponent - Would on build convert to walls and flooring
 *  
 * Functions:
 *  OnBuildCompleted
 *  OnUpdate
 */ 

namespace HospitalCeo.Building
{
    public class Building
    {
        protected Entity entity;
        protected Tile tile;
        protected Sprite sprite;

        public virtual string GetName() { return "Not set"; }
        public virtual Subtexture GetSprite() { return Utils.GlobalContent.Util.White_Tile; }
        public virtual BuildingType GetBuildingType() { return BuildingType.Gameplay; }
        public virtual void OnBuildingCompleted() { }
        public virtual bool CustomSpriteSettings() { return false; }
        public virtual void SetSpritesCorrectly() { }
        public virtual Vector2 GetSize() { return new Vector2(1, 1); }
        public virtual bool OneSquareWidth() { return false; }
        protected virtual void Update() { }
        protected virtual void FixedUpdate() { }
        protected virtual void LateUpdate() { }

        public Building(Vector2 position)
        {
            tile = WorldController.GetTileAt(position);
            entity = WorldController.SCENE.createEntity(GetName() + " x: " + position.X + " - y: " + position.Y);
            entity.position = position;
            SetSprite(GetSprite());
        }

        public void SetSprite(Subtexture texture)
        {
            if (sprite == null)
            {
                sprite = entity.addComponent(new Sprite(texture));
                sprite.renderLayer = 18;
            }
            sprite.setSubtexture(texture);
        }

        public void DoUpdate()
        {
            Update();
        }

        public void Destory()
        {
            entity.destroy();
        }

        public override string ToString()
        {
            return GetName() + " " + GetBuildingType() + " "+ tile.GetPosition();
        }
    }
}
