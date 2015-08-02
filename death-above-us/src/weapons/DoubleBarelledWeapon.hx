package weapons;

/**
 * Класс пушки с двумя стволами
 * @author Evgeniy Morozov
 */
class DoubleBarelledWeapon extends BaseWeapon
{
	private var barrellOffset:Float;
	
	public function new(bulletType:Class) 
	{
		this.bulletType = bulletType;
	}
	
	override public function createShot() 
	{
		super.createShot();

	}
	
	override public function update(deltaTime:Float):Void 
	{
		super.update(deltaTime);
		
	}
}