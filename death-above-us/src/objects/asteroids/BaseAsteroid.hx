package objects.asteroids;

import openfl.display.Sprite;
import openfl.events.Event;
import openfl.geom.Point;
import particles.FireParticle;
import particles.ParticlesEmitter;

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
	
	private var fireEmitter:ParticlesEmitter;
	
	public function new() { 
		super(); 
		addEventListener(Event.ADDED_TO_STAGE, setupParticles);
		addEventListener(Event.REMOVED_FROM_STAGE, removeParticles);
	}
	
	private function setupParticles(e:Event):Void 
	{
		removeEventListener(Event.ADDED_TO_STAGE, setupParticles);
		fireEmitter = new ParticlesEmitter(FireParticle, parent);
		fireEmitter.scaleX = mass;
	}
	
	private function removeParticles(e:Event):Void 
	{
		removeEventListener(Event.REMOVED_FROM_STAGE, removeParticles);
		fireEmitter.removeParticles();
		fireEmitter = null;
	}
	
	public function update(deltaTime:Float)
	{
		x += velocity * velocityDirection.x * deltaTime;
		y += velocity * velocityDirection.y * deltaTime;
		
		rotation += rotationSpeed * deltaTime;
		
		if (fireEmitter != null)
		{
			fireEmitter.x = x;
			fireEmitter.y = y;
			fireEmitter.update(deltaTime);
		}
	}
	
	public function onHit(damage:Float) {}
}