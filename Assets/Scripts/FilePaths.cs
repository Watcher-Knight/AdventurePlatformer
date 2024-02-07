public static class ComponentPaths
{
    public const string Master = "Custom";

    
    public const string Player = Master + "/Player";

    public const string PlayerMovement = Player + "/Player Movement";
    
    
    public const string GrapplePoint = Master + "/Grapple Point";
}

public static class CreateAssetPaths
{
    public const string Data = "Data";

    public const string MoverData = Data + "/Mover";
    public const string JumperData = Data + "/Jumper";
    public const string ObjectTargeterData = Data + "/ObjectTargeter";
    public const string GrapplerData = Data + "/Grappler";
}