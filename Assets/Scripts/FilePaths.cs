public static class ComponentPaths
{
    public const string Master = "Custom";


    public const string Player = Master + "/Player";

    public const string PlayerController = Player + "/Player Controller";
    public const string PlayerMovement = Player + "/Player Movement";
    public const string PlayerCamera = Player + "/Player Camera";


    public const string Movement = Master + "/Movement";

    public const string MoverBehavior = Movement + "/Mover";
    public const string JumperBehavior = Movement + "/Jumper";
    public const string GrapplerBehavior = Movement + "/Grappler";

    public const string GrapplePoint = Master + "/Grapple Point";
}

public static class CreateAssetPaths
{
    public const string Data = "Data";

    public const string PlayerControllerData = Data + "/Controller";
    public const string MoverData = Data + "/Mover";
    public const string JumperData = Data + "/Jumper";
    public const string ObjectTargeterData = Data + "/Object Targeter";
    public const string GrapplerData = Data + "/Grappler";
    public const string PlayerDataChannel = Data + "/Player Channel";
}