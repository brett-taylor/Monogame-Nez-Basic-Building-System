using Nez.Textures;

namespace HospitalCeo.Building.Infrastructure.Interfaces
{
    public interface IBuildingDynamicTexture
    {
        Subtexture SpriteNameHorizontal();
        Subtexture SpriteNameVertical();
        Subtexture SpriteNameWestEnding();
        Subtexture SpriteNameEastEnding();
        Subtexture SpriteNameNorthEnding();
        Subtexture SpriteNameSouthEnding();
        Subtexture SpriteNameLNorthEast();
        Subtexture SpriteNameLNorthWest();
        Subtexture SpriteNameLEastSouth();
        Subtexture SpriteNameLSouthWest();
        Subtexture SpriteNameTNorthEastSouth();
        Subtexture SpriteNameTNorthEastWest();
        Subtexture SpriteNameTNorthSouthWest();
        Subtexture SpriteNameTEastSouthWest();
        Subtexture SpriteNameCross();
    }
}
