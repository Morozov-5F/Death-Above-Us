package weapons;

import openfl.display.Sprite;
import openfl.events.Event;
import openfl.Vector;
import weapons.bullets.BaseBullet;

/**
 * Событие выстрела пушки
 * @author Evgeniy Morozov
 */
class WeaponShotEvent extends Event
{
	public static inline var WEAPON_SHOT = "weaponShot";
	public var bullets:Vector<BaseBullet>;
	
	public function new (type:String, bubbles:Bool, cancelable:Void, bullets:Vector<BaseBullet>):Void
	{
		super(type, bubbles, cancelable);
		this.bullets = bullets;
	}
}

 /**
 * Класс базовой пушки
 * @author Evgeniy Morozov
 */
class BaseWeapon extends Sprite
{
	
	private var reloadTime:Float;
	private var reloadMeter:Float;
	
	private var bulletType:Class;
	
	public function update(deltaTime:Float):Void { };
	public function createShot():Void { };
}