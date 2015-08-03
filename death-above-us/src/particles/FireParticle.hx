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
		
		maxLifeTime = 0.5;
		
		emitOnce = false;
		emitCount = 3;
		emitDelay = 0.01;
		
		friction = 0.99;
		sx = Math.random() * 100 - 50;
		sy = Math.random() * 100 - 50;
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