using System;
using Microsoft.Xna.Framework;
using Nez.Textures;

namespace HospitalCeo.Appearance
{
    public class WorkerAppearence : Appearance
    {
        private Bodypart helmet;

        private static Subtexture[][] bodyTextures =
        {
            new[] { Utils.GlobalContent.Character.WorkerBody.BodyOne.South, Utils.GlobalContent.Character.WorkerBody.BodyOne.North, Utils.GlobalContent.Character.WorkerBody.BodyOne.West},
            new[] { Utils.GlobalContent.Character.WorkerBody.BodyTwo.South, Utils.GlobalContent.Character.WorkerBody.BodyTwo.North, Utils.GlobalContent.Character.WorkerBody.BodyTwo.West}
        };

        private static Subtexture[] hardHat =
        {
            Utils.GlobalContent.Character.Hats.HardHat.South, Utils.GlobalContent.Character.Hats.HardHat.North, Utils.GlobalContent.Character.Hats.HardHat.West
        };

        private static Color[] bodyReplaceBlue =
        {
            new Color(1, 114, 151),
            new Color(168, 52, 99),
            new Color(235, 255, 104),
            new Color(239, 103, 27),
            new Color(80, 49, 127),
            new Color(105, 167, 43)
        };

        private static Color[] hardHatColor =
        {
            new Color(1, 116, 183),
            new Color(242, 49, 44),
            new Color(33, 26, 26),
            new Color(213, 210, 207),
        };

        protected override Bodypart CreateBody()
        {
            Subtexture[] bodyTextures = GetBodyTexture();
            return new Bodypart(bodyTextures[0], bodyTextures[1], bodyTextures[2], true, colorList[Nez.Random.range(0, colorList.Length)], false, Color.Black, true, bodyReplaceBlue[Nez.Random.range(0, bodyReplaceBlue.Length)], Utils.RenderLayers.MOB_BOTTOM, Utils.RenderLayers.MOB_BOTTOM_1, Utils.RenderLayers.MOB_BOTTOM);
        }

        protected override Bodypart CreateHead()
        {
            Subtexture[] headTextures = GetHeadTextures();
            return new Bodypart(headTextures[0], headTextures[1], headTextures[2], true, skinColors[Nez.Random.range(0, skinColors.Length)], true, eyeColour[Nez.Random.range(0, eyeColour.Length)], true, professionalhairColours[Nez.Random.range(0, professionalhairColours.Length)], Utils.RenderLayers.MOB_BOTTOM_1, Utils.RenderLayers.MOB_BOTTOM, Utils.RenderLayers.MOB_BOTTOM_1);
        }

        protected Bodypart CreateHelmet()
        {
            return new Bodypart(hardHat[0], hardHat[1], hardHat[2], true, hardHatColor[Nez.Random.range(0, hardHatColor.Length)], false, Color.Black, false, Color.Black, Utils.RenderLayers.MOB_MIDDLE, Utils.RenderLayers.MOB_MIDDLE, Utils.RenderLayers.MOB_MIDDLE);
        }

        protected override Subtexture[] GetBodyTexture()
        {
            return bodyTextures[Nez.Random.range(0, bodyTextures.Length)];
        }

        public override void onAddedToEntity()
        {
            base.onAddedToEntity();
            helmet = CreateHelmet();
            entity.addComponent(helmet);
        }

        protected override void SpecificUpdate()
        {
            Vector2 offsetPosition = entity.position - lastPosition;

            // Heading North
            if (offsetPosition.Y < -1)
                helmet.SetToNorth();

            // Heading South
            else if (offsetPosition.Y > 1)
                helmet.SetToSouth();

            // Heading West
            else if (offsetPosition.X < -1)
                helmet.SetToWest();

            // Heading East
            else if (offsetPosition.X > 1)
                helmet.SetToEast();
        }
    }
}
