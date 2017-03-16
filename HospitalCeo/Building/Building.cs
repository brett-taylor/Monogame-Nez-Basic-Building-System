using Nez;
using Nez.Console;
using Nez.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using HospitalCeo.World;

/*
 * Brett Taylor
 * Base building class, everything should be inherited from this
 */

namespace HospitalCeo.Building
{
    public class Building
    {
        protected Entity entity;
        protected Tile tile;

        public virtual string GetName() { return "Not set"; }
        public virtual string GetSpriteName() { return "Not set"; }
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
            entity = WorldController.SCENE.createEntity(GetName() + " x: " + position.X + " - y: " + position.Y);
            entity.addComponent<Sprite>(new Sprite(WorldController.SCENE.content.Load<Texture2D>(GetSpriteName())));
            tile = WorldController.GetTileAt(position);
        }

        public void SetSprite(string spriteName)
        {
            Sprite sprite = new Sprite(WorldController.SCENE.content.Load<Texture2D>(spriteName));
            if (sprite != null) SetSprite(sprite);
            else DebugConsole.instance.log("Sprite not found: " + spriteName);
        }

        public void SetSprite(Sprite sprite)
        {
            entity.removeComponent<Sprite>();
            entity.addComponent<Sprite>(sprite);
        }

        public void DoUpdate()
        {
            Update();
        }

        public void DoFixedUpdate()
        {
            FixedUpdate();
        }

        public void DoLateUpdate()
        {
            FixedUpdate();
        }

        public void Destory()
        {
            entity.destroy();
        }

        public override string ToString()
        {
            return GetName() + " " + GetBuildingType() + " "+ tile.position;
        }
    }
}
