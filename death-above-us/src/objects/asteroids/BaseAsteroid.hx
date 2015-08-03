package objects.asteroids;

import openfl.display.Sprite;
import openfl.geom.Point;

/**
 * Базовый класс астероида
 * @author Evgeniy Morozov
 */
class BaseAsteroid extends Sprite
{
	public var hp:Float;
	
	private var velocityDirection:Point;
	private var velocity:Float;
	private var rotationSpeed:Float;
	
	// Своеобразный масштаб астероида: 
	// больше масса - больше астероид
	private var mass:Float;
	
	public function new() { super(); }
	
	public function update(deltaTime:Float)
	{
		x += velocity * velocityDirection.x * deltaTime;
		y += velocity * velocityDirection.y * deltaTime;
		
		rotation += rotationSpeed * deltaTime;
	}
	
	public function onHit(damage:Float) {}
}