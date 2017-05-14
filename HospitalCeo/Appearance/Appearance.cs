using Microsoft.Xna.Framework;
using Nez;
using Nez.Textures;

/*
 * Brett Taylor
 * Appearance class.
 * Holds the hair, head, body
 * Handles rotating the character etc
 */
namespace HospitalCeo.Appearance
{
    public abstract class Appearance : Component, IUpdatable
    {
        private static Subtexture[][] heads =
        {
            new[] { Utils.GlobalContent.Character.Head.HeadOne.South, Utils.GlobalContent.Character.Head.HeadOne.North, Utils.GlobalContent.Character.Head.HeadOne.West},
            new[] { Utils.GlobalContent.Character.Head.HeadTwo.South, Utils.GlobalContent.Character.Head.HeadTwo.North, Utils.GlobalContent.Character.Head.HeadTwo.West}
        };

        protected static Color[] colorList =
        {
            new Color(41, 41, 41),
            new Color(171, 171, 177),
            new Color(230, 230, 230),
            new Color(1, 79, 57),
            new Color(254, 206, 65),
            new Color(255, 109, 50),
            new Color(208, 3, 8),
            new Color(135, 23, 61),
            new Color(74, 65, 130),
            new Color(45, 49, 76),
            new Color(0, 121, 171),
            new Color(104, 188, 224),
            new Color(197, 218, 249)
        };

        protected static Color[] eyeColour =
        {
            new Color(40, 120, 218),
            new Color(32, 143, 40),
            new Color(123, 83, 40),
        };

        protected static Color[] skinColors =
        {
            new Color(253, 198, 137),
            new Color(222, 133, 121),
            new Color(139, 88, 39),
            new Color(243, 172, 71),
            new Color(146, 93, 54),
            new Color(240, 177, 121),
            new Color(253, 198, 137),
        };

        protected static Color[] professionalhairColours =
        {
            new Color(103, 75, 66),
            new Color(167, 164, 0),
            new Color(114, 76, 9),
            new Color(142, 142, 142),
            new Color(53, 35, 11),
            new Color(170, 76, 25),
            new Color(91, 59, 28),
        };

        protected abstract Subtexture[] GetBodyTexture();
        protected abstract Bodypart CreateBody();
        protected abstract Bodypart CreateHead();
        protected abstract void SpecificUpdate();
        protected Bodypart body;
        protected Bodypart head;
        protected Vector2 lastPosition;

        public Appearance()
        {
        }

        public void update()
        {
            if (entity == null)
                return;

            Vector2 offsetPosition = entity.position - lastPosition;

            // Heading North
            if (offsetPosition.Y < -1)
            {
                body.SetToNorth();
                head.SetToNorth();
            }

            // Heading South
            else if (offsetPosition.Y > 1)
            {
                body.SetToSouth();
                head.SetToSouth();
            }

            // Heading West
            else if (offsetPosition.X < -1)
            {
                body.SetToWest();
                head.SetToWest();
            }

            // Heading East
            else if (offsetPosition.X > 1)
            {
                body.SetToEast();
                head.SetToEast();
            }

            SpecificUpdate();
            lastPosition = entity.position;
        }

        public override void onAddedToEntity()
        {
            // Create the body
            body = CreateBody();
            this.addComponent<Bodypart>(body);

            // Create the head
            head = CreateHead();
            this.addComponent<Bodypart>(head);

            lastPosition = entity.position;
        }

        protected Subtexture[] GetHeadTextures()
        {
            return heads[Nez.Random.range(0, heads.Length)];
        }
    }
}
