package weapons.bullets;
import openfl.Assets;
import openfl.display.Bitmap;
import openfl.geom.Point;

/**
 * ...
 * @author Evgeniy Morozov
 */
class SimpleBullet extends BaseBullet
{
	
	public function new(position:Point, velocity:Float, rotation:Float) 
	{
		super();
		var bulletTexture = new Bitmap(Assets.getBitmapData("img/bullets/simpleBullet.png"));
		bulletTexture.x = -bulletTexture.width  / 2;
		bulletTexture.y = -bulletTexture.height / 2;
		addChild(bulletTexture);
		
		this.rotation = rotation;
		
		x = position.x;
		y = position.y;
		
		damage = 5;
		
		this.velocity = velocity;
		scaleX = scaleY = Utils.gameScale;
	}
	
	override public function update(deltaTime:Float):Void 
	{
		super.update(deltaTime);
		
		x += velocity * deltaTime * Math.sin(Utils.DEG_TO_RAD * rotation);
		y -= velocity * deltaTime * Math.cos(Utils.DEG_TO_RAD * rotation);
	}
	
}