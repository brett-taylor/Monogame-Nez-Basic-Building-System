﻿using Nez.Sprites;
using Microsoft.Xna.Framework;


namespace Nez
{
	/// <summary>
	/// renders a sprite with an outline in a very inefficient (but simple) way. The sprite is rendered multiple times offset/colored then it
	/// is rendered normally on top of that.
	/// </summary>
	public class SpriteOutlineRenderer : RenderableComponent
	{
		public override float width { get { return _sprite.width + outlineWidth * 2; } }
		public override float height { get { return _sprite.height + outlineWidth * 2; } }

		public override RectangleF bounds { get { return _sprite.bounds; } }

		/// <summary>
		/// the width of the outline
		/// </summary>
		public int outlineWidth = 3;

		/// <summary>
		/// the color the sprite will be tinted when it is rendered
		/// </summary>
		public Color outlineColor = Color.Black;

		Sprite _sprite;


		/// <summary>
		/// the Sprite passed in will be disabled. The SpriteOutlineRenderer will handle manually calling its render method.
		/// </summary>
		/// <param name="sprite">Sprite.</param>
		public SpriteOutlineRenderer( Sprite sprite )
		{
			_sprite = sprite;
			// RenderableComponent doesnt have an origin so we copy over the Sprite.origin to our localOffset
			_localOffset = sprite.origin;
			_sprite.enabled = false;
		}


		public override void onEntityTransformChanged( Transform.Component comp )
		{
			base.onEntityTransformChanged( comp );

			// our sprite is disabled so we need to forward the call over to it so it can update its bounds for rendering
			_sprite.onEntityTransformChanged( comp );
		}


		public override void render( Graphics graphics, Camera camera )
		{
			_sprite.drawOutline( graphics, camera, outlineColor, outlineWidth );
			_sprite.render( graphics, camera );
		}
	}
}

