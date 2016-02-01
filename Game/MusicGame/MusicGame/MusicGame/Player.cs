using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace MusicGame
{
	class Player : GameObject
	{
		public Player(Texture2D texture, Vector2 position, int textureRows, int textureColumns) : base(texture, position, textureRows, textureColumns)
		{
			Texture = texture;
			Position = position;
			
		}
		
	}
	
}