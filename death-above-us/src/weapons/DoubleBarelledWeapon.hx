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
class DoubleBarelledWeapon extends BaseWeapon
{
	private var barrellOffset:Float;
	private var basePosition:Point;
	
	public function new(position:Point, barrellOffset:Float, bulletType:Class<BaseBullet>) 
	{
		super();
		this.bulletType = bulletType;
		basePosition = this.position = position;
		this.barrellOffset = barrellOffset;
		
		reloadMeter = 0;
		reloadTime = 1;
	}
	
	override public function createShot() :Void
	{
		super.createShot();
		trace(reloadMeter);
		if (reloadMeter <= reloadTime)
		{
			return;
		}
	
		var bulletsToAdd = new Vector<BaseBullet>(2, false);
		var multiplier:Point = new Point(Math.cos(rotation) * barrellOffset / 2 * Utils.gameScale, Math.sin(rotation) * barrellOffset / 2 * Utils.gameScale);
		var offsetLeft = new Point(position.x - multiplier.x, position.y - multiplier.y);
		var offsetRight = new Point(position.x +  multiplier.x, position.y + multiplier.y);
		bulletsToAdd[0] = new SimpleBullet(offsetRight, new Point(10, 10), rotation);
		bulletsToAdd[1] = new SimpleBullet(offsetRight, new Point(10, 10), rotation);
		// TODO: bulletMaking
		reloadMeter = 0;
		dispatchEvent(new WeaponShotEvent(WeaponShotEvent.WEAPON_SHOT, false, false, bulletsToAdd));
	}
	
	override public function update(deltaTime:Float):Void 
	{
		super.update(deltaTime);
		
		position = new Point(40 * Math.sin(Math.PI / 180 * rotation) + basePosition.x, -40 * Math.cos(Math.PI / 180 * rotation) + basePosition.y);
		reloadMeter += deltaTime;
		
	}
}