using System.Collections.Generic;

public class ApiResponse
{
    public Respostas response { get; set; }
}

public class ApiValues
{
    public List<int> prova1 { get; set; }
    public List<int> prova2 { get; set; }
}

public enum Respostas
{
    NComecado = 0,
    Comecado = 1, 
    Parou = 2
}