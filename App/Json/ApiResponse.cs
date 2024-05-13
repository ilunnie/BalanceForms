using System.Collections.Generic;

public class ApiResponse
{
    public Respostas response { get; set; }
    public List<TestValue> test_value { get; set; }
}

public class TestValue
{
    public string _id { get; set; }
    public int test_value { get; set; }
    public int __v { get; set; }
}

public class ApiValues
{ 
    public List<ValoresDaPorva> values { get; set; }
}

public class ValoresDaPorva
{
    public string _id { get; set; }
    public List<int> test1 { get; set; } // Rename prova1 to test1 to match the JSON key
    public List<int> test2 { get; set; } // Rename prova2 to test2 to match the JSON key
    public int __v { get; set; }
}

public enum Respostas
{
    NComecado = 0,
    Comecado = 1, 
    Parou = 2
}