package weapons.bullets;

import openfl.display.Sprite;

/**
 * Класс, реализующий базовый снаряд
 * @author Evgeniy Morozov
 */
class BaseBullet extends Sprite
{
	public var velocity:Float;
	public var damage:Float;
	public function update(deltaTime: Float) : Void {}
}