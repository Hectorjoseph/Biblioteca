namespace Dominio.Nucleo
{
    public static class DatosGenerales
    {
        public static string RutaJson { get; set; } = @"E:\Configuracion\secrets.json";
        public static bool UsaAzure { get; set; } = false;
        public static string Clave { get; set; } = "EVBgi345936456ghhVBJGtgnifytsidi3456678jhgUTytutyiiyi";
        public static string UsuarioDatos { get; set; } = EncriptarConversor.Encriptar("Test.Trghhjsgdj");
    }
}