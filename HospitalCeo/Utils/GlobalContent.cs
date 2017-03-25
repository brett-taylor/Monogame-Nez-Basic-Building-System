using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Nez;
using Nez.Sprites;
using Nez.Textures;

/*
 * Brett Taylor
 * Handles loading the global sprite altas's in.
 */

namespace HospitalCeo.Utils
{
    public static class GlobalContent
    {
        public static List<Subtexture> INFRASTRUCTURE_SPRITE_ALTAS;
        public static List<Subtexture> MOB_SPRITE_ATLAS;

        public static void Initialise()
        {
            Texture2D infrastrucureSpriteAtlas = Core.content.Load<Texture2D>("hospitalceo/tiles");
            INFRASTRUCTURE_SPRITE_ALTAS = Subtexture.subtexturesFromAtlas(infrastrucureSpriteAtlas, 100, 100);
            Nez.Console.DebugConsole.instance.log("Amount of infrastructure textures loaded: " + INFRASTRUCTURE_SPRITE_ALTAS.Count);

            Texture2D mobSpriteAtlas = Core.content.Load<Texture2D>("hospitalceo/mobs");
            MOB_SPRITE_ATLAS = Subtexture.subtexturesFromAtlas(mobSpriteAtlas, 70, 70);
            Nez.Console.DebugConsole.instance.log("Amount of mob textures loaded: " + MOB_SPRITE_ATLAS.Count);
        }

        public static class Util
        {
            public static Subtexture White_Tile = INFRASTRUCTURE_SPRITE_ALTAS[9];
        }

        public static class Earth
        {
            public static Subtexture Dirt = INFRASTRUCTURE_SPRITE_ALTAS[22]; 
            public static Subtexture Grass = INFRASTRUCTURE_SPRITE_ALTAS[13];
            public static Subtexture Sand = INFRASTRUCTURE_SPRITE_ALTAS[14];    
        }

        public static class Flooring
        {
            public static Subtexture CheapConcrete = INFRASTRUCTURE_SPRITE_ALTAS[17];
            public static Subtexture CheapCarpet = INFRASTRUCTURE_SPRITE_ALTAS[12];
        }

        public static class Wall
        {
            public static class BaseWall
            {
                public static Subtexture Horizontal = INFRASTRUCTURE_SPRITE_ALTAS[10];
                public static Subtexture Vertical = INFRASTRUCTURE_SPRITE_ALTAS[7];
                public static Subtexture Cross = INFRASTRUCTURE_SPRITE_ALTAS[0];

                public static Subtexture North_Ending = INFRASTRUCTURE_SPRITE_ALTAS[3];
                public static Subtexture East_Ending = INFRASTRUCTURE_SPRITE_ALTAS[5];
                public static Subtexture South_Ending = INFRASTRUCTURE_SPRITE_ALTAS[4];
                public static Subtexture West_Ending = INFRASTRUCTURE_SPRITE_ALTAS[8];

                public static Subtexture L_North_East = INFRASTRUCTURE_SPRITE_ALTAS[20];
                public static Subtexture L_North_West = INFRASTRUCTURE_SPRITE_ALTAS[1];
                public static Subtexture L_East_South = INFRASTRUCTURE_SPRITE_ALTAS[15];
                public static Subtexture L_South_West = INFRASTRUCTURE_SPRITE_ALTAS[2];

                public static Subtexture T_North_East_South = INFRASTRUCTURE_SPRITE_ALTAS[11];
                public static Subtexture T_North_South_West = INFRASTRUCTURE_SPRITE_ALTAS[21];
                public static Subtexture T_North_East_West = INFRASTRUCTURE_SPRITE_ALTAS[16];
                public static Subtexture T_East_South_West = INFRASTRUCTURE_SPRITE_ALTAS[6];
            }
        }

        public static class Worker
        {
            public static Subtexture Worker_North = MOB_SPRITE_ATLAS[1];
            public static Subtexture Worker_South = MOB_SPRITE_ATLAS[2];
            public static Subtexture Worker_West = MOB_SPRITE_ATLAS[0];
        }
    }
}
