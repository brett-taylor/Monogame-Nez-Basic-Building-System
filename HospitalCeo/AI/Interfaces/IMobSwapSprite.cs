using Nez;
using Nez.Textures;

/*
 * Brett Taylor
 * Indicates the entity should swap it sprites out when heading north/east/south/west
 */

namespace HospitalCeo.AI.Interfaces
{
    public interface IMobSwapSprite
    {
        Subtexture GetNorthFacingSprite();
        Subtexture GetEastFacingSprite();
        Subtexture GetSouthFacingSprite();
        Subtexture GetWestFacingSprite();
        bool UsesEastSprite();
    }
}
