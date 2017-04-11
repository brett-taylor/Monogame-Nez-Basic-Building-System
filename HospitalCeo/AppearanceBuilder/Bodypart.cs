using System;
using System.Collections.Generic;
using Nez;
using Microsoft.Xna.Framework;
using Nez.Textures;
using Nez.Sprites;
using Microsoft.Xna.Framework.Graphics;

namespace HospitalCeo.Appearance_Builder
{
    public class Bodypart : Sprite
    {
        private Subtexture southFacing, northFacing, westFacing;
        private Effect effect;
        private bool shouldSwapRed, shouldSwapGreen, shouldSwapBlue;
        private int renderLayerSouth, renderLayerNorth, renderLayerWest;
        private Color redSwap, greenSwap, blueSwap;

        public Bodypart(Subtexture south, Subtexture north, Subtexture west, bool shouldSwapRed, Color redSwap, bool shouldSwapGreen, Color greenSwap, bool shouldSwapBlue, Color blueSwap, int renderLayerSouth, int renderLayerNorth, int renderLayerWest)
        {
            this.southFacing = south;
            this.northFacing = north;
            this.westFacing = west;
            setSubtexture(south);
            this.shouldSwapRed = shouldSwapRed;
            this.shouldSwapGreen = shouldSwapGreen;
            this.shouldSwapBlue = shouldSwapBlue;
            this.redSwap = redSwap;
            this.greenSwap = greenSwap;
            this.blueSwap = blueSwap;
            this.renderLayerSouth = renderLayerSouth;
            this.renderLayerNorth = renderLayerNorth;
            this.renderLayerWest = renderLayerWest;
            renderLayer = this.renderLayerSouth;

            if (this.shouldSwapRed || this.shouldSwapGreen || this.shouldSwapBlue)
            {
                effect = Core.content.Load<Effect>("hospitalceo/shaders/CharacterColourPixelChange");
                effect = effect.Clone();
                effect.Parameters["Allowance"].SetValue(1f);

                if (this.shouldSwapRed)
                {
                    effect.Parameters["ShouldRedSwap"].SetValue(true);
                    effect.Parameters["RedChannelSwap"].SetValue(redSwap.ToVector4());
                }

                if (this.shouldSwapGreen)
                {
                    effect.Parameters["ShouldGreenSwap"].SetValue(true);
                    effect.Parameters["GreenChannelSwap"].SetValue(greenSwap.ToVector4());
                }

                if (this.shouldSwapBlue)
                {
                    effect.Parameters["ShouldBlueSwap"].SetValue(true);
                    effect.Parameters["BlueChannelSwap"].SetValue(blueSwap.ToVector4());
                }

                material = new Material(effect);
                setMaterial(material);
            }
        }

        public void SetToNorth()
        {
            setSubtexture(northFacing);
            flipX = false;
            renderLayer = renderLayerNorth;
        }

        public void SetToSouth()
        {
            setSubtexture(southFacing);
            flipX = false;
            renderLayer = renderLayerSouth;
        }

        public void SetToWest()
        {
            setSubtexture(westFacing);
            flipX = false;
            renderLayer = renderLayerWest;
        }

        public void SetToEast()
        {
            setSubtexture(westFacing);
            flipX = true;
            renderLayer = renderLayerWest;
        }
    }
}
