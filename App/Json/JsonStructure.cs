public class TestResult
{
    public string nome { get; set; }
    public string nascimento { get; set; }
    public Test prova1 { get; set; }
    public Test prova2 { get; set; }
}

public class Test
{
    public int triangulo { get; set; }
    public int quadrado { get; set; }
    public int circulo { get; set; }
    public int estrela { get; set; }
    public int hexagono { get; set; }
    public int tempo { get; set; }
    public int quantidade { get; set; }
    public int tentativas { get; set; }
    public float acertos { get; set; }
}