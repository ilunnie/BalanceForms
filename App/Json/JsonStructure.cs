using System.Collections.Generic;

public class TestResult
{
    public string nome { get; set; }
    public string nascimento { get; set; }
    public Test prova1 { get; set; }
    public Test prova2 { get; set; }
}

public class Test
{
    public List<int> corretas { get; set; } = new();
    public List<int> respostas { get; set; } = new();
    public int tempo { get; set; }
    public int quantidade { get; set; }
    public int tentativas { get; set; }
    public float acertos { get; set; }
}
