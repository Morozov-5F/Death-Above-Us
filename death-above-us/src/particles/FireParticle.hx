package particles;
import openfl.display.BitmapData;

/**
 * Частицы огня
 * @author dcr30
 */
class FireParticle extends Particle
{
	public function new(texture:BitmapData) 
	{
		super(texture);
		
		// Настройки частицы
		textureName = "fire";
		
		maxLifeTime = 1;
		
		emitOnce = false;
		emitCount = 1;
		emitDelay = 0.01;
		
		friction = 0.99;
		var speed:Float = 80;
		sx = Math.random() * speed - speed/2;
		sy = Math.random() * speed - speed/2;
		rotation = Math.random() * 360;
	}
	
	override public function update(deltaTime:Float):Void 
	{
		super.update(deltaTime);
		var lifeMul:Float = 1 - lifeTime / maxLifeTime;
		// Плавное исчезание
		alpha = lifeMul;
	}
	
}