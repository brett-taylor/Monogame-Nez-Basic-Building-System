using Nez;
using Microsoft.Xna.Framework;
using Nez.Textures;
using Nez.Sprites;
using Microsoft.Xna.Framework.Graphics;

namespace HospitalCeo.Appearance_Builder
{
    public class Bodypart
    {
        private Appearance appearance;
        private Color color;
        private Subtexture north, south, east;
        private Sprite sprite;
        private int renderNorth, renderSouth, renderEast;

        public Bodypart(Appearance appearance, Color color, Subtexture north, Subtexture south, Subtexture east, int renderLayerNorth, int renderLayerSouth, int renderLayerEast)
        {
            this.appearance = appearance;
            this.color = color;
            this.north = north;
            this.south = south;
            this.east = east;
            this.renderNorth = renderLayerNorth;
            this.renderSouth = renderLayerSouth;
            this.renderEast = renderLayerSouth;
        }

        public void Show()
        {
            sprite = new Sprite();
            sprite.setSubtexture(south);
            sprite.setColor(color);
            appearance.entity.addComponent(sprite);
        }
    }
}
