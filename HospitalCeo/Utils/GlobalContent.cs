using Microsoft.Xna.Framework.Graphics;
using Nez;
using Nez.Textures;
using System.Collections.Generic;

/*
 * Brett Taylor
 * Handles loading the global sprite altas's in.
 */

namespace HospitalCeo.Utils
{
    public static class GlobalContent
    {
        public static List<Subtexture> INFRASTRUCTURE_SPRITE_ALTAS;
        public static List<Subtexture> CHARACTER_SPRITE_ATLAS;
        public static List<Subtexture> OBJECT_SPRITE_ATLAS;

        public static void Initialise()
        {
            Texture2D infrastrucureSpriteAtlas = Core.content.Load<Texture2D>("hospitalceo/tiles");
            INFRASTRUCTURE_SPRITE_ALTAS = Subtexture.subtexturesFromAtlas(infrastrucureSpriteAtlas, 100, 100);
            Nez.Console.DebugConsole.instance.log("Amount of infrastructure textures loaded: " + INFRASTRUCTURE_SPRITE_ALTAS.Count);

            Texture2D characterSpriteAltas = Core.content.Load<Texture2D>("hospitalceo/characters");
            CHARACTER_SPRITE_ATLAS = Subtexture.subtexturesFromAtlas(characterSpriteAltas, 70, 70);
            Nez.Console.DebugConsole.instance.log("Amount of character textures loaded: " + CHARACTER_SPRITE_ATLAS.Count);

            Texture2D objectSpriteAtlas = Core.content.Load<Texture2D>("hospitalceo/objects");
            OBJECT_SPRITE_ATLAS = Subtexture.subtexturesFromAtlas(objectSpriteAtlas, 200, 100);
            Nez.Console.DebugConsole.instance.log("Amount of object textures loaded: " + OBJECT_SPRITE_ATLAS.Count);

        }

        public static class Util
        {
            public static Subtexture White_Tile = INFRASTRUCTURE_SPRITE_ALTAS[15];
        }

        public static class Zoning
        {
            public static Subtexture Purple = INFRASTRUCTURE_SPRITE_ALTAS[16];
        }

        public static class Earth
        {
            public static Subtexture Dirt = INFRASTRUCTURE_SPRITE_ALTAS[19]; 
            public static Subtexture Grass = INFRASTRUCTURE_SPRITE_ALTAS[20];
            public static Subtexture Sand = INFRASTRUCTURE_SPRITE_ALTAS[21];    
        }

        public static class Flooring
        {
            public static Subtexture CheapConcrete = INFRASTRUCTURE_SPRITE_ALTAS[18];
            public static Subtexture CheapCarpet = INFRASTRUCTURE_SPRITE_ALTAS[17];
        }

        public static class Wall
        {
            public static class BaseWall
            {
                public static Subtexture Horizontal = INFRASTRUCTURE_SPRITE_ALTAS[2];
                public static Subtexture Vertical = INFRASTRUCTURE_SPRITE_ALTAS[13];
                public static Subtexture Cross = INFRASTRUCTURE_SPRITE_ALTAS[0];

                public static Subtexture North_Ending = INFRASTRUCTURE_SPRITE_ALTAS[7];
                public static Subtexture East_Ending = INFRASTRUCTURE_SPRITE_ALTAS[1];
                public static Subtexture South_Ending = INFRASTRUCTURE_SPRITE_ALTAS[8];
                public static Subtexture West_Ending = INFRASTRUCTURE_SPRITE_ALTAS[14];

                public static Subtexture L_North_East = INFRASTRUCTURE_SPRITE_ALTAS[4];
                public static Subtexture L_North_West = INFRASTRUCTURE_SPRITE_ALTAS[5];
                public static Subtexture L_East_South = INFRASTRUCTURE_SPRITE_ALTAS[3];
                public static Subtexture L_South_West = INFRASTRUCTURE_SPRITE_ALTAS[6];

                public static Subtexture T_North_East_South = INFRASTRUCTURE_SPRITE_ALTAS[10];
                public static Subtexture T_North_South_West = INFRASTRUCTURE_SPRITE_ALTAS[12];
                public static Subtexture T_North_East_West = INFRASTRUCTURE_SPRITE_ALTAS[11];
                public static Subtexture T_East_South_West = INFRASTRUCTURE_SPRITE_ALTAS[9];
            }
        }

        public static class Character
        {
            public static class Hats
            {
                public static class HardHat
                {
                    public static Subtexture North = CHARACTER_SPRITE_ATLAS[0];
                    public static Subtexture South = CHARACTER_SPRITE_ATLAS[1];
                    public static Subtexture West = CHARACTER_SPRITE_ATLAS[2];
                }
            }

            public static class Head
            {
                public class HeadOne
                {
                    public static Subtexture North = CHARACTER_SPRITE_ATLAS[3];
                    public static Subtexture South = CHARACTER_SPRITE_ATLAS[4];
                    public static Subtexture West = CHARACTER_SPRITE_ATLAS[5];
                }

                public class HeadTwo
                {
                    public static Subtexture North = CHARACTER_SPRITE_ATLAS[6];
                    public static Subtexture South = CHARACTER_SPRITE_ATLAS[7];
                    public static Subtexture West = CHARACTER_SPRITE_ATLAS[8];
                }
            }

            public static class WorkerBody
            {
                public class BodyOne
                {
                    public static Subtexture North = CHARACTER_SPRITE_ATLAS[9];
                    public static Subtexture South = CHARACTER_SPRITE_ATLAS[10];
                    public static Subtexture West = CHARACTER_SPRITE_ATLAS[11];
                }

                public class BodyTwo
                {
                    public static Subtexture North = CHARACTER_SPRITE_ATLAS[12];
                    public static Subtexture South = CHARACTER_SPRITE_ATLAS[13];
                    public static Subtexture West = CHARACTER_SPRITE_ATLAS[14];
                }
            }
        }

        public static class Objects
        {
            public static class OfficeTable
            {
                public static Subtexture North_South = OBJECT_SPRITE_ATLAS[0];
            }
        }
    }
}
