package;

/**
 * ...
 * @author Evgeniy Morozov
 */
class Utils
{
	public static var cameraPosX: Float;
	public static var gameScale : Float;
	
	public static inline var DEG_TO_RAD: Float = 3.1415 / 180;
	
	public static inline function getRandomInt(left:Int, right:Int):Int
	{
		return Math.round(Math.random() * (right - left) + left);
	}
	
	public static inline function getRandomFloat(left:Float, right:Float):Float
	{
		return Math.random() * (right - left) + left;
	}
}