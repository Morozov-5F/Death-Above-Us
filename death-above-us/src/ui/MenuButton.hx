package ui;

import js.html.Navigator;
import openfl.Assets;
import openfl.events.Event;
import openfl.events.MouseEvent;
import openfl.events.TouchEvent;
import openfl.text.Font;
import openfl.text.TextField;
import openfl.text.TextFieldAutoSize;
import openfl.text.TextFormat;

/**
 * ...
 * @author someone
 */

class MenuButton extends TextField
{
	public static var DOWN:String = "down";
	
	private var normalColor:Int = 0xFFFFFF;
	private var hoverColor:Int = 0xFF0000;
	
	public function new(text:String) 
	{
		super();
		var fontName:String = null;
		#if !html5
		fontName = Assets.getFont("fonts/arial.ttf").fontName;
		#end
		var textFormat:TextFormat = new TextFormat(fontName, 32, 0xFFFFFF);
		setTextFormat(textFormat);
		this.text = text;
		autoSize = TextFieldAutoSize.LEFT;
		
		addEventListener(MouseEvent.MOUSE_OVER, onMouseOver);
		addEventListener(MouseEvent.MOUSE_OUT, onMouseOut);
		addEventListener(MouseEvent.CLICK, onMouseClick);
		addEventListener(TouchEvent.TOUCH_END, onMouseClick);
	}
	
	private function onMouseClick(e:Event):Void 
	{
		dispatchEvent(new Event(MenuButton.DOWN));
	}
	
	private function onMouseOut(e:MouseEvent):Void 
	{
		textColor = normalColor;
	}
	
	private function onMouseOver(e:MouseEvent):Void 
	{
		textColor = hoverColor;
	}
	
	
	
}