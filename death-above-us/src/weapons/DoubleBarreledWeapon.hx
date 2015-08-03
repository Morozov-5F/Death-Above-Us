package weapons;

import openfl.geom.Point;
import openfl.Vector;
import weapons.BaseWeapon.WeaponShotEvent;
import weapons.bullets.BaseBullet;
import weapons.bullets.SimpleBullet;

/**
 * Класс пушки с двумя стволами
 * @author Evgeniy Morozov
 */
class DoubleBarreledWeapon extends BaseWeapon
{
	private var barrelOffset:Float;
	private var basePosition:Point;
	
	private static inline var BULLET_SPEED:Float = 450;
	
	public function new(position:Point, barrelOffset:Float, bulletType:Class<BaseBullet>) 
	{
		super();
		this.bulletType = bulletType;
		basePosition = this.position = position;
		this.barrelOffset = barrelOffset;
		
		reloadMeter = 0;
		reloadTime = 100;
	}
	
	override public function createShot() :Void
	{
		super.createShot();
		if (reloadMeter <= reloadTime)
		{
			return;
		}
		// Здесь не используем в типе вектора bulletType, потому что здесь 
		// все объекты будут иметь одну и ту же суть
		var bulletsToAdd = new Vector<BaseBullet>(2, false);
		// По сути, трансляция кода с C#
		// TODO: заменить на более читаемый код
		var multiplier:Point = new Point(Math.cos(Utils.DEG_TO_RAD * rotation) * barrelOffset / 2 * Utils.gameScale, Math.sin(Utils.DEG_TO_RAD * rotation) * barrelOffset / 2 * Utils.gameScale);
		var offsetLeft =  new Point(position.x - multiplier.x, position.y - multiplier.y);
		var offsetRight = new Point(position.x + multiplier.x, position.y + multiplier.y);
		
		bulletsToAdd[0] = Type.createInstance(bulletType, [offsetLeft,  BULLET_SPEED, rotation]);
		bulletsToAdd[1] = Type.createInstance(bulletType, [offsetRight, BULLET_SPEED, rotation]);
	
		reloadMeter = 0;
		dispatchEvent(new WeaponShotEvent(WeaponShotEvent.WEAPON_SHOT, false, false, bulletsToAdd));
	}
	
	override public function update(deltaTime:Float):Void 
	{
		super.update(deltaTime);
		
		position = new Point(40 * Math.sin(Math.PI / 180 * rotation) + basePosition.x, -40 * Math.cos(Math.PI / 180 * rotation) + basePosition.y);
		// Чтобы какая-нибудь ерунда с переполнением не случилась
		reloadMeter += deltaTime * 1000;
	}
}