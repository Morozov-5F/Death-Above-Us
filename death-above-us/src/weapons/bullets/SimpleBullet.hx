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
	
	public function new(postion:Point, velocity:Point, rotation:Float) 
	{
		super();
		var bulletTexture = new Bitmap(Assets.getBitmapData("img/bullets/simpleBullet.png"));
		bulletTexture.x = -bulletTexture.width  / 2;
		bulletTexture.y = -bulletTexture.height / 2;
		addChild(bulletTexture);
		
		this.rotation = rotation;
		
		var pos = localToGlobal(postion);
		x = pos.x;
		y = pos.y;
		
		sx = velocity.x;
		sy = velocity.y;
		
		scaleX = scaleY = Utils.gameScale;
	}
	
	override public function update(deltaTime:Float):Void 
	{
		super.update(deltaTime);
		
		x += sx * deltaTime * Math.sin(Utils.DEG_TO_RAD * rotation);
		y -= sy * deltaTime * Math.cos(Utils.DEG_TO_RAD * rotation);
	}
	
}