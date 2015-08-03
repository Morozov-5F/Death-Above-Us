package controllers;
import objects.Turret;

/**
 * @author Evgeniy Morozov
 */
interface IController 
{
	public var controllableTurret:Turret;
	public function update(deltaTime:Float):Void;
}