using HospitalCeo.World;
using Microsoft.Xna.Framework;
using Nez;
using Nez.Textures;

/*
 * Brett Taylor
 * The zone class
 */

namespace HospitalCeo.Zoning
{
    public abstract class Zone : Component
    {
        protected abstract string GetName();
        public abstract Color GetColor();
        public abstract Subtexture GetTexture();

        protected Vector2 position;
        protected Vector2 size;
        protected Tile[,] tiles;
        protected VisualZoneComponent visualZone;

        public Zone(Vector2 position, Vector2 size)
        {
            this.position = position;
            this.size = size;
            tiles = new Tile[(int) size.X, (int) size.Y];

            // Update all tiles under this zone about the new zone above it
            for (int x = 0; x < size.X; x++)
                for (int y = 0; y < size.Y; y++)
                {
                    Tile tile = WorldController.GetTileAt((int) position.X + x, (int) position.Y + y);
                    if (tile == null) continue;
                    tile.SetZone(this);
                    tiles[x, y] = tile;
                }

            visualZone = new VisualZoneComponent(this);
        }

        public override void onAddedToEntity()
        {
            base.onAddedToEntity();
            entity.position = new Vector2(this.position.X * 100 + 50, this.position.Y * 100 + 50);
            entity.addComponent<VisualZoneComponent>(visualZone);
        }

        public override string ToString()
        {
            return "Zone (" + GetName() + ") at position: " + position + ", size: " + size;
        }

        public Vector2 GetPosition()
        {
            return position;
        }

        public Vector2 GetSize()
        {
            return size;
        }

        public Tile[,] GetTiles()
        {
            return tiles;
        }
    }
}
