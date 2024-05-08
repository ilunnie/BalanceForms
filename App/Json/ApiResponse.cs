public class ApiResponse
{
    public Respostas response { get; set; }
}

public enum Respostas
{
    NComecado = 0,
    Comecado = 1, 
    Parou = 2
}