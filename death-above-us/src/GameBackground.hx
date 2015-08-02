package;

import openfl.Assets;
import openfl.display.Bitmap;
import openfl.display.DisplayObjectContainer;
import openfl.Lib;
import openfl.Vector;

/**
 * ...
 * @author Evgeniy Morozov
 */
class GameBackground extends DisplayObjectContainer
{	
	public var backgroundBitmaps:Vector<Bitmap>;
	public var layers:Int;
	
	private var initialPostion:Float;
	
	public function new() 
	{
		super();
		
		layers = 0;
	}
		
	public function load(levelName:String):Bool
	{
		// Костыльный подсчет кол-ва текстур в папке
		while (Assets.exists("img/levels/" + levelName + "/background/bg" + layers + ".png"))
		{
			layers++;
		}
		if (layers == 0)
		{
			trace("No assets found for this level. Maybe wrong levelName?");
			return false;
		}
		trace("Assets found: " + layers);
		
		backgroundBitmaps = new Vector<Bitmap>(layers, true);
		for (i in 0 ... layers)	
		{
			backgroundBitmaps[i] = new Bitmap(Assets.getBitmapData("img/levels/" + levelName + "/background/bg" + i + ".png"));
			backgroundBitmaps[i].scaleX = Utils.gameScale;
			backgroundBitmaps[i].scaleY = Utils.gameScale;
			backgroundBitmaps[i].x = Lib.current.stage.stageWidth / 2 - backgroundBitmaps[i].width / 2;
			
			addChild(backgroundBitmaps[i]);
		}
		initialPostion = backgroundBitmaps[layers - 1].x;
		
		return true;
	}
	
	public function update(dt:Float) : Void
	{
		for (i in 0 ... layers)
		{
			backgroundBitmaps[i].x = initialPostion - Utils.cameraPosX * Utils.gameScale * (i + 1) / layers;
		}
	}
	
}